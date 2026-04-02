import fs from "node:fs"
import path from "node:path"
import dotenv from "dotenv"

dotenv.config()

function stripQuotes(value) {
  return String(value || "").trim().replace(/^['"]|['"]$/g, "")
}

const envPath = stripQuotes(process.env.LAUNCH_SETTINGS_PATH)
const fallbackPath = path.resolve(process.cwd(), "../KSeF_app_fix.Server/Properties/launchSettings.json")
const filePath = envPath || fallbackPath

if (!envPath) {
  console.warn("LAUNCH_SETTINGS_PATH is not set. Using fallback:", fallbackPath)
}

if (!filePath) {
  console.error("No launchSettings path to read.")
  process.exit(1)
}

fs.readFile(filePath, "utf8", (err, data) => {
  if (err) {
    console.error("Failed to read launchSettings.json at", filePath, err)
    return
  }

  let json
  try {
    json = JSON.parse(data)
  } catch (parseErr) {
    console.error("Failed to parse launchSettings.json:", parseErr)
    return
  }

  const profiles = json.profiles || {}
  let httpUrl = null
  let httpsUrl = null

  if (profiles.http?.applicationUrl) {
    httpUrl = profiles.http.applicationUrl.split(";")[0]
  }

  if (profiles.https?.applicationUrl) {
    const urls = profiles.https.applicationUrl.split(";")
    httpsUrl = urls.find(url => url.startsWith("https://")) || null
  }

  const urlsToWrite = { httpUrl, httpsUrl }

  try {
    fs.writeFileSync("./scripts/urls.json", JSON.stringify(urlsToWrite, null, 2), "utf8")
    console.log("URLs written to file successfully.")
  } catch (writeErr) {
    console.error("Error writing file:", writeErr)
  }

  console.log("httpUrl:", httpUrl)
  console.log("httpsUrl:", httpsUrl)
})