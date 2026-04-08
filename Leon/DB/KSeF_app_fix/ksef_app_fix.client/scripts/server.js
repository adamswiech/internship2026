import { readFile, writeFile, mkdir } from "node:fs/promises";
import { fileURLToPath } from "node:url";
import path from "node:path";
import dotenv from "dotenv";
import { writeUrls } from "./urlHandler.js";
import { writeInterfaces, buildInterfaces } from "./interfaceBuilder.js";
import { buildApi } from "./buildApi.js";

dotenv.config();
const __dirname = path.dirname(fileURLToPath(import.meta.url));
const urlsFilePath = path.join(__dirname, "urls.json");


await main();
    
async function main() {
    await writeUrls(urlsFilePath);

    const interfaces = await buildInterfaces(urlsFilePath);

    await writeInterfaces(interfaces);
    await buildApi(urlsFilePath);
}
