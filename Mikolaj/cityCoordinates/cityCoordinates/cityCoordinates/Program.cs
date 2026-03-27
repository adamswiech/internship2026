using System.Text.Json;

string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
using HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "MyGeoApp/1.0 (jewq1.asdh@gmail.com)");

string content = File.ReadAllText("miasta.txt");
string outputFile = Path.Combine(projectRoot, "out.txt");

File.WriteAllText(outputFile, string.Empty);

foreach (string line in content.Split('\n'))
{
    try
    {
        using (StreamWriter writer = new StreamWriter(outputFile, append: true))
        {
            string query = line;
            string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(query)}&format=json&polygon_kml=1&addressdetails=1";

            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;
            var results = JsonDocument.Parse(json);

            foreach (var place in results.RootElement.EnumerateArray())
            {
                string cityName = place.GetProperty("name").GetString();
                string lat = place.GetProperty("lat").GetString();
                string lon = place.GetProperty("lon").GetString();

                writer.WriteLine($"{cityName}, {lat}, {lon}");
            }

            writer.Close();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    await Task.Delay(1000);
}