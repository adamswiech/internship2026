import { generateApiFile } from "./generateApi.js";
import { generateInterfaces } from "./generateInterfaces.js";

await generateInterfaces();
await generateApiFile();