import { generateApiFile } from "./generateApi.js";
import { generateInterfaces } from "./generateInterfaces.js";

console.log("\x1b[32m> start.js started...\x1b[0m");

await generateInterfaces();
await generateApiFile();


//IN EACH .env PATH CHANGE \ TO /
// LAUNCH_SETTINGS_PATH={PATH TO YOUR launchSettings.json FILE IN C# PROJECT}
// INTERFACES_PATH={PATH TO YOUR INTERFACES DIRECTORY (FOLDER)}
// API_PATH={PATH TO YOUR API FILE - api.ts}
//INSTAL DEPENDENCIES: npm install node-fetch@2.6.7 dotenv