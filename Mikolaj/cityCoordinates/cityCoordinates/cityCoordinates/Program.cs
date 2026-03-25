using System.Text.Json;
using HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "MyGeoApp/1.0 (j.sjahw@gmail.com)");

string content = File.ReadAllText("miasta.txt");

foreach (string line in content.Split('\n'))
{
    string query = line;
    string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(query)}&format=json&polygon_kml=1&addressdetails=1";

    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    string json = await response.Content.ReadAsStringAsync();
    var results = JsonDocument.Parse(json);

    foreach (var place in results.RootElement.EnumerateArray())
    {
        string cityName = place.GetProperty("name").GetString();
        string lat = place.GetProperty("lat").GetString();
        string lon = place.GetProperty("lon").GetString();
        Console.WriteLine($"{cityName}, lat: {lat}, lon: {lon}");
    }

    await Task.Delay(1100);
}