import { readFile } from 'node:fs/promises';   // ESM (recommended)
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import {dictionaryType, dictionaryFormat} from './dictionary.js';
import fs from 'node:fs/promises';
import path  from 'node:path';
console.log("---------------------------------------------------------------");
console.log("run scipt");

let httpApiBase = "";
let httpsApiBase = "";
let shemas = {};

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
    const shemasPv = swaggerData.components.schemas;
    
    shemas = shemasPv;
})();

const createModels = () =>{
    for(let interfaceName in shemas){
        const i = shemas[interfaceName];
        const improtList = [];
        const fields = [];

        const GetType = (model)=>{
            let type = "object";
            if(model["type"])
                type = dictionaryType[model["type"]];
        
            if(model["format"]){
                type = dictionaryFormat[model["format"]];
            }
        
            if(model["items"])
                type = GetType(model["items"]) + "[]";
        
            if(model["$ref"]){
                const objectName = model["$ref"].split('/').pop(); 
                type = objectName;
                if(!improtList.find((e)=> e === objectName))
                    improtList.push(objectName);
            }
            return type;
        };

        for(let fieldName in i.properties)
            fields.push(fieldName + ": " + GetType(i.properties[fieldName]) + ",");

        const importString = improtList.reduce((acc,curr) => acc + `import { ${curr} } from "./${curr}"; \n`,"");
        const fieldString = fields.reduce((acc,curr) =>acc +"\n\t" +  curr, "");
        const fileString = `${importString}
export interface ${interfaceName}{${fieldString}
}`;
        async function createAndWriteFile() {
            
            const filePath = path.join(           
                'src',
                'Models',
                'API',
                `${interfaceName}.ts`
            );
            
            try {

                const dir = path.dirname(filePath);
                await fs.mkdir(dir, { recursive: true });

                await fs.writeFile(filePath, fileString, 'utf8');

                console.log('File created successfully at:', filePath);
            } catch (err) {
                console.error('Error:', err);
            }
        }

        createAndWriteFile();
    }
};

async function deleteAllFilesInFolder(folderPath) {
try {
  
    const items = await fs.readdir(folderPath);

    for (const item of items) {
        const itemPath = path.join(folderPath, item);
        const stats = await fs.stat(itemPath);

        if (stats.isFile()) {
            await fs.unlink(itemPath);        
            console.log(`Deleted file: ${item}`);
        }
    }

    console.log('All files deleted successfully!');
    } catch (err) {
        console.error('Error deleting files:', err);
    }
}

deleteAllFilesInFolder(path.join(  'src','Models','API',)).then(()=> createModels());







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
		
        return type + modasdasdasdasdasdasdsad
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