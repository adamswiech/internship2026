import fs from "node:fs/promises";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";

const output = "./interface";

const mapSwagger = (key, value) => {
    const type = value?.type ?? value?.$ref ?? "unknown";
    console.log(`Key: ${key}, Type: ${type}`);
    if (value?.properties) {
        Object.keys(value.properties).forEach(subKey => mapSwagger(subKey, value.properties[subKey]))
    }
}
const loadSwaggerJSON = async () => {
    const swaggerContent = await fs.readFile("./swagger.json", "utf-8");
    return JSON.parse(swaggerContent);
};

try {
    const swaggerJSON = await loadSwaggerJSON();
    const schemas = swaggerJSON.components.schemas;

    let schemasV = Object.keys(schemas)
    console.log("Lista schemas:", Object.keys(schemas));
    //console.log ("v", schemasV[1])

    for (let i = 0; i < schemasV.length; i++) {

        console.log("STC", i)
        const schemaName = schemasV[i];
        const schemaValue = schemas[schemaName];
        mapSwagger(schemaName, schemaValue);
        
    }
    //Object.entries(schemas).forEach(([schemaName, schemaValue]) => {
    //    mapSwagger(schemaName, schemaValue);
    //});


} catch (error) {
    console.error("Nie udało się odczytać pliku swagger.json:", error.message);
}
