import { readFile, writeFile, mkdir } from "node:fs/promises";
import path from "node:path";



export async function writeUrls(urlsFilePath) {
    const envPath = stripQuotes(process.env.LAUNCH_SETTINGS_PATH);
    const fallbackPath = path.resolve(
        process.cwd(),
        "../KSeF_app_fix.Server/Properties/launchSettings.json"
    );

    const launchSettingsPath = envPath || fallbackPath;

    if (!envPath) {
        console.warn(
            "LAUNCH_SETTINGS_PATH is not set. Using fallback:",
            fallbackPath
        );
    }

    if (!launchSettingsPath) {
        throw new Error("No launchSettings path available");
    }

    let data;
    try {
        data = await readFile(launchSettingsPath, "utf8");
    } catch (err) {
        console.error(
            "Failed to read launchSettings.json at",
            launchSettingsPath,
            err
        );
        throw err;
    }

    let json;
    try {
        json = JSON.parse(data);
    } catch (err) {
        console.error("Failed to parse launchSettings.json:", err);
        throw err;
    }

    const profiles = json.profiles || {};

    const httpUrl =
        profiles.http?.applicationUrl?.split(";")[0] ?? null;

    const httpsUrl =
        (profiles.https?.applicationUrl ?? "")
            .split(";")
            .find((url) => url.startsWith("https://")) || null;

    const urlsToWrite = { httpUrl, httpsUrl };

    try {
        await writeFile(
            urlsFilePath,
            JSON.stringify(urlsToWrite, null, 2),
            "utf8"
        );
        console.log("URLs written to file successfully at", urlsFilePath);
    } catch (err) {
        console.error("Error writing urls.json:", err);
        throw err;
    }

    console.log("httpUrl:", httpUrl);
    console.log("httpsUrl:", httpsUrl);
}

function stripQuotes(value) {
    return String(value || "")
        .trim()
        .replace(/^['"]|['"]$/g, "");
}
