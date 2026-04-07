import fs from "node:fs/promises";
import "dotenv/config";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";

let httpApiAddress = "";
let httpsApiAddress = "";
const FILE_PATH = process.env.LAUNCH_SETTINGS_PATH;
const INTERFACES_PATH = process.env.INTERFACES_PATH;

const readConfig = async () => {
  const content = await fs.readFile(FILE_PATH, "utf-8");
  return JSON.parse(content);
};

const config = await readConfig();
const deleteAllInterfaces = async () => {
  try {
    await fs.unlink(`${INTERFACES_PATH}/Podmiot.ts`);
    await fs.unlink(`${INTERFACES_PATH}/FaWiersz.ts`);
    await fs.unlink(`${INTERFACES_PATH}/Faktura.ts`);
  } catch {
    console.log("No interfaces to delete");
  }
};

await deleteAllInterfaces();
const profiles = config.profiles;
const profilesLen = Object.keys(profiles).length;

for (let i = 0; i < profilesLen; i++) {
  const profileName = Object.keys(profiles)[i];
  const urls = profiles[profileName].applicationUrl.split(";");

  httpApiAddress = urls.find((url) => url.startsWith("http:")) ?? "";
  httpsApiAddress = urls.find((url) => url.startsWith("https:")) ?? "";

  if (httpApiAddress != "") {
    console.log(`\nHTTP address: ${httpApiAddress}`);
  } else {
    console.log(`HTTPS address: ${httpsApiAddress}\n`);
  }
}

const fetchSwaggerJson = async () => {
  try {
    const response = await fetch(`${httpsApiAddress}/swagger/v1/swagger.json`);

    if (!response.ok) {
      const error = await response.text();
      return error;
    }

    const result = await response.json();
    return result;
  } catch (error) {
    return error.message;
  }
};

const swaggerJsonContent = await fetchSwaggerJson();
const schemasList = swaggerJsonContent.components.schemas; //this path here is always the same so can be hardcoded (in the latest versions of swagger)
// const keys = Object.keys(schemasList);

// const x = JSON.stringify(schemasList[keys[i]].properties, null, 2);

// for (let i = 0; i < x.length; i++) {
//   console.log(x[i]);
// } HERE CONTINUE TOMORROW

/*
1. 







*/
// IMPORTANT
// wiersze: {
//     type: 'array',
//     items: { '$ref': '#/components/schemas/FaWiersz' },
//     nullable: true
//   }
