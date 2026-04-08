import { accessSwaggerJsonContent } from "./accessServer.js";
import fs from "node:fs/promises";
const API_PATH = process.env.API_PATH;

console.log("\x1b[32mgenerateApi.js\x1b[0m");

const fetchApiEndpoints = async () => {
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
        dataList.push({ method: method, url: path });
      }
    }
  }

  return dataList;
}; //whole function works

const apiEndpointsList = await fetchApiEndpoints();

const generateApiFile = async () => {
  await fs.appendFile(
    `${API_PATH}/api.ts`,
    `export default class Api {\n`,
    "utf8",
  );

  // Fix 1: Use for...of loop (sequential, awaits each)
  for (const endpoint of apiEndpointsList) {
    const endpointURL = endpoint.url.substring(
      endpoint.url.lastIndexOf("/") + 1,
    );

    const methodCode = `  public static async ${endpointURL}(): Promise<unknown> {
    return {};
  }\n`;

    await fs.appendFile(`${API_PATH}/api.ts`, methodCode, "utf8");
  }

  await fs.appendFile(`${API_PATH}/api.ts`, `}\n`, "utf8");
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
