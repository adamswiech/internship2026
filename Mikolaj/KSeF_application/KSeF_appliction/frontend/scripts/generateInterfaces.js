import fs from "node:fs/promises";
import "dotenv/config";
import { dictionaryType, dictionaryFormat } from "./dictionary.js";
import { swaggerJsonContentAccess } from "./accessServer.js";

const INTERFACES_PATH = process.env.INTERFACES_PATH;

const deleteAllInterfaces = async () => {
  try {
    await fs.unlink(`${INTERFACES_PATH}/Podmiot.ts`);
    await fs.unlink(`${INTERFACES_PATH}/FaWiersz.ts`);
    await fs.unlink(`${INTERFACES_PATH}/Faktura.ts`);
  } catch {
    console.log("No interfaces to delete\n");
  }
};

await deleteAllInterfaces();
const swaggerJsonContent = await swaggerJsonContentAccess;

const generateInterfaces = async () => {
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
          //action on items list
          const objRef = Object.values(
            schemasList[mainKey].properties[name].items,
          )[0];
          const objRefName = objRef.substring(objRef.lastIndexOf("/") + 1);

          await fs.appendFile(
            `${INTERFACES_PATH}/${mainKey}.ts`,
            `${name}: ${objRefName}[];\n`,
            "utf8",
          );

          if (!imports.includes(`${objRefName}`)) imports.push(`${objRefName}`);
        } else if (propertiesObjects[objectsKeys[j]]["$ref"]) {
          //action on $ref
          const objRef = propertiesObjects[objectsKeys[j]]["$ref"];
          const objRefName = objRef.substring(objRef.lastIndexOf("/") + 1);

          await fs.appendFile(
            `${INTERFACES_PATH}/${mainKey}.ts`,
            `${name}: ${objRefName};\n`,
            "utf8",
          );

          if (!imports.includes(`${objRefName}`)) imports.push(`${objRefName}`);
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
    console.log("\x1b[32mInterfaces successfully generated.\x1b[0m");
  } catch (e) {
    console.log("\x1b[31mInterfaces generating error.\x1b[0m");
    console.log(`\x1b[31mError: ${e}\x1b[0m`);
  }
};

generateInterfaces();