using databaseOperations;
using System.Diagnostics;

XmlOperations xml = new XmlOperations("");
string xmlFilePath = "@\"C:\\Users\\Vulcan\\source\\repos\\internship2026\\Mikolaj\\csvConverter\\csvConverter\\personal_data.xml\"";

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
Console.WriteLine("Timer started!");

xml.loadFile(xmlFilePath);

stopwatch.Stop();
TimeSpan elapsedTime = stopwatch.Elapsed;
Console.WriteLine($"Elapsed time for upload xml file to database: {elapsedTime}");