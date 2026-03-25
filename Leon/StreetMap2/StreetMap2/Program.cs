
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

string filePath = Path.GetFullPath(@"..\..\..\miasta.txt");
string filePathOut = Path.GetFullPath(@"..\..\..\Out.txt");

using var client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("CityCoordsApp/1.0 (zedowlamacz295@gmail.com)");
string gmail = "zedowlamacz295@gmail.com";

var lines = File.ReadAllLines(filePath);

foreach (var line in lines)
{
    string city = line.Split(',')[0].Trim();
    var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(city)}+Polska&format=json&limit=1&email={Uri.EscapeDataString(gmail)}&addressdetails=1";
    using (StreamWriter sw = new StreamWriter(filePathOut, append: true))
    {
        try
        {
            var response = client.GetStringAsync(url).Result;
            var content = JsonDocument.Parse(response);
            var place = JsonSerializer.Deserialize<place[]>(content);
            var p = place[0];

            sw.WriteLine($"{p.name},{p.address.country},{p.lat},{p.lon}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    await Task.Delay(1000);
}




public class place{
    public string name { get; set; }
    public address address { get; set; }
    public string lat { get; set; }
    public string lon { get; set; }

}
public class address
{
    public string country { get; set; }

}