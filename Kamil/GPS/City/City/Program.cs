//var client = new HttpClient();




using System.Text;
using System.Text.Json;

string pathIn = "C:/Users/VULCAN/Documents/Main/GIT/internship2026/Kamil/GPS/City/City/miasta.txt";
string pathOut = "C:/Users/VULCAN/Documents/Main/GIT/internship2026/Kamil/GPS/City/City/output.txt";
string gmail = "zedowlamacz295@gmail.com";

var client = new HttpClient(); 
client.DefaultRequestHeaders.UserAgent.ParseAdd("CityCoordsApp/1.0 (zedowlamacz295@gmail.com)");


// Create the file, or overwrite if the file exists.

var fs = File.Create(pathOut);
fs.Close();


foreach (string line in File.ReadLines(pathIn))
{

    StreamWriter sw = new StreamWriter(pathOut, append: true);

    int index = line.IndexOf(',');
    string city = line.Substring(0,index);

    string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(city)}+Polska&format=json&limit=1&email={gmail}";

    var response = client.GetAsync(url).Result;
    response.EnsureSuccessStatusCode();

    var responseBody = response.Content.ReadAsStringAsync().Result;

    var responseJson = JsonDocument.Parse(responseBody);

    var lat = responseJson.RootElement[0].GetProperty("lat").GetString();
    var lon = responseJson.RootElement[0].GetProperty("lon").GetString();

    sw.WriteLine($"{line},{lat},{lon}");
    Console.WriteLine(city);
    Thread.Sleep(1000);
    sw.Close();
}
