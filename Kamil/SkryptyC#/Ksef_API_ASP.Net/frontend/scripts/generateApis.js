import 'dotenv/config';              
import dotenv from 'dotenv';
import fs from 'node:fs/promises';
import {dictionaryType, dictionaryFormat} from './dictionary.js';
import path from 'node:path'
import { resolve, dirname } from 'path'
import { fileURLToPath } from 'url'
import { loadJSON } from './loadJSON.js';

const __dirname = dirname(fileURLToPath(import.meta.url))
dotenv.config({ path: resolve(__dirname, '../../.env') });

const projectPath = process.env.PROJCETPATH;
let paths = {};

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
    }

    if(model["items"])
        type = GetType(model["items"]) + "[]";

    return type;
};

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

    paths = swaggerData.paths;
}

loadSwaggerData()
.then(() => deleteAllFilesInFolder(projectPath + process.env.DestinationApiePath)
.then(() =>{
    const controllers = {};
    for(let endpoint in paths)
    {
        let tag = "";
        let method
        const endpointName = endpoint.split('/').pop().toString();

        //geting method
        //assuming that there is only one method
        //getting tag(controller) name
        for(let methodI in paths[endpoint]){
            method = methodI;
            tag = paths[endpoint][method].tags[0];
        }
        
        //adding controller
        controllers[tag] = (controllers[tag])? controllers[tag]: {};
        
        //adding endpoint to controller and setting method
        controllers[tag][endpointName] = {
            method : method
        }

        //adding input parameters or body
        switch(method)
        {
            case "get":
                controllers[tag][endpointName]["parameters"] = [];

                if(!paths[endpoint][method].parameters) break;

                for(let i of paths[endpoint][method].parameters)
                    controllers[tag][endpointName]["parameters"].push({
                        name: i.name,
                        type: GetType(i.schema)
                })

            break;
            case "post":
                // if(paths[endpoint][method].requestBody)
                controllers[tag][endpointName]["requestBodyType"] = GetType(paths[endpoint][method].requestBody.content["application/json"].schema);

            break;
        }

        //adding return type
        for(let code in paths[endpoint][method].responses)
        {
            const codeObject = paths[endpoint][method].responses[code];
            if(codeObject["content"])
                controllers[tag][endpointName]["responsType"] =  GetType(codeObject["content"]["text/json"].schema);
        }
        
    }

    for(let controllerName in controllers)
    {
        const controller = controllers[controllerName];
        let methodsString = "";
        //setting methods string
        for(let methodName in controller){
            const method = controller[methodName];
            let methodString = "";

            //setting method string
            switch(controller[methodName].method)
            {
                case "get":
                const paramsString = method.parameters.reduce((acc,curr) => (acc === '')?acc + curr.name + ": " + curr.type: acc + ', ' + curr.name + ": " + curr.type, '');
                const fetchParamString = method.parameters.reduce((acc,curr) => (acc === '')?acc + curr.name + "${" + curr.name + "}" : acc + '&&'+curr.name + "${" + curr.name + "}" , '?');
                methodString = `
    ${methodName}: async (${paramsString}) : Promise<${method.responsType}> => {
        try {
            const response  = await fetch(\`https://`+"${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}" +`/${controllerName}/${methodName}${fetchParamString}\`);

            if (!response.ok) 
                throw new Error(\`Network response was not ok: ` + "${response.status}" + `\`);

            return await response.json() as ${method.responsType};
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, `;
                break;
                case "post":
                methodString = `
    ${methodName}: async (data: ${method.requestBodyType}): Promise<${method.responsType}> => {
        try {
        const response = await fetch(\`https://`+"${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}" +`/${controllerName}/${methodName}\`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) 
            throw new Error(\`Network response was not ok: ` + "${response.status}" + `\`);

        return await response.json() as ${method.responsType};

        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }
    },`
                break;
            }
            methodsString += methodString;
        }
        const importOb = {};
        const importList = [];
        for(let methodName in controller){
            const FormatDataType = (e) => (e.endsWith("[]")? FormatDataType(e.slice(0, -2)) : e)
            const method = controller[methodName];
            if(method.requestBodyType)
                importOb[FormatDataType(method.requestBodyType)] = 1;
            else
                method.parameters.forEach(e => importOb[FormatDataType(e.type)] = 1);
            if(method.responsType)
                importOb[FormatDataType(method.responsType)] = 1;
        }
        //object -> list and filtering basic types
        for(let i in importOb)
        {
            let isBasickType = false;
            for(let type in dictionaryType)
                if(i === dictionaryType[type]) {isBasickType = true; break;}
            if(!isBasickType)
                importList.push(i);
        }
        
        const controllerString = `${importList.reduce((acc,curr) => acc + `import {${curr} } from '../Models/API/${curr}'\n`,'')}
export const ${controllerName}Api = {
${methodsString}
}`;
  async function createAndWriteFile() {
            
            const filePath = projectPath + process.env.DestinationApiePath + '/' + controllerName + 'Api.ts';
            
            try {
                const dir = path.dirname(filePath);
                await fs.mkdir(dir, { recursive: true });

                const existing = await fs.stat(filePath).catch(() => null);
                if (existing?.isDirectory()) {
                throw new Error(`Expected a file path but got a directory: ${filePath}`);
                }
                
                await fs.writeFile(filePath, controllerString, 'utf8');

                console.log('File created successfully at:', filePath);
            } catch (err) {
                console.error('Error:', err);
            }
        }
        createAndWriteFile();
    }
}));
