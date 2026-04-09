import 'dotenv/config';              
import dotenv from 'dotenv';
import fs from 'node:fs/promises';
import {dictionaryType, dictionaryFormat} from './dictionary.js';
import path from 'node:path'
import { resolve, dirname } from 'path'
import { fileURLToPath } from 'url'
import { loadJSON } from './loadJSON.js'

console.log("---------------------------------------------------------------");

//accesing external .env
const __dirname = dirname(fileURLToPath(import.meta.url))
dotenv.config({ path: resolve(__dirname, '../../.env') });

let schemas = {}; 
const projectPath = process.env.PROJCETPATH;

async function deleteAllFilesInFolder(folderPath) {
    async function folderExists(path) {
        try {
            await fs.access(path);
            return true;
        } catch {
            return false;
        }
    }
    try {        
        if(!folderExists(folderPath)) return false;

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
        // console.error('Error deleting files:', err);
    }
}


// Load Swagger/OpenAPI data
async function loadSwaggerData() {
    const swaggerDataPathRelational = process.env.swaggerDataPath;

    const swaggerDataPath = projectPath + swaggerDataPathRelational;

    const swaggerData = await loadJSON(swaggerDataPath);

    schemas = swaggerData.components?.schemas || {};
}

const createInterfaces = () =>{
    for(let interfaceName in schemas){
        const i = schemas[interfaceName]; // interface as object
        const improtList = [];
        const fields = [];

        const GetType = (model)=>{
            let type = "any";

            if(model["type"])
                type = dictionaryType[model["type"]];

            if(model["format"]){
                type = dictionaryFormat[model["format"]];
            }

            if(model["$ref"]){
                const objectName = model["$ref"].split('/').pop(); 
                type = objectName;
                if(!improtList.find((e)=> e === objectName))
                    improtList.push(objectName);
            }

            if(model["items"])
                type = GetType(model["items"]) + "[]";


            return type;
        };

        //filling fields array -> generating types
        for(let fieldName in i.properties){
            const properties = i.properties;
            //filling nulable fields because nulable fields are not filled properly
            if(i.required){
                for(let required of i.required){
                    properties[required].nullable = false;
                }
            }
            const isNulable = (model) => (model["nullable"])? '?': '';  

            fields.push(fieldName + isNulable(properties[fieldName]) + ": " + GetType(properties[fieldName]) + ",");
        }
        
        const importString = improtList.reduce((acc,curr) => acc + `import type { ${curr} } from "./${curr}"; \n`,"");
        const fieldString = fields.reduce((acc,curr) =>acc +"\n\t" +  curr, "");
        const fileString = `${importString}
export interface ${interfaceName}{${fieldString}
}`;

        async function createAndWriteFile() {
            
            const filePath = projectPath + process.env.DestinationInterfacePath + '/' + interfaceName + '.ts';
            
            try {
                const dir = path.dirname(filePath);
                await fs.mkdir(dir, { recursive: true });

                const existing = await fs.stat(filePath).catch(() => null);
                if (existing?.isDirectory()) {
                throw new Error(`Expected a file path but got a directory: ${filePath}`);
                }
                
                await fs.writeFile(filePath, fileString, 'utf8');

                console.log('File created successfully at:', filePath);
            } catch (err) {
                console.error('Error:', err);
            }
        }
        createAndWriteFile();
    }
};

deleteAllFilesInFolder(projectPath + process.env.DestinationInterfacePath)
.then(() =>
    loadSwaggerData()
        .then(() =>createInterfaces()))
