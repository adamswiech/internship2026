using System.Text.Json;

string content = File.ReadAllText("miasta.txt");
string query = "Warszawa";
string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(query)}&format=json&polygon_kml=1&addressdetails=1";
string mail = "";

using HttpClient client = new HttpClient();

client.DefaultRequestHeaders.Add("User-Agent", "MyGeoApp/1.0 (j.sjahw@gmail.com)");

HttpResponseMessage response = await client.GetAsync(url);
response.EnsureSuccessStatusCode();

string json = await response.Content.ReadAsStringAsync();
var results = JsonDocument.Parse(json);

foreach (var place in results.RootElement.EnumerateArray())
{
    string displayName = place.GetProperty("display_name").GetString();
    string lat = place.GetProperty("lat").GetString();
    string lon = place.GetProperty("lon").GetString();
    Console.WriteLine($"{displayName} | lat: {lat}, lon: {lon}");
}
