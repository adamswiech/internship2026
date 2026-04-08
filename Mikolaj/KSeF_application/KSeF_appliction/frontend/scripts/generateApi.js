import { accessSwaggerJsonContent } from "./accessServer.js";
import fs from "node:fs/promises";
const API_PATH = process.env.API_PATH;
console.log("\x1b[32mgenerateApi.js\x1b[0m");

const deleteAllDataFromApi= async () => {
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
      if (operation && typeof operation === "object") {
        const tags = Array.isArray(operation.tags) ? operation.tags : [];
        dataList.push({
          method: method,
          url: path,
          tags: tags,
        });
      }
    }
  }

  dataList.forEach((el) => {
    console.log(
      `{url: ${el.url}, method: ${el.method}, data-type: ${el.tags}}`,
    );
  });

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

    methodCode = `public static async ${endpointName}(): Promise<${apiEndpointsList[i].tags}[]> {
      return [];
    }\n`;

    if (!imports.includes(endpoint.tags)) {
      console.log(endpoint.tags); //here's problem with duplicates of types to api.ts file (many imports of the same interface)
      imports.push(endpoint.tags);
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
