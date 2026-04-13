using csvConverter;
using System.Diagnostics;

string createIndexSql = "CREATE INDEX idx_firstname ON PersonalData (FirstName)";
string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";

XmlOperations xml = new XmlOperations(connectionString);
//string xmlFilePath = @"C:/Users/Vulcan/source/repos/internship2026/Mikolaj/csvConverter/csvConverter/test.xml";



//await xml.loadFile(xmlFilePath);

//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    connection.Open();
//    using (SqlCommand command = new SqlCommand(createIndexSql, connection))
//    {
//        command.ExecuteNonQuery();
//        Console.WriteLine("Index created successfully.");
//    }
//}

Stopwatch stopwatch2 = new Stopwatch();
stopwatch2.Start();
var firstNameIdx = await xml.selectByIndex("FirstName", "Emily", "idx_firstname");
stopwatch2.Stop();
TimeSpan elapsedTime2 = stopwatch2.Elapsed;
Console.WriteLine($"Elapsed time for firstname index: {elapsedTime2}");


Stopwatch stopwatch1 = new Stopwatch();
stopwatch1.Start();
var lastNameIdx = await xml.selectByIndex("LastName", "Johnson", "idx_lastname");
stopwatch1.Stop();
TimeSpan elapsedTime1 = stopwatch1.Elapsed;
Console.WriteLine($"Elapsed time for lastname index: {elapsedTime1}");



//var lastName_firstNameIdx = await xml.selectByIndex("Johnson", "idx_lastname");
//var firstName_lastNameIdx = await xml.selectByIndex("Johnson", "idx_lastname");


//foreach (var p in list)
//{
//    Console.WriteLine(p.firstName);
//}



