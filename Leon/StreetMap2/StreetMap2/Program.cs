
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




//using System.Text.Json;
//using var client = new HttpClient();
//client.DefaultRequestHeaders.UserAgent.ParseAdd("CityCoordsApp/1.0 (zedowlamacz295@gmail.com)");
//if (File.Exists(Path.GetFullPath(@"..\..\..\Out.txt"))) File.Delete(Path.GetFullPath(@"..\..\..\Out.txt"));
//foreach (var line in File.ReadAllLines(Path.GetFullPath(@"..\..\..\miasta.txt")))
//{
//    using (StreamWriter sw = new StreamWriter(Path.GetFullPath(@"..\..\..\Out.txt"), append: true))
//    {
//        var place = JsonSerializer.Deserialize<place[]>(await client.GetStringAsync($"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(line.Split(',')[0].Trim())}+Polska&format=json&limit=1&email={Uri.EscapeDataString("zedowlamacz295@gmail.com")}&addressdetails=1"));
//        sw.WriteLine($"{place[0].name},{place[0].address.country},{place[0].lat},{place[0].lon}");
//    }
//    await Task.Delay(1000);
//}
//record place(string name, address address, string lat, string lon);
//record address(string country);