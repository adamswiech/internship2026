using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

//string filePath = "miasta.txt";
//var cities = File.ReadAllLines(filePath);


//using var client = new HttpClient();
//var results = new List<string>();

//string filePath = Path.GetFullPath(@"..\..\..\miasta.txt");
//string filePathOut = Path.GetFullPath(@"..\..\..\out.txt");
//try
//{
//    using (StreamReader sr = new StreamReader(filePath))
//    {
//        string line;
//        while ((line = sr.ReadLine()) != null)
//        {
//            string city = line.Split(',')[0].Trim();
//            var url = $"https://nominatim.openstreetmap.org/search?q={city}&format=json&limit=1";
//            try
//            {
//                var response = await client.GetStringAsync(url);
//                var json = JsonDocument.Parse(response);
//                if (json.RootElement.GetArrayLength() > 0)
//                {
//                    var element = json.RootElement[0];
//                    var lat = element.GetProperty("lat").GetString();
//                    var lon = element.GetProperty("lon").GetString();

//                    results.Add($"{city};{lat};{lon}");
//                    Console.WriteLine($"{city} -> {lat}, {lon}");
//                }
//                else
//                {
//                    results.Add($"{city};NOT_FOUND");
//                }
//            }
//            catch (Exception ex)
//            {
//                results.Add($"{city};ERROR");
//                Console.WriteLine($"Error for {city}: {ex.Message}");

//            }
//        }
//    }
//    using (StreamWriter sw = new StreamWriter(filePathOut))
//    {
//        foreach (var result in results)
//        {
//            sw.WriteLine(result);
//        }
//    }

//}
//catch (Exception e)
//{
//    Console.WriteLine("The file could not be read:");
//    Console.WriteLine(e.Message);
//}








//internal class coordinates
//{
//    public string city { get; set; }
//    public string lat { get; set; }
//    public string lon { get; set; }
//}


using var client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("CityCoordsApp/1.0 (zedowlamacz295@gmail.com)");

string filePath = Path.GetFullPath(@"..\..\..\miasta.txt");
string filePathOut = Path.GetFullPath(@"..\..\..\out.txt");

var lines = File.ReadAllLines(filePath);
string gmail = "zedowlamacz295@gmail.com";


if (File.Exists(filePathOut)) { File.Delete(filePathOut); }

foreach (var line in lines)
{
    string city = line.Split(',')[0].Trim();
    var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(city)}+Polska&format=json&limit=1&email={Uri.EscapeDataString(gmail)}";
    try
    {
        using var sw = new StreamWriter(filePathOut, append: true);
        Console.WriteLine(url);
        var response = client.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        if (json.RootElement.GetArrayLength() > 0)
        {
            var element = json.RootElement[0];
            var lat = element.GetProperty("lat").GetString();
            var lon = element.GetProperty("lon").GetString();
            Console.WriteLine(lat);
            sw.WriteLine($"{city.ToString()},Polska,{lat.ToString(System.Globalization.CultureInfo.InvariantCulture)},{lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
        }
        else
        {
            Console.WriteLine($"{city};NOT_FOUND");
            sw.WriteLine($"{city};NOT_FOUND");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{city};ERROR");
        Console.WriteLine($"Error for {city}: {ex.Message}");
    }
    await Task.Delay(1000);
}
