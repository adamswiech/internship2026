import { readFile } from 'node:fs/promises';   // ESM (recommended)
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import {dictionaryType, dictionaryFormat} from './dictionary.js';
import fs from 'node:fs/promises';
import path  from 'node:path';
console.log("---------------------------------------------------------------");

let httpApiBase = "";
let httpsApiBase = "";
let paths = {};

const GetType = (model)=>{
    let type = "any";
    
    if (!model || typeof model !== "object") {
        return type;
    }

    if(Exist(model["type"]))
        type = dictionaryType[model.type];

    if(Exist(model["format"])){
        type = dictionaryFormat[model["format"]];
    }

    if(Exist(model["items"]))
    {
        type = GetType(model["items"]) + "[]";
    }

    if(Exist(model["$ref"])){
        const objectName = model["$ref"].split('/').pop(); 
        type = objectName;
    }
    return type;
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

//delet and create files of ApiObjects
deleteAllFilesInFolder(path.join(  'src','API'));  

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
const Exist = (e) => (e !== undefined && e !== null );


//setting paths
await (async ()=>{
    const swaggerLink = `${httpsApiBase}/swagger/v1/swagger.json`;
    const swaggerRespons = await fetch(swaggerLink);
    const swaggerData = await swaggerRespons.json();
    paths = swaggerData.paths;
})();

//settring controllers
const controllers = {};
for(let endpoint in paths)
{
    let tag = "";
    let method
    const endpointName = endpoint.split('/').pop().toString();
    //geting method
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
            
            // if(!paths[endpoint][method].parameters) break;

            // controllers[tag][endpointName]["parameters"] = [];

            // for(let i of paths[endpoint][method].parameters)
            //     controllers[tag][endpointName]["parameters"].push({
            //         name: i.name,
            //         type: GetType(i.schema)
            // })
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

//writhing controllers to files

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
            methodString = `
    ${methodName}: async (${paramsString}) : Promise<${method.responsType}> => {
        try {
            const response  = await fetch('${httpsApiBase}/${controllerName}/${methodName}');

            if (!response.ok) 
                throw new Error("Network response was not ok: ` + "${response.status}" + `");

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
        const response = await fetch('${httpsApiBase}/${controllerName}/createFaktura', {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) 
            throw new Error('Network response was not ok: ` + "${response.status}" + `');

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
        
        const filePath = path.join(           
            'src',
            'API',
            `${controllerName}Api.ts`
        );
        
        try {
            const dir = path.dirname(filePath);
            await fs.mkdir(dir, { recursive: true });

            await fs.writeFile(filePath, controllerString, 'utf8');

            console.log('File created successfully at:', filePath);
        } catch (err) {
            console.error('Error:', err);
        }
    }

    createAndWriteFile();
                 
}


console.log("----------------------------------------------");