import fs from "node:fs/promises";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";

const output = "./interface";

const mapSwagger = (key, value, path = key) => {
    const tsType = mapSchemaType(value);
    console.log(`Path: ${path}, Type: ${tsType}`);

    // schodzimy rekurencyjnie po właściwościach obiektu
    if (value?.properties) {
        Object.entries(value.properties).forEach(([subKey, subValue]) =>
            mapSwagger(subKey, subValue, `${path}.${subKey}`)
        );
    }

    // obsługa tablic – w logach będzie widać typ elementów
    if (value?.items && value.type === "array") {
        mapSwagger(`${key}[]`, value.items, `${path}[]`);
    }
};
const loadSwaggerJSON = async () => {
    const swaggerContent = await fs.readFile("./swagger.json", "utf-8");
    return JSON.parse(swaggerContent);
};

const resolveRef = ref => {
    const parts = ref.split("/");
    return parts[parts.length - 1];
};
const mapSchemaType = schema => {
    if (!schema) {
        return "any";
    }

    const { type, items, format } = schema;

    if (schema.$ref) {
        console.log("ref");
        return resolveRef(schema.$ref);
    }

    if (type === "array" && items) {
        const itemType = mapSchemaType(items);
        return `${itemType}[]`;
    }

    const baseType = dictionaryType[type] ?? "any";

    if (format && dictionaryFormat[format]) {
        return dictionaryFormat[format];
    }

    return baseType;
};

try {
    const swaggerJSON = await loadSwaggerJSON();
    const schemas = swaggerJSON.components.schemas;

    const schemasV = Object.keys(schemas);
    console.log("Lista schemas:", schemasV);

    for (let i = 0; i < schemasV.length; i++) {
        console.log("STC", i);
        const schemaName = schemasV[i];
        const schemaValue = schemas[schemaName];
        mapSwagger(schemaName, schemaValue);
    }
} catch (error) {
    console.error("Nie udało się odczytać pliku swagger.json:", error.message);
}