import { accessSwaggerJsonContent } from "./accessServer.js";
import fs from "node:fs/promises";
import { apiAddressesList } from "../scripts/accessServer.js";

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
      let hasRequestBody = false;
      let requestBodyProperties = null;
      let hasContent = false;
      let returnType = "";
      if (!operation || typeof operation !== "object") continue;

      if (
        operation.requestBody?.content?.["multipart/form-data"]?.schema
          ?.properties
      ) {
        const fileProps =
          operation.requestBody.content["multipart/form-data"].schema.properties
            .file;

        requestBodyProperties = {
          type: fileProps.type,
          format: fileProps.format,
        };
      }

      const successResponse =
        operation.responses?.["200"] || operation.responses?.["201"];
      if (successResponse?.content) {
        hasContent = true;

        const firstContentType = Object.keys(successResponse.content)[0];
        const schema = successResponse.content[firstContentType]?.schema;

        if (schema) {
            console.log("123"); //code here is executed 
            //ISSUE: where I even could find data type to return? 
            
          if (schema.type === "array" && schema.items?.$ref) {
            const ref = schema.items.$ref;
            returnType = ref.split("/").pop();
            console.log(ref.split("/").pop());
          } else if (schema.$ref) {
            returnType = schema.$ref.split("/").pop();
            console.log(schema.$ref.split("/").pop());
            
          } else if (schema.type) {
            returnType = schema.type;
            console.log(schema.type);
            

            if (schema.format) returnType += `(${schema.format})`; //CHECK THIS LINE
          }
        }
      }

      dataList.push({
        method: method.toUpperCase(),
        url: path,
        tags: Array.isArray(operation.tags) ? operation.tags : [],
        hasRequestBody: hasRequestBody,
        requestBodyProperties: requestBodyProperties,
        hasContent: hasContent,
        returnType: returnType || "any"
      });
    }
  }

  return dataList;
}; //whole function works

const apiEndpointsList = await fetchApiEndpointData();

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

    switch (endpoint.method) {
      case "GET":
        methodCode = `public static async ${endpointName}(): Promise<${apiEndpointsList[i].tags}[]> {
        const response = await fetch("${HTTPS_URL}${endpoint.url}");
        const jsonResponse: any[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status:" + response.status);
        }   
            
        return jsonResponse;        
      
      \n}\n`;
        break;

      case "POST":
        methodCode = `public static async ${endpointName}(file:${endpoint.returnType}): Promise<${apiEndpointsList[i].tags}[]> {
        const response = await fetch(
            "${HTTPS_URL}${endpoint.url}", 
            {
                method: "${endpoint.method.toUpperCase()}",
                body: file
            }
        );
        const jsonResponse: any[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status:" + response.status);
        }   

        

        return [];        
      
      \n}\n`;
        break;
    }

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

//SCHEMA FROM JSON ABOUT ENDPOINTS - SAME AS INTERFACES SCHEMA -> GET TYPE FOR METHOD IN CLASS FROM HERE
// "schema": {
//                 "type": "object",
//                 "properties": {
//                   "file": {
//                     "type": "string",
//                     "format": "binary"
//                   }

//EXAMPLE VIEW OF CLASS OF FIRST METHOD
// import type { Faktura } from "../src/interfaces/Faktura";
// export default class Api {
//   public static async getFaktura(): Promise<Faktura[]> {
//     const response = await fetch(
//       "https://server-ksef_appliction.dev.localhost:7459/api/Faktura/GetFaktury",
//     );
//     const jsonResponse: any[] = await response.json();

//     return jsonResponse.map(
//       (item: any): Faktura => ({
//         ...item,
//         p_1: new Date(item.p_1),
//         p_6_Od: new Date(item.p_6_Od),
//         p_6_Do: new Date(item.p_6_Do),
//         wiersze: item.wiersze,
//       }),
//     );
//   }
// }
