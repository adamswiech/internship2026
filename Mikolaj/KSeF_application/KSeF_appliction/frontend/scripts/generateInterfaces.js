import fs from "node:fs/promises";
import "dotenv/config";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";
import { accessSwaggerJsonContent } from "./accessServer.js";
const INTERFACES_PATH = process.env.INTERFACES_PATH;

console.log("\x1b[32m> generateInterfaces.js started...\x1b[0m");

const deleteAllInterfaces = async () => {
  try {
    const files = await fs.readdir(INTERFACES_PATH);
    for (const file of files) {
      const filePath = `${INTERFACES_PATH}/${file}`;
      await fs.unlink(filePath);
    }

    // console.log(`Deleted ${files.length} interface files`);
  } catch (error) {
    console.log("No interfaces to delete or directory doesn't exist\n");
  }
};

await deleteAllInterfaces();
const swaggerJsonContent = await accessSwaggerJsonContent;

export const generateInterfaces = async () => {
  try {
    const schemasList = swaggerJsonContent.components.schemas; //this path here is always the same so can be hardcoded (in the latest versions of swagger)
    const keys = Object.keys(schemasList); //main keys - faktura, fawiersz, podmiot

    for (let i = 0; i < keys.length; i++) {
      //each iteration = new interface because of new keys[i] value
      let imports = [];

      const mainKey = keys[i]; //main key from main keys
      const propertiesObjects = schemasList[mainKey].properties;
      const objectsKeys = Object.keys(propertiesObjects);

      await fs.appendFile(
        `${INTERFACES_PATH}/${mainKey}.ts`,
        `export interface ${mainKey} {\n`,
        "utf8",
      );

      for (let j = 0; j < objectsKeys.length; j++) {
        const name = objectsKeys[j];
        const type = dictionaryType[propertiesObjects[objectsKeys[j]].type];
        const format =
          dictionaryFormat[propertiesObjects[objectsKeys[j]].format];

        if (schemasList[mainKey].properties[name].items) {
          const objRef = Object.values(
            schemasList[mainKey].properties[name].items,
          )[0];

          const objRefName = objRef.substring(objRef.lastIndexOf("/") + 1);

          await fs.appendFile(
            `${INTERFACES_PATH}/${mainKey}.ts`,
            `${name}: ${objRefName}[];\n`,
            "utf8",
          );

          !imports.includes(`${objRefName}`) && imports.push(`${objRefName}`);
        } else if (propertiesObjects[objectsKeys[j]]["$ref"]) {
          const objRef = propertiesObjects[objectsKeys[j]]["$ref"];
          const objRefName = objRef.substring(objRef.lastIndexOf("/") + 1);

          await fs.appendFile(
            `${INTERFACES_PATH}/${mainKey}.ts`,
            `${name}: ${objRefName};\n`,
            "utf8",
          );

          !imports.includes(`${objRefName}`) && imports.push(`${objRefName}`);
        } else {
          await fs.appendFile(
            `${INTERFACES_PATH}/${mainKey}.ts`,
            `${name}: ${format == undefined ? type : format};\n`,
            "utf8",
          );
        }
      }

      await fs.appendFile(`${INTERFACES_PATH}/${mainKey}.ts`, `}\n`, "utf8");
      imports.forEach(async (el) => {
        await fs.appendFile(
          `${INTERFACES_PATH}/${mainKey}.ts`,
          `import type { ${el} } from "./${el}";\n`,
          "utf8",
        );
      });
    }
    console.log("\x1b[32mInterfaces have been generated!\x1b[0m");
  } catch (e) {
    console.log("\x1b[31mInterfaces generating error.\x1b[0m");
    console.log(`\x1b[31mError: ${e}\x1b[0m`);
  }
};
