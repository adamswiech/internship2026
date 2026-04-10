import { readFile, writeFile, mkdir } from "node:fs/promises";
import fs from 'node:fs';
import path from "node:path";






export async function writeInterfaces(interfaces) {
    const baseDir = path.resolve(process.cwd(), "./src/interfaces");

    await mkdir(baseDir, { recursive: true });

    for (const iface of interfaces) {
        const filePath = path.join(baseDir, `${iface.name}.ts`);

        const importLines = iface.imports
            .filter((i) => i !== iface.name)
            .map((i) => `import type { ${i} } from "./${i}";`)
            .join("\n");

        const fileContent = `${importLines}\n\n${iface.content}\n`;

        await writeFile(filePath, fileContent, "utf8");

        console.log("Written:", filePath);
    }
}



export async function buildInterfaces(urlsFilePath) {
    const bases = await readBaseUrl(urlsFilePath);
    const base = bases.httpUrl ?? bases.httpsUrl;

    if (!base) {
        console.error("No base URL found");
        return null;
    }

    const swaggerUrl = `${base}/swagger/v1/swagger.json`;
    console.log("Fetching swagger.json from:", swaggerUrl);

    const swaggerJson = await getSwaggerJson(swaggerUrl);

    if (!swaggerJson || typeof swaggerJson !== "object") {
        console.error("Failed to fetch swagger.json from", swaggerUrl);
        return null;
    }

    const schemas = swaggerJson.components?.schemas;

    if (!schemas) {
        console.error("No schemas found in swagger.json");
        return null;
    }

    return Object.keys(schemas).map((key) => {
        const schema = schemas[key];
        const props = schema.properties || {};

        const imports = new Set();

        const propsString = Object.entries(props)
            .map(([propName, propSchema]) => {
                const tsType = mapSwaggerTypeToTS(propSchema, imports);
                if(typeof tsType === "object") {
                    return `${propName}: ${tsType.type} | null;`;
                }
                return `${propName}: ${tsType};`;
            })
            .join("\n    ");

        return {
            name: key,
            imports: Array.from(imports),
            content: `export interface ${key} {\n    ${propsString}\n}`,
        };
    });
}

// -----------------------------------------------------------------------------
// TYPE MAPPING
// -----------------------------------------------------------------------------

function mapSwaggerTypeToTS(propSchema, imports) {
    if (!propSchema) return "any";

    if (propSchema.$ref) {
        const refType = propSchema.$ref.split("/").pop();
        if (imports && refType) imports.add(refType);
        return propSchema.nullable ? `${refType} | null` : refType;
    }

    if (propSchema.type === "array") {
        const itemType = mapSwaggerTypeToTS(propSchema.items, imports);
        let typ = `${itemType}[]`;
        if (propSchema.nullable) typ += " | null";
        return typ;
    }

    let typ;

    switch (propSchema.type) {
        case "string":
            typ = "string";
            break;
        case "integer":
        case "number":
            typ = "number";
            break;
        case "boolean":
            typ = "boolean";
            break;
        case "object":
            if (propSchema.properties) {
                const entries = Object.entries(propSchema.properties)
                    .map(([key, val]) => `${key}: ${mapSwaggerTypeToTS(val, imports)}`);
                typ = `{ ${entries.join("; ")} }`;
            } else {
                typ = "Record<string, any>";
            }
            break;
        default:
            typ = "any";
    }
    if (propSchema.type === "string" && propSchema.format){
        switch (propSchema.format){
            case "date-time":
                typ = "Date";
                break;
        }
    }

    if (propSchema.nullable && !typ.includes("null")) {
        typ += " | null";
    }

    return typ;
}

// -----------------------------------------------------------------------------
// FETCH + FILE HELPERS
// -----------------------------------------------------------------------------
function stripQuotes(value) {
    return String(value || "")
        .trim()
        .replace(/^['"]|['"]$/g, "");
}


async function getSwaggerJson(url) {
    const envPath = stripQuotes(process.env.SWAGGER_LOCAL_FILE);
    const fallbackPath = path.resolve(process.cwd(), "../KSeF_app_fix.Server/swagger.json");
    const localPath = envPath || fallbackPath;

    if (!envPath) {
        console.warn("SWAGGER_LOCAL_FILE is not set. Using fallback:", fallbackPath);
    }

    try {
        if (localPath && fs.existsSync(localPath)) {
            const data = await readFile(localPath, "utf8");
            return JSON.parse(data);
        }
    } catch (err) {
        console.error("Local swagger.json failed, falling back to URL:", err);
    }


    try {
        const res = await fetch(url);

        if (!res.ok) {
            const body = await res.text();
            console.error("Fetch error:", res.status, res.statusText, body);
            return null;
        }

        return await res.json();
    } catch (err) {
        console.error("Error fetching swagger.json:", err);
        return null;
    }
}

async function readBaseUrl(urlsFilePath) {
    try {
        const data = await readFile(urlsFilePath, "utf-8");
        const json = JSON.parse(data);

        return {
            httpUrl: json.httpUrl ?? null,
            httpsUrl: json.httpsUrl ?? null,
        };
    } catch (err) {
        console.error("Failed to read or parse urls.json:", err);
        return {
            httpUrl: null,
            httpsUrl: null,
        };
    }
}