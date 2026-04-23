using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Xml.Linq;

namespace sql_to_csv
{
    internal class xmlToDb
    {
        public static void xmlToDbE(SqlConnection conn, string path)
        {
            var file = XDocument.Load(path);
            var users = file.Root?.Elements("User");
            if (users is null)
            {
                Console.WriteLine("Brak danych do zapisania");
                return;
            }

            var table = new DataTable();
            table.Columns.Add("firstname", typeof(string));
            table.Columns.Add("lastname", typeof(string));
            table.Columns.Add("city", typeof(string));
            table.Columns.Add("email", typeof(string));

            foreach (var user in users)
            {
                table.Rows.Add(
                    (object?)user.Element("Name")?.Element("FirstName")?.Value ?? DBNull.Value,
                    (object?)user.Element("Name")?.Element("LastName")?.Value ?? DBNull.Value,
                    (object?)user.Element("Location")?.Value ?? DBNull.Value,
                    (object?)user.Element("ContactInfo")?.Element("Email")?.Value ?? DBNull.Value
                );
            }

            using var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, null)
            {
                DestinationTableName = "dane",
                BatchSize = 5000,
                BulkCopyTimeout = 0
            };

            bulkCopy.ColumnMappings.Add("firstname", "firstname");
            bulkCopy.ColumnMappings.Add("lastname", "lastname");
            bulkCopy.ColumnMappings.Add("city", "city");
            bulkCopy.ColumnMappings.Add("email", "email");

            bulkCopy.WriteToServer(table);
            Console.WriteLine("Zapisane do DB");
        }
    }
}
