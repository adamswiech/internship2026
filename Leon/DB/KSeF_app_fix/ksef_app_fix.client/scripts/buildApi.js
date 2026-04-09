import { readFile, writeFile, mkdir} from "node:fs/promises";
import fs from 'node:fs';
import path from "node:path";
import { json } from "node:stream/consumers";
import dotenv from "dotenv";
import { env } from "node:process";
dotenv.config();

export async function buildApi(urlsFilePath){
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

    const paths = swaggerJson.paths;

    if (!paths) {
        console.error("No paths found in swagger.json");
        return null;
    }


    

    const Endpoints = await getEndpoints(paths);


    

    const apiCodes = await genApi(Endpoints, base);
    await writeApi(apiCodes);



}




async function writeApi(apiCodes){
    const baseDir = path.resolve(process.cwd(), "./src/api");

    await mkdir(baseDir, { recursive: true });

    for (const [tag, code] of Object.entries(apiCodes)) {
        const filePath = path.join(baseDir, `${tag}.ts`)
        await writeFile(filePath, code, "utf-8")
        console.log(`Written ${tag} API\n`)
    }

}


async function genApi(endpoints, baseUrl) {
    const groups = {};
    let apiCodes = {};
    

    for (const ep of endpoints) {
        if (!groups[ep.tag]){ 
            groups[ep.tag] = []; 
            apiCodes[ep.tag] = "";
        }
        groups[ep.tag].push(ep);
    }
    

    for (const [tag, items] of Object.entries(groups)) {
        const imports = new Set();
        let importsS = "";


        apiCodes[tag] += `export const ${tag}Con = {\n`;

        for (const ep of items) {
            const { type: paramsType, usage: paramsUsage } = await getParams(ep.params);
            const { type: bodyType, usage: bodyUsage, contentType } = await getRequestBody(ep.requestBody, imports);
            const fnName = await genName(ep.path);
            const responseType = await getResType(ep.responses, imports);
            const args = [paramsType, bodyType].filter(Boolean).join(", ");

            let url = ep.path.replace(/{(\w+)}/g, (_, key) => `\${${key}}`);

            apiCodes[tag] += `
            ${fnName}: async (${args}): Promise<${responseType}> => {
                try {
                    const response = await fetch(\`${baseUrl}${url}\`, {
                        method: "${ep.method}",
                        ${bodyUsage ? `
                        body: ${contentType === "application/json" ? "JSON.stringify(body)" : "formData"},
                        ` : ""}
                    });

                    if (!response.ok)
                        throw new Error(\`Network response was not ok: \${response.status}\`);

                    return await response.json() as ${responseType};
                } catch (error) {
                    console.error("Error calling ${fnName}:", error);
                    throw error;
                }
            },\n`;
        }

        apiCodes[tag] += `};\n\n`;

        for (const ref of imports) {
            importsS += `import type { ${ref} } from "../interfaces/${ref}.ts";\n`;
        }
        apiCodes[tag] = importsS + apiCodes[tag];
    }

    return apiCodes;

}

async function getParams(params = []) {
    if (!params.length) {
        return {
            type: "",
            usage: ""
        };
    }

    const args = params.map(p => {
        const name = p.name;
        const schema = p.schema || {};
        const tsType = csTypeToTS(schema);
        const optional = !p.required;

        return `${name}${optional ? "?" : ""}: ${tsType}`;
    });

    const usage = params.map(p => p.name).join(", ");

    return {
        type: `${args.join("; ")}`,
        usage
    };
}
async function getRequestBody(requestBody, imports) {
    if (!requestBody) {
        return {
            type: "",
            usage: "",
            contentType: null
        };
    }

    const content = requestBody.content || {};


    if (content["application/json"]) {
        const schema = content["application/json"].schema;

        return {
            type: `body: ${csTypeToTS(schema, imports).split(":")[0].split("}")[1]}`,
            usage: "body",
            contentType: "application/json"
        };
    }

    if (content["multipart/form-data"]) {
        const schema = content["multipart/form-data"].schema;

        return {
            type: `formData: FormData`,
            usage: "formData",
            contentType: "multipart/form-data"
        };
    }

    return {
        type: "",
        usage: "",
        contentType: null
    };
}




async function getResType(responses, imports) {
    const OK = responses["200"] || responses["201"] || responses["202"] || responses["204"];
    if (!OK) return "any";

    const jsonContent = OK.content?.["application/json"];
    if (!jsonContent) return "any";

    return csTypeToTS(jsonContent.schema, imports)

}

async function genName(path) {
    const parts = path.split("/").filter(Boolean);
    return parts
        .map((p, i) => i === 0 ? p.toLowerCase() : p[0].toUpperCase() + p.slice(1))
        .join("")
        .split("=")[0];

}







async function getEndpoints(paths) {
    return Object.entries(paths).flatMap(([pathKey, methods]) =>
        Object.entries(methods).map(([methodKey, details]) => ({
            path: pathKey,
            method: methodKey.toUpperCase(),
            tag: details.tags?.[0] ?? "DefTag",
            params: details.parameters ?? [],
            requestBody: details.requestBody,
            responses: details.responses
        }))
    );
}

function csTypeToTS(schema, imports) {
    if (!schema) return "any";

    if (schema.$ref) {
        const ref = schema.$ref.split("/").pop();
        if (imports) imports.add(ref);
        return schema.nullable ? `${ref} | null` : ref;
    }

    if (schema.type === "array") {
        const itemType = csTypeToTS(schema.items, imports);
        const arr = `${itemType}[]`;
        return schema.nullable ? `${arr} | null` : arr;
    }

    if (schema.type === "object") {
        const props = schema.properties || {};
        const entries = Object.entries(props).map(([key, val]) => {
            const optional = val.nullable || !(schema.required || []).includes(key);
            return `${key}${optional ? "?" : ""}: ${csTypeToTS(val, imports)}`;
        });

   
        if (schema.additionalProperties === true) {
            entries.push(`[key: string]: any`);
        }


        if (schema.additionalProperties && schema.additionalProperties !== true) {
            const apType = csTypeToTS(schema.additionalProperties, imports);
            entries.push(`[key: string]: ${apType}`);
        }

        const obj = `{ ${entries.join("; ")} }`;
        return schema.nullable ? `${obj} | null` : obj;
    }

    let typ = "any";

    switch (schema.type) {
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
    }


    if (schema.type === "string" && schema.format === "date-time") {
        typ = "Date";
    }

    return schema.nullable ? `${typ} | null` : typ;
}

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