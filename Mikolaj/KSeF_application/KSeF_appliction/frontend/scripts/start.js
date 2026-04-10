import { generateApiFile } from "./generateApi.js";
import { generateInterfaces } from "./generateInterfaces.js";

console.log("\x1b[32m> start.js started...\x1b[0m");

await generateInterfaces();
await generateApiFile();