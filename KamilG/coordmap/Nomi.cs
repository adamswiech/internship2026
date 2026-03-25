using System;
using System.Collections.Generic; // Wymagane dla List<>

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace coordmap
{
    public class Nomi
    {
        // Pusty konstruktor używany przy deserializacji JSON
        public Nomi() { }

        // Konstruktor do wstrzykiwania instancji HttpClient
        public Nomi(HttpClient client)
        {
            _client = client;
        }

        // Zmapowane oryginalne nazwy "lat" i "lon" z API, a typ zwracany to string
        [JsonPropertyName("lat")]
        public string latitude { get; set; }

        [JsonPropertyName("lon")]
        public string longitude { get; set; }
       
        public readonly HttpClient _client;

        public async Task GetNomi()
        {
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("CityCoordsApp/1.0 (iph@gmail.com)");

            string filePath = Path.GetFullPath(@"..\..\..\miasta.txt");
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] t = line.Split(',');
                string city = t[0].Trim();
                string country = t[1].Trim();
                
                var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(city)}+{Uri.EscapeDataString(country)}&format=json&limit=1";
                
                // Poprawka: Oczekiwanie na List<Nomi> i użycie await zamiast .Result
                var response = await _client.GetFromJsonAsync<List<Nomi>>(url);
                var element = response?.FirstOrDefault();

                if (element is null)
                {
                    Console.WriteLine("Brak danych.");
                }
                else
                {
                    Console.WriteLine($"Lat: {element.latitude}, Lon: {element.longitude}");
                }
                
                await Task.Delay(1000);
            }
        }
    }
}
