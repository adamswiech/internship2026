import fs from "node:fs/promises";
import "dotenv/config";

let apiAddressesList = { httpApiAddress: "", httpsApiAddress: "" };
const FILE_PATH = process.env.LAUNCH_SETTINGS_PATH;

const readConfig = async () => {
  const content = await fs.readFile(FILE_PATH, "utf-8");
  return JSON.parse(content);
};

const config = await readConfig();

const getServersURLs = () => {
  //function to get from json from C# addresses of server
  const profiles = config.profiles;
  const profilesLen = Object.keys(profiles).length;

  for (let i = 0; i < profilesLen; i++) {
    const profileName = Object.keys(profiles)[i];
    const urls = profiles[profileName].applicationUrl.split(";");

    apiAddressesList.httpApiAddress =
      urls.find((url) => url.startsWith("http:")) ?? "";
    apiAddressesList.httpsApiAddress =
      urls.find((url) => url.startsWith("https:")) ?? "";

    if (apiAddressesList.httpApiAddress != "") {
      console.log(`HTTP address: ${apiAddressesList.httpApiAddress}`);
    } else {
      console.log(`HTTPS address: ${apiAddressesList.httpsApiAddress}\n`);
    }
  }
};

const fetchSwaggerJson = async () => {
  try {
    const response = await fetch(`${apiAddressesList.httpsApiAddress}/swagger/v1/swagger.json`);

    if (!response.ok) {
      const error = await response.text();
      console.log(`Error: ${error}`);
      return null;
    }

    const result = await response.json();
    return result;
  } catch (error) {
    console.log(`Error: ${error.message}`);
    return null;
  }
};

getServersURLs();
export const accessSwaggerJsonContent = await fetchSwaggerJson();