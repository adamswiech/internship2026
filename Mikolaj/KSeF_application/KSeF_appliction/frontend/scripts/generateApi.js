import { swaggerJsonContentAccess } from "./accessServer.js";

const fetchApiEndpoints = async () => {
  const swaggerJsonContent = await swaggerJsonContentAccess;
  if (!swaggerJsonContent || typeof swaggerJsonContent !== "object") {
    console.error("Invalid or missing Swagger JSON:", swaggerJsonContent);
    return;
  }

  const paths = swaggerJsonContent.paths;

  if (!paths || typeof paths !== "object") {
    console.log("No paths found in Swagger document.");
    return;
  }

  const pathsList = Object.keys(paths);

  for (let path of pathsList) {
    console.log(path);
  }

  return pathsList;
};

fetchApiEndpoints();