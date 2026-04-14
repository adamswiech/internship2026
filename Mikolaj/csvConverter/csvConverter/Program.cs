using csvConverter;
using System.Diagnostics;

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";
XmlOperations xml = new XmlOperations(connectionString);
string xmlFilePath = @"C:/Users/Vulcan/source/repos/internship2026/Mikolaj/csvConverter/csvConverter/test.xml";

//await xml.loadFile(xmlFilePath);
//await xml.createNewIdx(connectionString, "CREATE INDEX idx_lastname_firstname ON PersonalData (LastName, FirstName)");

//for first name
Stopwatch stopwatch2 = new Stopwatch();
stopwatch2.Start();
var firstNameIdx = await xml.selectByIndex("FirstName", "Emily", "idx_firstname");
stopwatch2.Stop();
TimeSpan elapsedTime2 = stopwatch2.Elapsed;
Console.WriteLine($"Elapsed time for firstname index: {elapsedTime2}");

//for last name
Stopwatch stopwatch1 = new Stopwatch();
stopwatch1.Start();
var lastNameIdx = await xml.selectByIndex("LastName", "Johnson", "idx_lastname");
stopwatch1.Stop();
TimeSpan elapsedTime1 = stopwatch1.Elapsed;
Console.WriteLine($"Elapsed time for lastname index: {elapsedTime1}");

//for first name & last name
Stopwatch s3 = new Stopwatch();
s3.Start();
var firstName_lastNameIdx = await xml.selectByIndex("", "Emma Johnson", "idx_firstname_lastname");
s3.Stop();
TimeSpan elapsedTime3 = s3.Elapsed;
Console.WriteLine($"Elapsed time for firstname & lastname index: {elapsedTime3}");

//for last name & first name
Stopwatch s4 = new Stopwatch();
s4.Start();
var lastName_firstNameIdx = await xml.selectByIndex("", "Johnson Emma", "idx_lastname_firstname");
s4.Stop();
TimeSpan elapsedTime4 = s4.Elapsed;
Console.WriteLine($"Elapsed time for lastname & firstname index: {elapsedTime4}");


//foreach (var p in lastNameIdx)
//{
//    Console.WriteLine($"{p.firstName} {p.lastName}");
//}
