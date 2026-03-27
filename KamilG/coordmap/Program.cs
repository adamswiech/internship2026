using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.IO;
using coordmap;


var client = new HttpClient();
var nomi = new Nomi(client);

await nomi.GetNomi();







// https://nominatim.openstreetmap.org/search?