using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

namespace db_1
{
    public class Insert
    {
        public static void Execute(int totalRecords, int batchSize)
        {
            const string constr = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=""SQL Server Management Studio"";Command Timeout=0";

            using SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            Console.WriteLine("Connection Open!");

            string[] firstnames = new string[]
            {
                "Anna", "Jan", "Maria", "Piotr", "Katarzyna",
                "Michał", "Agnieszka", "Krzysztof", "Magdalena", "Tomasz",
                "Barbara", "Paweł", "Krystyna", "Marcin", "Elżbieta",
                "Marek", "Zofia", "Andrzej", "Teresa", "Karol"
            };

            string[] lastnames = new string[]
            {
                "Nowak", "Kowalski", "Wiśniewski", "Wójcik", "Kowalczyk",
                "Kamiński", "Lewandowski", "Zieliński", "Szymański", "Woźniak",
                "Dąbrowski", "Kozłowski", "Jankowski", "Mazur", "Kwiatkowski",
                "Krawczyk", "Kaczmarek", "Piotrowski", "Grabowski", "Zając"
            };

            string[] cities = new string[]
            {
                "Warszawa", "Kraków", "Łódź", "Wrocław", "Poznań",
                "Gdańsk", "Szczecin", "Bydgoszcz", "Lublin", "Białystok",
                "Katowice", "Gdynia", "Częstochowa", "Radom", "Sosnowiec",
                "Toruń", "Kielce", "Rzeszów", "Gliwice", "Zabrze"
            };

            string[] emails = new string[]
            {
                "user01@example.com", "user02@example.com", "user03@example.com", "user04@example.com",
                "user05@example.com", "user06@example.com", "user07@example.com", "user08@example.com",
                "user09@example.com", "user10@example.com", "user11@example.com", "user12@example.com",
                "user13@example.com", "user14@example.com", "user15@example.com", "user16@example.com",
                "user17@example.com", "user18@example.com", "user19@example.com", "user20@example.com"
            };

            //int totalRecords = 1000000;
            //int batchSize = 100_000;
            Random rnd = new();

            using SqlBulkCopy bulkCopy = new SqlBulkCopy(conn)
            {
                DestinationTableName = "dane",
                BatchSize = batchSize,
                BulkCopyTimeout = 0
            };

            bulkCopy.ColumnMappings.Add("firstname", "firstname");
            bulkCopy.ColumnMappings.Add("lastname", "lastname");
            bulkCopy.ColumnMappings.Add("city", "city");
            bulkCopy.ColumnMappings.Add("email", "email");

            int inserted = 0;

            for (int i = 0; i < totalRecords; i += batchSize)
            {
                using DataTable table = new DataTable();
                table.Columns.Add("firstname", typeof(string));
                table.Columns.Add("lastname", typeof(string));
                table.Columns.Add("city", typeof(string));
                table.Columns.Add("email", typeof(string));

                int currentBatch = Math.Min(batchSize, totalRecords - i);

                for (int j = 0; j < currentBatch; j++)
                {
                    table.Rows.Add(
                        firstnames[rnd.Next(firstnames.Length)],
                        lastnames[rnd.Next(lastnames.Length)],
                        cities[rnd.Next(cities.Length)],
                        emails[rnd.Next(emails.Length)]
                    );
                }

                bulkCopy.WriteToServer(table);
                inserted += currentBatch;
                //Console.WriteLine($"Wstawiono {inserted} / {totalRecords} rekordów...");
            }

            Console.WriteLine("Done.");
        }
    }
}
