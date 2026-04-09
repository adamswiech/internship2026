import { accessSwaggerJsonContent } from "./accessServer.js";
import fs from "node:fs/promises";
import { apiAddressesList } from "../scripts/accessServer.js";
import { dictionaryType } from "./dictionary.js";

const API_PATH = process.env.API_PATH;
const HTTPS_URL = apiAddressesList.httpsApiAddress;

console.log("\x1b[32mgenerateApi.js\x1b[0m");
console.log("HTTPS_URL: ", HTTPS_URL);

const deleteAllDataFromApi = async () => {
  try {
    await fs.unlink(`${API_PATH}/api.ts`);
  } catch {
    console.log("Nothing to clear in api.ts\n");
  }
};
await deleteAllDataFromApi();

function mediaTypeToJsBodyType(mediaType) {
  if (!mediaType || typeof mediaType !== "string") {
    return "Json";
  }

  let type = mediaType.split("/").pop()?.trim() || "";
  const parts = type.split(/[-+]/);
  const pascalCase = parts
    .map((part) => part.charAt(0).toUpperCase() + part.slice(1).toLowerCase())
    .join("");

  return pascalCase || "Raw";
}

const fetchApiEndpointData = async () => {
  const swaggerJsonContent = await accessSwaggerJsonContent;

  if (!swaggerJsonContent || typeof swaggerJsonContent !== "object") {
    console.error("Invalid or missing Swagger JSON:", swaggerJsonContent);
    return;
  }

  const paths = swaggerJsonContent.paths;

  if (!paths || typeof paths !== "object") {
    console.log("No paths found in Swagger document.");
    return;
  }

  const dataList = [];

  for (const [path, methodsObj] of Object.entries(paths)) {
    for (const [method, operation] of Object.entries(methodsObj)) {
      let hasRequestBody = false; //clear this hardcoded values
      let requestBodyProperties = null;
      let hasContent = false; //check if json has content in responses to recognize if it is GET or POST, true - get, false - post
      let returnTypeFromRef = "";
      let tags = null;
      let args = [];
      let parameters = []; //fix this XDDD

      //here you add arguments to method arguments in api.ts
      if (Array.isArray(operation.parameters)) {
        parameters = operation.parameters.map((param) => {
          const schema = param.schema || {};

          args.push({
            name: param.name,
            in: param.in,
            type: schema.type || "string",
            format: schema.format || null,
            required: !!param.required,
            description: param.description || "",
          });
        });
      }

      if (!operation || typeof operation !== "object") continue;
      if (operation.requestBody?.content) {
        const contentTypes = Object.keys(operation.requestBody.content);

        if (contentTypes.length > 0) {
          const mediaType =
            contentTypes.find((ct) => ct.includes("multipart/form-data")) ||
            contentTypes[0];

          const mediaTypeObject = operation.requestBody.content[mediaType];

          if (mediaTypeObject?.schema?.properties) {
            const properties = mediaTypeObject.schema.properties;

            // Dynamically find the file/binary property
            let fieldName = null;
            let fileProps = null;

            // First, look for a property with type: string + format: binary
            for (const [propName, schema] of Object.entries(properties)) {
              if (schema?.type === "string" && schema?.format === "binary") {
                fieldName = propName;
                fileProps = schema;
                break;
              }
            }

            // Fallback: if no binary field found, take the first property
            if (!fieldName) {
              fieldName = Object.keys(properties)[0];
              fileProps = properties[fieldName];
            }

            // Now build the object
            requestBodyProperties = {
              name: fieldName, // ← "file" (or whatever the field is named)
              type: fileProps?.type || "string",
              format: fileProps?.format || "binary",
              contentType: mediaTypeToJsBodyType(mediaType), // "FormData"
              mediaType: mediaType, // original full media type (optional but useful)
              isFileUpload: true,
            };
          }
        }
      }

      const successResponse =
        operation.responses?.["200"] || operation.responses?.["201"];
      if (successResponse?.content) {
        hasContent = true;

        const firstContentType = Object.keys(successResponse.content)[0];
        const schema = successResponse.content[firstContentType]?.schema;

        if (schema) {
          if (schema.type === "array" && schema.items?.$ref) {
            const ref = schema.items.$ref;
            //for arrays
            returnTypeFromRef = `${ref.split("/").pop()}[]`;
          } else if (schema.$ref) {
            //for single return
            returnTypeFromRef = `${schema.$ref.split("/").pop()}`;
          } else if (schema.type) {
            returnTypeFromRef = schema.type;
            if (schema.format) returnTypeFromRef += `(${schema.format})`; //CHECK THIS LINE
          } else {
            returnTypeFromRef = "any";
          }
        }
      }

      dataList.push({
        method: method.toUpperCase(),
        url: path,
        tags: Array.isArray(operation.tags) ? operation.tags : [],
        hasRequestBody: hasRequestBody,
        requestBodyProperties: requestBodyProperties || null,
        hasContent: hasContent,
        returnType: returnTypeFromRef || "any",
        args: args,
      });

      console.log(
        `\nmethod:${method}, 
        url: ${path}, 
        tags: ${tags}, 
        hasRequestBody: ${hasRequestBody},
        format: ${requestBodyProperties != null ? requestBodyProperties.format : "null"}, 
        type: ${requestBodyProperties != null ? requestBodyProperties.type : "null"}, 
        fileName: ${requestBodyProperties != null ? requestBodyProperties.name : "null"}, 
        hasContent: ${hasContent}, 
        returnType: ${returnTypeFromRef == "any" ? "xyz" : returnTypeFromRef}`,
      );
    }
  }

  return dataList;
}; //whole function works

