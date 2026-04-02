import fs from "node:fs"



// const filePath = `${process.env.LAUNCH_SETTINGS_PATH}`;

// fs.readFile(filePath, 'utf8', (err, data) => {
//   if (err) {
//     console.error(err);
//     return;
//   }

//   const json = JSON.parse(data);
//   const profiles = json.profiles;

//   let httpUrl = null;
//   let httpsUrl = null;

//   if (profiles.http?.applicationUrl) {
//     httpUrl = profiles.http.applicationUrl.split(';')[0];
//   }

//   if (profiles.https?.applicationUrl) {
//     const urls = profiles.https.applicationUrl.split(';');
//     httpsUrl = urls.find(url => url.startsWith('https://')) || null;
//   }

//   console.log('httpUrl:', httpUrl);
//   console.log('httpsUrl:', httpsUrl);
// });

// ------


if (!process.env.LAUNCH_SETTINGS_PATH) {
  console.log("ENV is NOT set");

  fs.writeFile('./scripts/test.json', "file is called", (err) => {
    if (err) {
      console.error(err);
      return;
    }
    console.log('File written successfully.');
  });

} else {
  console.log("ENV is set:", process.env.LAUNCH_SETTINGS_PATH);
}