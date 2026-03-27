using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.SqlClient;

namespace sql_to_csv
{
    public class Convert
    {
        public static void ConvertToCsv()
        {
            const string constr = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=""SQL Server Management Studio"";Command Timeout=0";
            string query = "SELECT * FROM dane;";
            string outputPath = @"C:\Users\VULCAN\Documents\repo\internship2026\KamilG\sql-to-csv\export.csv";

            ExportQueryToCsv(constr, query, outputPath);
            Console.WriteLine($"Zapisano CSV: {outputPath}");
        }

        static void ExportQueryToCsv(string connectionString, string query, string outputPath)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            using var writer = new StreamWriter(outputPath, false, new UTF8Encoding(true));

            // Nagłówki
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (i > 0) writer.Write(",");
                writer.Write(EscapeCsv(reader.GetName(i)));
            }
            writer.WriteLine();

            // Wiersze
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i > 0) writer.Write(",");
                    string value = reader.IsDBNull(i) ? "" : System.Convert.ToString(reader.GetValue(i)) ?? "";
                    writer.Write(EscapeCsv(value));
                }
                writer.WriteLine();
            }
        }

        static string EscapeCsv(string value)
        {
            if (value.Contains('"'))
                value = value.Replace("\"", "\"\"");

            if (value.Contains(',') || value.Contains('\n') || value.Contains('\r') || value.Contains('"'))
                return $"\"{value}\"";

            return value;
        }
    }
}