const apiEndpointsList = await fetchApiEndpointData();

// const printArgs = (args) => {
//     let argsText = "";

//     for(let el of args) {
//         argsText += `${el}: any, `;
//     }
// }

const generateApiFile = async () => {
  let imports = [];

  await fs.appendFile(
    `${API_PATH}/api.ts`,
    `export default class Api {\n`,
    "utf8",
  );
  let i = 0;
  for (const endpoint of apiEndpointsList) {
    const endpointName = endpoint.url.substring(
      endpoint.url.lastIndexOf("/") + 1,
    );
    let methodCode = "";

    methodCode = `public static async ${endpointName}(${
      endpoint.requestBodyProperties != undefined
        ? `${endpoint.requestBodyProperties.name}:${endpoint.requestBodyProperties.contentType}`
        : endpoint.args.length > 0
          ? endpoint.args[0].name + `: ${dictionaryType[endpoint.args[0].type]}`
          : ""
    }): Promise<${endpoint.returnType}> {`;

    switch (endpoint.method) {
      case "GET":
        methodCode += `
        const response = await fetch("${HTTPS_URL}${endpoint.url}");
        const jsonResponse: ${endpoint.returnType} = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status:" + response.status);
        }   
            
        return jsonResponse;        
      
      \n}\n
      `;
        break;

      case "POST":
        methodCode += `
            const response = await fetch(
                "${HTTPS_URL}${endpoint.url}", 
                {
                    method: "${endpoint.method.toUpperCase()}",
                    body: file
                }
            );
            const jsonResponse: ${endpoint.returnType} = await response.json();

            if (!response.ok) {
                throw new Error("HTTP error! status:" + response.status);
            }

            return jsonResponse;        
      \n}\n`;
        break;
    }

    //1. how to recognize that POST has to upload data to db and doesn't behave as GET? - Check if json has "content" in "responses"
    //a. if has just go to $ref and take this last part after last / -> that is your type to return
    //b. if has not - you know that this POST is uploading data.

    //2. how to recognize if returned type should be array or sth else?
    //a. if "content" > "text/plain" > "schema" > "type" = "array" set type as "$ref" value + [] (array of given type)
    //b. if "content" doesn't have this "type":"array" just set type as "$ref" value

    const typeName = endpoint.tags[0];

    if (!imports.includes(typeName)) {
      console.log(typeName);
      imports.push(typeName);
    }

    i++;

    await fs.appendFile(`${API_PATH}/api.ts`, methodCode, "utf8");
  }

  await fs.appendFile(`${API_PATH}/api.ts`, `}\n`, "utf8");

  for (let el of imports) {
    await fs.appendFile(
      `${API_PATH}/api.ts`,
      `import type {${el}} from "../src/interfaces/${el}";\n`,
      "utf8",
    );
  }

  console.log("API methods have been generated!");
};

generateApiFile();
