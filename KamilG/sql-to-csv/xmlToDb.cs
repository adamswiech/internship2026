using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace sql_to_csv
{
    internal class xmlToDb
    {
        public static void xmlToDbE(SqlConnection conn, string path)
        {
            var file = XDocument.Load(path);
            var qu = @"Insert into dane (firstname, lastname, city, email) values (@firstname, @lastname, @city, @email)";
            //using var connection = new SqlConnection(conn);
            using var cmd = new SqlCommand(qu, conn);
            foreach (var user in file.Root.Elements("User"))
            {
                cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@id", (int)user.Attribute("id"));
                cmd.Parameters.AddWithValue("@firstname", (string)user.Element("Name").Element("FirstName"));
                cmd.Parameters.AddWithValue("@lastname", (string)user.Element("Name").Element("LastName"));
                cmd.Parameters.AddWithValue("@city", (string)user.Element("Location"));
                cmd.Parameters.AddWithValue("@email", (string)user.Element("ContactInfo").Element("Email"));
                cmd.ExecuteNonQuery();
            }
             Console.WriteLine("Zapisane do DB");
        }
    }
}
