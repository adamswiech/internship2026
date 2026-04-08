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

const getServersURLs = () => {
  //function to get from json from C# addresses of server
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
};

getServersURLs();

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

const generateInterfaces = () => {
  const schemasList = swaggerJsonContent.components.schemas; //this path here is always the same so can be hardcoded (in the latest versions of swagger)
  const keys = Object.keys(schemasList); //main keys - faktura, fawiersz, podmiot

  for (let i = 0; i < keys.length; i++) {
    //each iteration = new interface because of new keys[i] value

    const mainKey = keys[i]; //main key from main keys
    const propertiesObjects = schemasList[mainKey].properties;
    const objectsKeys = Object.keys(propertiesObjects);

    console.log(`\n${mainKey}\n`);

    for (let j = 0; j < objectsKeys.length; j++) {
      const name = objectsKeys[j];
      const type = dictionaryType[propertiesObjects[objectsKeys[j]].type];
      const format = dictionaryFormat[propertiesObjects[objectsKeys[j]].format];

      if (schemasList[mainKey].properties[name].items) {
        //action on items list
        const objRef = Object.values(
          schemasList[mainKey].properties[name].items,
        )[0];
        const objRefName = objRef.substring(objRef.lastIndexOf("/") + 1);
        console.log(`${name}: ${objRefName}`);
      } else if (propertiesObjects[objectsKeys[j]]["$ref"]) {
        //action on $ref
        const objRef = propertiesObjects[objectsKeys[j]]["$ref"];
        const objRefName = objRef.substring(objRef.lastIndexOf("/") + 1);
        console.log(`${name}: ${objRefName}`);
      } else {
        console.log(`${name}: ${type}, ${format}`);
      }
    }
  }
};

generateInterfaces();
