import { readFile, writeFile, mkdir } from "node:fs/promises";
import { fileURLToPath } from "node:url";
import path from "node:path";
import dotenv from "dotenv";

dotenv.config();

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const urlsFilePath = path.join(__dirname, "urls.json");

await main();

async function main() {
    await writeUrls();

    const interfaces = await buildInterfaces();
    console.log("interfaces:", interfaces);

    await writeInterfaces(interfaces);
}

// -----------------------------------------------------------------------------
// URL HANDLING
// -----------------------------------------------------------------------------

async function writeUrls() {
    const envPath = stripQuotes(process.env.LAUNCH_SETTINGS_PATH);
    const fallbackPath = path.resolve(
        process.cwd(),
        "../KSeF_app_fix.Server/Properties/launchSettings.json"
    );

    const launchSettingsPath = envPath || fallbackPath;

    if (!envPath) {
        console.warn(
            "LAUNCH_SETTINGS_PATH is not set. Using fallback:",
            fallbackPath
        );
    }

    if (!launchSettingsPath) {
        throw new Error("No launchSettings path available");
    }

    let data;
    try {
        data = await readFile(launchSettingsPath, "utf8");
    } catch (err) {
        console.error(
            "Failed to read launchSettings.json at",
            launchSettingsPath,
            err
        );
        throw err;
    }

    let json;
    try {
        json = JSON.parse(data);
    } catch (err) {
        console.error("Failed to parse launchSettings.json:", err);
        throw err;
    }

    const profiles = json.profiles || {};

    const httpUrl =
        profiles.http?.applicationUrl?.split(";")[0] ?? null;

    const httpsUrl =
        (profiles.https?.applicationUrl ?? "")
            .split(";")
            .find((url) => url.startsWith("https://")) || null;

    const urlsToWrite = { httpUrl, httpsUrl };

    try {
        await writeFile(
            urlsFilePath,
            JSON.stringify(urlsToWrite, null, 2),
            "utf8"
        );
        console.log("URLs written to file successfully at", urlsFilePath);
    } catch (err) {
        console.error("Error writing urls.json:", err);
        throw err;
    }

    console.log("httpUrl:", httpUrl);
    console.log("httpsUrl:", httpsUrl);
}

function stripQuotes(value) {
    return String(value || "")
        .trim()
        .replace(/^['"]|['"]$/g, "");
}

// -----------------------------------------------------------------------------
// INTERFACE GENERATION
// -----------------------------------------------------------------------------

async function buildInterfaces() {
    const bases = await readBaseUrl();
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
                    return `${propName}?: ${tsType.type} | null;`;
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

async function writeInterfaces(interfaces) {
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

// -----------------------------------------------------------------------------
// TYPE MAPPING
// -----------------------------------------------------------------------------

function mapSwaggerTypeToTS(propSchema, imports) {
    if (!propSchema) return "any";

    if (propSchema.$ref) {
        const refType = propSchema.$ref.split("/").pop();

        if (imports && refType) {
            imports.add(refType);
        }

        return refType;
    }

    if (propSchema.type === "array") {
        const itemType = mapSwaggerTypeToTS(
            propSchema.items,
            imports
        );
        return `${itemType}[]`;
    }
    if (propSchema.nullable === true) {
        switch (propSchema.type) {
        case "string":
            return { type: "string", nullable: true };
        case "integer":
        case "number":
            return { type: "number", nullable: true };
        case "boolean":
            return { type: "boolean", nullable: true };
        case "object":
            return { type: "Record<string, any>", nullable: true };
        default:
            return {type: "any", nullable: true};
    }
    }

    switch (propSchema.type) {
        case "string":
            return "string";
        case "integer":
        case "number":
            return "number";
        case "boolean":
            return "boolean";
        case "object":
            return "Record<string, any>";
        default:
            return "any";
    }
}

// -----------------------------------------------------------------------------
// FETCH + FILE HELPERS
// -----------------------------------------------------------------------------

async function getSwaggerJson(url) {
    try {
        const res = await fetch(url);

        if (!res.ok) {
            const body = await res.text();
            console.error(
                "Fetch error:",
                res.status,
                res.statusText,
                body
            );
            return null;
        }

        return await res.json();
    } catch (err) {
        console.error("Error fetching swagger.json:", err);
        return null;
    }
}

async function readBaseUrl() {
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