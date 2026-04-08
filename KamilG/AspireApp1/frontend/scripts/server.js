console.log('fmfmsflsmf server...');
import fs from "node:fs/promises";
import "dotenv/config";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";


// Path to \AspireApp1.Server\Properties\launchSettings.json
const filepath = process.env.LAUNCH_SETTINGS_PATH;
console.log('filepath', filepath);

const readContent = async () => {
    const content = await fs.readFile(filepath, "utf-8");
    return JSON.parse(content);
};



// Getting API Address
let httpAddress = "";
let httpsAddress = "";
const rC = await readContent();
const profiles = rC.profiles;
const profileslength = Object.keys(profiles).length;
for (let i = 0; i < profileslength; i++) {
    const profileName = Object.keys(profiles)[i];
    const url = profiles[profileName].applicationUrl.split(";");
    httpAddress = url.find((url) => url.startsWith("http://"));
    httpsAddress = url.find((url) => url.startsWith("https://"));

    console.log(`Profile: ${profileName}`);
    console.log(`HTTP Address: ${httpAddress}`);
    console.log(`HTTPS Address: ${httpsAddress}`);
}

const fetchSwaggerJSON = async () => {
    try {
        const call = await fetch(`${httpsAddress}/swagger/v1/swagger.json`);
        if (!call.ok) {
            const error = await call.text();
            return error
        }
        const result = await call.json();
        return result;

    }
    catch (error) {
        return error.message;
    }
}
const swaggerJSON = await fetchSwaggerJSON();
console.log(swaggerJSON);

let interfacesList = [];
const schemasList = swaggerJSON.components.schemas;
let interafacesList = [];

//const mapSwagger = (key, value) => {
//    console.log(`key: ${key}, value ${value.type ?? value.$ref}`)
//    const type = value.type ?? value.$ref;
//    interfacesList.push({ key, type });
//}
//console.log(mapSwagger(key, value))
