import { readFile } from 'node:fs/promises';   // ESM (recommended)
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import {dictionaryType, dictionaryFormat} from './dictionary.js';
import fs from 'node:fs/promises';
import { backup } from 'node:sqlite';
console.log("---------------------------------------------------------------");
console.log("run scipt");

let httpApiBase = "";
let httpsApiBase = "";
let models = [];

//sets apiBaseUrl
await ( async () =>{
    const __filename = fileURLToPath(import.meta.url);
    const __dirname = dirname(__filename);

    const apiSettingsPath = join(__dirname, '../../Ksef_API_ASP.Net/Properties/launchSettings.json');
    
    async function loadJSON(filname) {
      try {
        const data = await fs.readFile(filname, 'utf8');
        return JSON.parse(data);
        
      } catch (error) {

        console.error('Error reading JSON file:', error.message);
        throw error;
      }
    }
    
    async function getApiUrl() {
      await loadJSON(apiSettingsPath).then((json)=>{

        httpApiBase = json.profiles.http.applicationUrl;
        httpsApiBase = json.profiles.https.applicationUrl;
    })
    }
    
    await getApiUrl();
})()

//setting models
await (async ()=>{
        const swaggerLink = `${httpApiBase}/swagger/v1/swagger.json`;
        const swaggerRespons = await fetch(swaggerLink);
        const swaggerData = await swaggerRespons.json();
        const shemas = swaggerData.components.schemas;
        for(let i in shemas)
            models.push(shemas[i]);
})();

const GetType = (model)=>{
    let type = "object";
    if(model["type"])
        type = dictionaryType[model["type"]];

    if(model["Format"])
        type = dictionaryFormat[model["format"]];

    if(model["items"])
        type = GetType(model["items"]) + "[]";

    if(model["$ref"])
        type = "object";
    console.log(model);
    return type;
};


for(let i of models)
{
    for(let fieldName in i.properties)
        console.log(GetType(i.properties[fieldName]));
    console.log("");
}






/*
export interface Name{
	FieldName : type[],
	Iterator: Type()
    }
    Type(){
        typ = dictionaryType[type]
        
        if(Format.exist)
		typ = dictionaryFormat[Format]
        
        if(Items.exist)
		mod = "[]"
		typ = Type(Item)
		
        return type + mod
        }
        
        profiles: {
            http: {
                commandName: 'Project',
    environmentVariables: [Object],
    dotnetRunMessages: true,
    applicationUrl: 'http://localhost:5058'
    },
    https: {
    commandName: 'Project',
    environmentVariables: [Object],
    dotnetRunMessages: true,
    applicationUrl: 'https://localhost:7173;http://localhost:5058'
    },
    'Container (Dockerfile)': {
        commandName: 'Docker',
        launchUrl: '{Scheme}://{ServiceHost}:{ServicePort}',
        environmentVariables: [Object],
    publishAllPorts: true,
    useSSL: true
    }
    },

    
    */
   console.log("---------------------------------------------------------------");