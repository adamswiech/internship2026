using Microsoft.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Text;

const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";





const string query = "USE [InterDB];   SELECT * FROM [dbo].[people]";

if (File.Exists(Path.GetFullPath(@"..\..\..\db.csv"))) { File.Delete(Path.GetFullPath(@"..\..\..\db.csv")); }

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









// Two-threaded export




if (File.Exists(Path.GetFullPath(@"..\..\..\db_part1.csv"))) { File.Delete(Path.GetFullPath(@"..\..\..\db_part1.csv")); }
if (File.Exists(Path.GetFullPath(@"..\..\..\db_part2.csv"))) { File.Delete(Path.GetFullPath(@"..\..\..\db_part2.csv")); }
if (File.Exists(Path.GetFullPath(@"..\..\..\db2.csv"))) { File.Delete(Path.GetFullPath(@"..\..\..\db2.csv")); }


Stopwatch doubleThreadFullTime = Stopwatch.StartNew();

string queryMinMax = "USE [InterDB]; SELECT MIN(Id), MAX(Id) FROM [dbo].[people]";

int minId = 0, maxId = 0;


using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    using (SqlCommand cmd = new SqlCommand(queryMinMax, connection))
    using (SqlDataReader reader = cmd.ExecuteReader())
    {
        if (reader.Read())
        {
            minId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            maxId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
        }
    }
}

int midId = (minId + maxId) / 2;

string query1 = $"USE [InterDB]; SELECT * FROM [dbo].[people] WHERE Id <= {midId} ORDER BY Id";
string query2 = $"USE [InterDB]; SELECT * FROM [dbo].[people] WHERE Id > {midId} ORDER BY Id";

string file1 = Path.GetFullPath(@"..\..\..\db_part1.csv");
string file2 = Path.GetFullPath(@"..\..\..\db_part2.csv");


Stopwatch doubleThreadTime = Stopwatch.StartNew();
await Task.WhenAll(
    ExportToCsv(query1, file1),
    ExportToCsv(query2, file2)
);
doubleThreadTime.Stop();
Console.WriteLine($"Two-threaded export time: {doubleThreadTime.Elapsed.TotalSeconds:F2}");



string finalFile = Path.GetFullPath(@"..\..\..\db2.csv");
using (var sw = new StreamWriter(finalFile))
{
    foreach (var line in File.ReadLines(file1))
        sw.WriteLine(line);


    foreach (var line in File.ReadLines(file2).Skip(1))
        sw.WriteLine(line);
}
doubleThreadFullTime.Stop();
Console.WriteLine($"Two-threaded full work time: {doubleThreadFullTime.Elapsed.TotalSeconds:F2}");

async Task ExportToCsv(string query, string filePath)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();
        using (SqlCommand cmd = new SqlCommand(query, connection))
        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        using (StreamWriter sw = new StreamWriter(filePath, append: false))
        {
            object[] output = new object[reader.FieldCount];

            for (int i = 0; i < reader.FieldCount; i++)
                output[i] = reader.GetName(i);

            await sw.WriteLineAsync(string.Join(";", output));

            while (await reader.ReadAsync())
            {
                reader.GetValues(output);
                await sw.WriteLineAsync(string.Join(";", output));
            }
        }
    }
}













// 10 thread export







int threadCount = 10;

var files = Enumerable.Range(1, threadCount)
  .Select(i => Path.GetFullPath( $@"..\..\..\db_multi_part{i}.csv"))
  .ToList();

string fullFile = Path.GetFullPath(@"..\..\..\db_multi_final.csv");

foreach (var f in files)
    if (File.Exists(f)) File.Delete(f);

if (File.Exists(fullFile)) File.Delete(fullFile);

Stopwatch totalTimeMulti = Stopwatch.StartNew();

string prepareQuery = $@"
USE [InterDB];

IF OBJECT_ID('[dbo].[PeopleBatch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PeopleBatch];

SELECT
    *,
    NTILE({threadCount}) OVER (ORDER BY (SELECT NULL)) AS Batch
INTO [dbo].[PeopleBatch]
FROM [dbo].[people];
";//ORDER BY (id)

using (SqlConnection conn = new SqlConnection(connectionString))
{
    await conn.OpenAsync();
    using (SqlCommand cmd = new SqlCommand(prepareQuery, conn))
    {
        cmd.CommandTimeout = 0;
        await cmd.ExecuteNonQueryAsync();
    }
}


string indexQuery = @"USE [InterDB];
CREATE INDEX IX_PeopleBatch_Batch_Id
ON [dbo].[PeopleBatch](Batch, Id);
";

using (SqlConnection conn = new SqlConnection(connectionString))
{
    await conn.OpenAsync();
    using (SqlCommand cmd = new SqlCommand(indexQuery, conn))
    {
        await cmd.ExecuteNonQueryAsync();
    }
}


Stopwatch exportTimeMulti = Stopwatch.StartNew();

var tasks = new List<Task>();

for (int i = 1; i <= threadCount; i++)
{
    int batch = i;
    string file = files[i - 1];

    string queryString = $@"USE [InterDB];
  SELECT*
    FROM [dbo].[PeopleBatch]
  WHERE Batch = {
    batch
  }
  ORDER BY Id ";

  tasks.Add(ExportToCsvMulti(queryString, file));
}

await Task.WhenAll(tasks);

exportTimeMulti.Stop();
Console.WriteLine($"multi thread export time: {exportTimeMulti.Elapsed.TotalSeconds:F2}s");


using (var sw = new StreamWriter(finalFile))
{
    for (int i = 0; i < files.Count; i++)
    {
        var lines = File.ReadLines(files[i]);

        if (i == 0)
        {
            foreach (var line in lines)
                sw.WriteLine(line);
        }
        else
        {
            foreach (var line in lines.Skip(1))
                sw.WriteLine(line);
        }
    }
}

using (SqlConnection conn = new SqlConnection(connectionString))
{
    await conn.OpenAsync();
    using (SqlCommand cmd = new SqlCommand("USE [InterDB]; DROP TABLE [dbo].[PeopleBatch];", conn))
    {
        await cmd.ExecuteNonQueryAsync();
    }
}

totalTimeMulti.Stop();
Console.WriteLine($"multi thread total time: {totalTimeMulti.Elapsed.TotalSeconds:F2}s");



static async Task ExportToCsvMulti(string query, string filePath)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();

        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            cmd.CommandTimeout = 0;

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                object[] values = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                    values[i] = reader.GetName(i);

                await sw.WriteLineAsync(string.Join(";", values));

                while (await reader.ReadAsync())
                {
                    reader.GetValues(values);
                    await sw.WriteLineAsync(string.Join(";", values));
                }
            }
        }
    }
}