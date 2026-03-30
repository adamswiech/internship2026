using Microsoft.Data.SqlClient;
using GenFu;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace InsertRecord
{
    internal class Bulk
    {
        private static readonly string[] countries = new[]
        {
            "USA","Canada","Germany","France","Japan","Australia","Brazil","China","India","Russia",
            "Italy","United Kingdom","Mexico","South Korea","Spain","Netherlands","Switzerland","Sweden","Norway","Belgium",
            "Argentina","South Africa","New Zealand","Saudi Arabia","Turkey","Indonesia","Thailand","Egypt","Greece","Portugal",
            "Poland","Austria","Finland","Ireland","Hungary","Czech Republic","Vietnam","Philippines","Malaysia","Singapore",
            "Chile","Colombia","Peru","Pakistan","Bangladesh","Ukraine","Romania","Israel","Morocco","Kenya"
        };

        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";
        private static readonly string resultsFile = Path.GetFullPath(@"..\..\..\Results.txt");

        public static void InsertPeople(int count = 1000000)
        {
            Stopwatch totalTime = Stopwatch.StartNew();

            Stopwatch genFuTime = Stopwatch.StartNew();
            A.Configure<Person>()
                .Fill(p => p.FirstName).AsFirstName()
                .Fill(p => p.MiddleName).AsFirstName()
                .Fill(p => p.LastName).AsLastName()
                .Fill(p => p.Age).WithinRange(0, 99)
                .Fill(p => p.HeightCm).WithinRange(150, 200)
                .Fill(p => p.WeightKg).WithinRange(50, 100)
                .Fill(p => p.City).AsCity()
                .Fill(p => p.Country).WithRandom(countries)
                .Fill(p => p.FavoriteNumber).WithinRange(0, 9);

            var peoples = A.ListOf<Person>(count);
            genFuTime.Stop();

            Stopwatch dataTableTime = Stopwatch.StartNew();
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

            foreach (var p in peoples)
            {
                dt.Rows.Add(p.FirstName, p.MiddleName, p.LastName, p.Age, p.HeightCm, p.WeightKg, p.City, p.Country, p.FavoriteNumber);
            }
            dataTableTime.Stop();


            Stopwatch bulkCopyTime = Stopwatch.StartNew();
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
            bulkCopyTime.Stop();
            totalTime.Stop();


            string result = $@"
Run Time: {DateTime.Now}
Data generation: {genFuTime.Elapsed.TotalSeconds:F2} sec
DataTable creation: {dataTableTime.Elapsed.TotalSeconds:F2} sec
Bulk copy insert: {bulkCopyTime.Elapsed.TotalSeconds:F2} sec
Total time: {totalTime.Elapsed.TotalSeconds:F2} sec
----------------------------------------
";
            File.AppendAllText(resultsFile, result);
            Console.WriteLine(result);
        }

        private class Person
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public int HeightCm { get; set; }
            public int WeightKg { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public int FavoriteNumber { get; set; }
        }
    }
}