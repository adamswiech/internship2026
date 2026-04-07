import fs from "node:fs/promises";
import "dotenv/config";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";

let httpApiAddress = "";
let httpsApiAddress = "";
const filepath = process.env.LAUNCH_SETTINGS_PATH;

const readConfig = async () => {
  const content = await fs.readFile(filepath, "utf-8");
  return JSON.parse(content);
};

const config = await readConfig();
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

let interfacesList = [];

const mapSwaggerJson = (key, value) => {
  // console.log(`key: ${key}, value: ${value.type ?? value.$ref}`); //works
  const type = value.type ?? value.$ref;
  interfacesList.push({ key, type });
};

const printSwaggerSchema = (objKey, header) => {
//   console.log(`---${header}---`); //works 
  const objKeys = Object.keys(objKey); //.keys method get just keys of given key: value data struct here it works on Object

  for (let i = 0; i < objKeys.length; i++) {
    const key = objKeys[i];
    const value = objKey[key];

    mapSwaggerJson(key, value);
  }
};

const prepareSwaggerSchemas = () => {
  let i = 0;
  for (const schema of Object.values(schemasList)) {
    //Object.values(schemasList) -> it converts json object with all schemas (Faktura, Podmiot, FaWiersz) to array of objects (schemas)
    if (!schema.properties) continue;

    printSwaggerSchema(schema.properties, Object.keys(schemasList)[i]); //here you can get keys of Object it's key: value -> key is header and value is json object
    console.log("\n");
    i++;
  }
};

prepareSwaggerSchemas();

interfacesList.forEach((element) => {
  console.log(element);
});
