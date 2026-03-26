using Microsoft.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Text;

const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";





const string query = "USE [InterDB];   SELECT * FROM [dbo].[people]";

Stopwatch singleThreadTime = Stopwatch.StartNew();

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    using (SqlCommand sqlCmd = new SqlCommand(query, connection))
    using (SqlDataReader reader = sqlCmd.ExecuteReader())
    {
        string outputFilePath = Path.GetFullPath(@"..\..\..\db.csv");
        using (StreamWriter swr = new StreamWriter(outputFilePath, append: true))
        {
            object[] output = new object[reader.FieldCount];

            for (int i = 0; i < reader.FieldCount; i++)
            {
                output[i] = reader.GetName(i);
            }
            swr.WriteLine(string.Join(";", output));

            while (reader.Read())
            {
                reader.GetValues(output);
                swr.WriteLine(string.Join(";", output));
            }
        }
    }
}
singleThreadTime.Stop();
Console.WriteLine($"single thread Time: {singleThreadTime.Elapsed.TotalSeconds:F2}");







//Stopwatch doubleThreadFullTime = Stopwatch.StartNew();

//string queryMinMax = "USE [InterDB]; SELECT MIN(Id), MAX(Id) FROM [dbo].[people]";

//int minId = 0, maxId = 0;


//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    connection.Open();
//    using (SqlCommand cmd = new SqlCommand(queryMinMax, connection))
//    using (SqlDataReader reader = cmd.ExecuteReader())
//    {
//        if (reader.Read())
//        {
//            minId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
//            maxId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
//        }
//    }
//}

//int midId = (minId + maxId) / 2;

//string query1 = $"USE [InterDB]; SELECT * FROM [dbo].[people] WHERE Id <= {midId} ORDER BY Id";
//string query2 = $"USE [InterDB]; SELECT * FROM [dbo].[people] WHERE Id > {midId} ORDER BY Id";

//string file1 = Path.GetFullPath(@"..\..\..\db_part1.csv");
//string file2 = Path.GetFullPath(@"..\..\..\db_part2.csv");


//Stopwatch doubleThreadTime = Stopwatch.StartNew();
//await Task.WhenAll(
//    ExportToCsv(query1, file1),
//    ExportToCsv(query2, file2)
//);
//doubleThreadTime.Stop();
//Console.WriteLine($"Two-threaded export time: {doubleThreadTime.Elapsed.TotalSeconds:F2}");



//string finalFile = Path.GetFullPath(@"..\..\..\db2.csv");
//using (var sw = new StreamWriter(finalFile))
//{
//    foreach (var line in File.ReadLines(file1))
//        sw.WriteLine(line);


//    foreach (var line in File.ReadLines(file2).Skip(1))
//        sw.WriteLine(line);
//}
//doubleThreadFullTime.Stop();
//Console.WriteLine($"Two-threaded full work time: {doubleThreadFullTime.Elapsed.TotalSeconds:F2}");

//async Task ExportToCsv(string query, string filePath)
//{
//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        await connection.OpenAsync();
//        using (SqlCommand cmd = new SqlCommand(query, connection))
//        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
//        using (StreamWriter sw = new StreamWriter(filePath, append: false))
//        {
//            object[] output = new object[reader.FieldCount];

//            for (int i = 0; i < reader.FieldCount; i++)
//                output[i] = reader.GetName(i);

//            await sw.WriteLineAsync(string.Join(";", output));

//            while (await reader.ReadAsync())
//            {
//                reader.GetValues(output);
//                await sw.WriteLineAsync(string.Join(";", output));
//            }
//        }
//    }
//}