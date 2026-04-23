using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace sql_to_csv
{
    
    public class xml
    {
        public static void xmlE( string conn, string que, string path)
        {
            var dataAdapter = new SqlDataAdapter(que,conn);
            {
                var dataset = new DataTable("data");
                dataAdapter.Fill(dataset);
                //dataset.WriteXml(path, XmlWriteMode.WriteSchema); Bez zagnieżdzeń

                var root = new XElement(
                    "Users", from DataRow row in dataset.Rows
                             select new XElement("User",
                         new XAttribute("id", row["id"]),

                         new XElement("Name",
                             new XElement("FirstName", row["firstname"]),
                             new XElement("LastName", row["lastname"])
                         ),

                         new XElement("Location", row["city"]),

                         new XElement("ContactInfo",
                             new XElement("Email", row["email"])
                         )
                     )
        
                    );

                new XDocument(root).Save(path);
            }
            Console.WriteLine("Zapisane XML");
        }
        
    }
}
