import fs from "node:fs/promises";

console.log("--------------------------------------------------------------");
console.log("Starting script...");

let httpApiAddress = "";
let httpsApiAddress = "";

const filepath = "C:/Users/Vulcan/source/repos/internship2026/Mikolaj/KSeF_application/KSeF_appliction/KSeF_appliction.Server/Properties/launchSettings.json";

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
    console.log("HTTP address: ", httpApiAddress);
  } else {
    console.log("HTTPS address: ", httpsApiAddress);
  }
}

/*
GETTING CURRENTLY SERVER ADDRESS [DONE]
1. you have to open launchSettings.json [DONE]
2. find there https -> applicationUrl and get this to js [DONE]
3. assign to varriable - that is your servers address [DONE]
--------------------------------------------------------------
SWAGGER JSON MAP
1. fetch json from swagger api endpoint
2. map json
3. based on mapped json delete everything from interfaces folder and re-generate all files (interfaces)


vite will start react app and everything will be working fine
*/

console.log("Script has ended");
console.log("--------------------------------------------------------------");
