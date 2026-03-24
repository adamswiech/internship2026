using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

//string filePath = "miasta.txt";
//var cities = File.ReadAllLines(filePath);
internal class coordinates
{
    public string city { get; set; }
    public string lat { get; set; }
    public string lon { get; set; }
}


using var client = new HttpClient();
client.BaseAddress = new Uri("https://nominatim.openstreetmap.org/search?");

string filePath = Path.GetFullPath(@"..\..\..\miasta.txt");
try
{
    using (StreamReader sr = new StreamReader(filePath))
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            try
            https://nominatim.openstreetmap.org/search?<params>
        }
    }
}
catch (Exception e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}




