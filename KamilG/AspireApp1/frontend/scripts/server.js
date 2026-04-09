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


const downloadSwagger = async () => {
    try {
        const data = await fetch(`${httpsAddress}/swagger/v1/swagger.json`);
        if (!data.ok) {
            throw new Error(`HTTP ${data.status} - ${await data.text()}`);
        }
        const dataText = await data.text();
        const outputPath = "./swagger.json";
        await fs.writeFile(outputPath, dataText, "utf-8");
        console.log("plik saved");
        return outputPath;

    }
    catch (error) {
        console.error("Error fetching Swagger JSON:", error.message);
        return null;
    }


}

//const fetchSwaggerJSON = async () => {
//    try {
//        const call = await fetch(`${httpsAddress}/swagger/v1/swagger.json`);
//        if (!call.ok) {
//            const error = await call.text();
//            return error
//        }
//        const result = await call.json();
//        return result;

//    }
//    catch (error) {
//        return error.message;
//    }
//}
//const swaggerJSON = await fetchSwaggerJSON();

const swaggerFilePath = await downloadSwagger();
if (!swaggerFilePath) {
    throw new Error("Nie udało się pobrać pliku swagger.json");
}

//const swaggerContent = await fs.readFile(swaggerFilePath, "utf-8");
//const swaggerJSON = JSON.parse(swaggerContent);
//console.log(swaggerJSON);





//const mapSwagger = (key, value) => {
//    const type = value?.type ?? value?.$ref ?? "unknown";
//    console.log(`Key: ${key}, Type: ${type}`);
//    if (value?.properties) {
//        Object.keys(value.properties).forEach(subKey => mapSwagger(subKey, value.properties[subKey]))
//    }
//}


//const schemas = swaggerJSON.components?.schemas;
//if (schemas) {
//    Object.keys(schemas).forEach(schemaName => {
//        mapSwagger(schemaName, schemas[schemaName]);
//    })
//}
//const faWiersz = swaggerJSON.components?.schemas?.faWiersz;
//if (faWiersz) {
//    mapSwagger('faWiersz', faWiersz);
//}
