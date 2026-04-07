import fs from "node:fs/promises";
import "dotenv/config";
import {dictionaryType, dictionaryFormat} from "./dictionary";

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
const faktura = swaggerJsonContent.components.schemas.Faktura.properties;



// console.log(swaggerJsonContent.components.schemas.Faktura.properties); //this can render data about Faktura like type and format object
// console.log("Test: ", swaggerJsonContent.components.schemas.Faktura.properties.podmiot1["$ref"]); //works to see this part of url to podmiot1 obj
