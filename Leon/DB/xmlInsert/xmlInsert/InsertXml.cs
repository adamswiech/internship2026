using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xmlInsert
{
    public class InsertXml
    {
        const string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InterDB;Integrated Security=True;";

        public void InsertLinq(string xmlFilePath)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                XDocument doc = XDocument.Load(xmlFilePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = @"
                    INSERT INTO dbo.people
                    (first_name, middle_name, last_name, age, height_cm, weight_kg, city, country, favorite_number)
                    VALUES
                    (@first, @middle, @last, @age, @height, @weight, @city, @country, @fav)
                ";

                    foreach (var person in doc.Descendants("Person"))
                    {
                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@first", (string)person.Element("Name")?.Element("first_name"));
                            cmd.Parameters.AddWithValue("@middle", (string)person.Element("Name")?.Element("middle_name"));
                            cmd.Parameters.AddWithValue("@last", (string)person.Element("Name")?.Element("last_name"));
                            cmd.Parameters.AddWithValue("@age", (int)person.Element("Stats")?.Element("age"));
                            cmd.Parameters.AddWithValue("@height", (decimal)person.Element("Stats")?.Element("height_cm"));
                            cmd.Parameters.AddWithValue("@weight", (decimal)person.Element("Stats")?.Element("weight_kg"));
                            cmd.Parameters.AddWithValue("@city", (string)person.Element("Location")?.Element("city"));
                            cmd.Parameters.AddWithValue("@country", (string)person.Element("Location")?.Element("country"));
                            cmd.Parameters.AddWithValue("@fav", (int)person.Element("Stats")?.Element("favorite_number"));

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"InsertLinq took {sw.Elapsed.TotalMilliseconds} ms");
            }
        }
        public void InsertLinqBulk(string xmlFilePath)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                XDocument doc = XDocument.Load(xmlFilePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = @"
                    INSERT INTO dbo.people
                    (first_name, middle_name, last_name, age, height_cm, weight_kg, city, country, favorite_number)
                    VALUES
                    (@first, @middle, @last, @age, @height, @weight, @city, @country, @fav)
                ";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("first_name", typeof(string));
                    dt.Columns.Add("middle_name", typeof(string));
                    dt.Columns.Add("last_name", typeof(string));
                    dt.Columns.Add("age", typeof(int));
                    dt.Columns.Add("height_cm", typeof(int));
                    dt.Columns.Add("weight_kg", typeof(int));
                    dt.Columns.Add("city", typeof(string));
                    dt.Columns.Add("country", typeof(string));
                    dt.Columns.Add("favorite_number", typeof(int));
                    foreach (var person in doc.Descendants("Person"))
                    {
                        dt.Rows.Add(
                            (string)person.Element("Name")?.Element("first_name"),
                            (string)person.Element("Name")?.Element("middle_name"),
                            (string)person.Element("Name")?.Element("last_name"),
                            (int)person.Element("Stats")?.Element("age"),
                            (decimal)person.Element("Stats")?.Element("height_cm"),
                            (decimal)person.Element("Stats")?.Element("weight_kg"),
                            (string)person.Element("Location")?.Element("city"),
                            (string)person.Element("Location")?.Element("country"),
                            (int)person.Element("Stats")?.Element("favorite_number")
                        );

                    }
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
                    {
                        bulkCopy.DestinationTableName = "InterDB.dbo.people";
                        bulkCopy.BulkCopyTimeout = 0;

                        bulkCopy.ColumnMappings.Add("first_name", "first_name");
                        bulkCopy.ColumnMappings.Add("middle_name", "middle_name");
                        bulkCopy.ColumnMappings.Add("last_name", "last_name");
                        bulkCopy.ColumnMappings.Add("age", "age");
                        bulkCopy.ColumnMappings.Add("height_cm", "height_cm");
                        bulkCopy.ColumnMappings.Add("weight_kg", "weight_kg");
                        bulkCopy.ColumnMappings.Add("city", "city");
                        bulkCopy.ColumnMappings.Add("country", "country");
                        bulkCopy.ColumnMappings.Add("favorite_number", "favorite_number");

                        try
                        {
                            bulkCopy.WriteToServer(dt);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"InsertLinqBulk took {sw.Elapsed.TotalMilliseconds} ms");
            }
        }
        public void InsertSerialize(string xmlFilePath)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                using (XmlReader reader = XmlReader.Create(xmlFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(People));
                    People people = (People)serializer.Deserialize(reader);

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;

                            cmd.Connection = connection;
                            cmd.CommandText = @"
                            INSERT INTO dbo.people
                            (first_name, middle_name, last_name, age, height_cm, weight_kg, city, country, favorite_number)
                            VALUES
                            (@first, @middle, @last, @age, @height, @weight, @city, @country, @fav)
                        ";
                            foreach (var person in people.Persons)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("@first", SqlDbType.NVarChar, 50).Value = person.Name.FirstName;
                                cmd.Parameters.Add("@middle", SqlDbType.NVarChar, 50).Value = person.Name.MiddleName;
                                cmd.Parameters.Add("@last", SqlDbType.NVarChar, 50).Value = person.Name.LastName;
                                cmd.Parameters.Add("@age", SqlDbType.Int).Value = person.Stats.Age;
                                var heightParam = cmd.Parameters.Add("@height", SqlDbType.Decimal);
                                heightParam.Precision = 5;
                                heightParam.Scale = 2;
                                heightParam.Value = person.Stats.HeightCm;
                                var weightParam = cmd.Parameters.Add("@weight", SqlDbType.Decimal);
                                weightParam.Precision = 5;
                                weightParam.Scale = 2;
                                weightParam.Value = person.Stats.WeightKg;
                                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = person.Location.City;
                                cmd.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = person.Location.Country;
                                cmd.Parameters.Add("@fav", SqlDbType.Int).Value = person.Stats.FavoriteNumber;
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                    }
                }
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"InsertSerialize took {sw.Elapsed.TotalMilliseconds} ms");
            }
        }
        public void InsertSerializeBulk(string xmlFilePath)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                using (XmlReader reader = XmlReader.Create(xmlFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(People));
                    People people = (People)serializer.Deserialize(reader);

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("first_name", typeof(string));
                        dt.Columns.Add("middle_name", typeof(string));
                        dt.Columns.Add("last_name", typeof(string));
                        dt.Columns.Add("age", typeof(int));
                        dt.Columns.Add("height_cm", typeof(decimal));
                        dt.Columns.Add("weight_kg", typeof(decimal));
                        dt.Columns.Add("city", typeof(string));
                        dt.Columns.Add("country", typeof(string));
                        dt.Columns.Add("favorite_number", typeof(int));
                        foreach (var person in people.Persons)
                        {
                            dt.Rows.Add(
                                person.Name.FirstName,
                                person.Name.MiddleName,
                                person.Name.LastName,
                                person.Stats.Age,
                                person.Stats.HeightCm,
                                person.Stats.WeightKg,
                                person.Location.City,
                                person.Location.Country,
                                person.Stats.FavoriteNumber
                            );
                        }
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "InterDB.dbo.people";
                            bulkCopy.BulkCopyTimeout = 0;

                            bulkCopy.ColumnMappings.Add("first_name", "first_name");
                            bulkCopy.ColumnMappings.Add("middle_name", "middle_name");
                            bulkCopy.ColumnMappings.Add("last_name", "last_name");
                            bulkCopy.ColumnMappings.Add("age", "age");
                            bulkCopy.ColumnMappings.Add("height_cm", "height_cm");
                            bulkCopy.ColumnMappings.Add("weight_kg", "weight_kg");
                            bulkCopy.ColumnMappings.Add("city", "city");
                            bulkCopy.ColumnMappings.Add("country", "country");
                            bulkCopy.ColumnMappings.Add("favorite_number", "favorite_number");

                            try
                            {
                                bulkCopy.WriteToServer(dt);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"InsertSerializeBulk took {sw.Elapsed.TotalMilliseconds} ms");
            }
        }


        private static readonly string logFile = Path.GetFullPath("../../../results.txt");
        private StreamWriter logWriter;

        public class Scenario
        {
            public string Name { get; set; }
            public string Sql { get; set; }
            public string IndexName { get; set; }
        }

        public List<Scenario> scenarios = new()
    {
        new() { Name = "No index", Sql = null, IndexName = null },
        new() { Name = "Index first_name", Sql = "CREATE INDEX IX1 ON dbo.people(first_name)", IndexName = "IX1" },
        new() { Name = "Index last_name", Sql = "CREATE INDEX IX2 ON dbo.people(last_name)", IndexName = "IX2" },
        new() { Name = "Index first+last", Sql = "CREATE INDEX IX3 ON dbo.people(first_name, last_name)", IndexName = "IX3" },
        new() { Name = "Index last+first", Sql = "CREATE INDEX IX4 ON dbo.people(last_name, first_name)", IndexName = "IX4" }
    };

        private void InitLogger()
        {
            logWriter = new StreamWriter(logFile, append: true)
            {
                AutoFlush = true
            };
        }

        private void Log(string message)
        {
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}";
            Console.WriteLine(line);
            logWriter.WriteLine(line);
        }

        public void BulkCopyF(DataTable dt, SqlConnection connection, string scenario)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, null))
            {
                bulkCopy.DestinationTableName = "InterDB.dbo.people";
                bulkCopy.BulkCopyTimeout = 0;

                bulkCopy.ColumnMappings.Add("first_name", "first_name");
                bulkCopy.ColumnMappings.Add("middle_name", "middle_name");
                bulkCopy.ColumnMappings.Add("last_name", "last_name");
                bulkCopy.ColumnMappings.Add("age", "age");
                bulkCopy.ColumnMappings.Add("height_cm", "height_cm");
                bulkCopy.ColumnMappings.Add("weight_kg", "weight_kg");
                bulkCopy.ColumnMappings.Add("city", "city");
                bulkCopy.ColumnMappings.Add("country", "country");
                bulkCopy.ColumnMappings.Add("favorite_number", "favorite_number");

                bulkCopy.WriteToServer(dt);
            }
        }

        public void InsertTest(string xmlFilePath)
        {
            InitLogger();

            try
            {
                XDocument doc = XDocument.Load(xmlFilePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("first_name", typeof(string));
                    dt.Columns.Add("middle_name", typeof(string));
                    dt.Columns.Add("last_name", typeof(string));
                    dt.Columns.Add("age", typeof(int));
                    dt.Columns.Add("height_cm", typeof(decimal));
                    dt.Columns.Add("weight_kg", typeof(decimal));
                    dt.Columns.Add("city", typeof(string));
                    dt.Columns.Add("country", typeof(string));
                    dt.Columns.Add("favorite_number", typeof(int));

                    foreach (var person in doc.Descendants("Person"))
                    {
                        dt.Rows.Add(
                            (string)person.Element("Name")?.Element("first_name"),
                            (string)person.Element("Name")?.Element("middle_name"),
                            (string)person.Element("Name")?.Element("last_name"),
                            (int)person.Element("Stats")?.Element("age"),
                            (decimal)person.Element("Stats")?.Element("height_cm"),
                            (decimal)person.Element("Stats")?.Element("weight_kg"),
                            (string)person.Element("Location")?.Element("city"),
                            (string)person.Element("Location")?.Element("country"),
                            (int)person.Element("Stats")?.Element("favorite_number")
                        );
                    }

                    Log("");
                    Log("====================================================");
                    Log($"NEW RUN STARTED: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    Log("====================================================");
                    Log("");

                    foreach (var s in scenarios)
                    {
                        Log($"==============================");
                        Log($"Running scenario: {s.Name}");
                        Log($"==============================");

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = connection;

                            cmd.CommandText = @"
                            DROP INDEX IF EXISTS IX1 ON dbo.people;
                            DROP INDEX IF EXISTS IX2 ON dbo.people;
                            DROP INDEX IF EXISTS IX3 ON dbo.people;
                            DROP INDEX IF EXISTS IX4 ON dbo.people;";
                            cmd.ExecuteNonQuery();

                            if (s.Sql != null)
                            {
                                cmd.CommandText = s.Sql;
                                cmd.ExecuteNonQuery();
                            }

                            decimal totalInsert = 0;
                            decimal totalSelect = 0;

                            for (int i = 0; i < 20; i++)
                            {
                                cmd.CommandText = "CHECKPOINT; DBCC DROPCLEANBUFFERS; DBCC FREEPROCCACHE;";
                                cmd.ExecuteNonQuery();

                                var insertWatch = Stopwatch.StartNew();
                                BulkCopyF(dt, connection, s.Name);
                                insertWatch.Stop();

                                var selectWatch = Stopwatch.StartNew();

                                cmd.CommandText = @"
                                SELECT *
                                FROM dbo.people
                                WHERE first_name = 'Hailey'
                                  AND last_name = 'Morgan';";

                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read()) { }
                                }

                                selectWatch.Stop();

                                Log($"{s.Name} run {i} insert: {insertWatch.Elapsed.TotalMilliseconds} ms");
                                Log($"{s.Name} run {i} select: {selectWatch.Elapsed.TotalMilliseconds} ms");

                                totalInsert += insertWatch.ElapsedMilliseconds;
                                totalSelect += selectWatch.ElapsedMilliseconds;

                                cmd.CommandText = "TRUNCATE TABLE dbo.people";
                                cmd.ExecuteNonQuery();
                            }

                            Log("------------------------------");
                            Log($"{s.Name} AVG insert: {totalInsert / 20} ms");
                            Log($"{s.Name} AVG select: {totalSelect / 20} ms");
                            Log("------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
            }
            finally
            {
                logWriter?.Dispose();
            }
        }
    }
}