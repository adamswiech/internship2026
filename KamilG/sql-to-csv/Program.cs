using Microsoft.Data.SqlClient;
using sql_to_csv;
using System;
using System.Diagnostics;
using System.Xml;

internal class Program
{
    static void Main()
    {
        Connect();
        Console.ReadKey();
    }

    static void Connect()
    {
        string constr;

        SqlConnection conn;

        constr = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=""SQL Server Management Studio"";Command Timeout=0";

        conn = new SqlConnection(constr);

        //// to open the connection
        conn.Open();
        Console.WriteLine("Connection Open!");
        string csvPath = @"C:\Users\VULCAN\Documents\repo\internship2026\KamilG\sql-to-csv\export.csv";
        string Xmlpath = @"C:\Users\VULCAN\Documents\repo\internship2026\KamilG\sql-to-csv\export.xml";



        // DB -> CSV generator
        //Stopwatch stopWatch1 = new Stopwatch();
        //stopWatch1.Start();
        //sql_to_csv.Convert.ConvertToCsv(constr, csvPath);
        //stopWatch1.Stop();
        //TimeSpan Time1 = stopWatch1.Elapsed;
        //Console.WriteLine("Single: " + Time1);

        //DB -> XML generator
        //sql_to_csv.xml.xmlE(constr, "SELECT * FROM dane;", Xmlpath);


        // XML -> DB generator
        sql_to_csv.xmlToDb.xmlToDbE(conn, Xmlpath);



        //using (XmlWriter writer = XmlWriter.Create("books.xml"))
        //{
        //    writer.WriteStartElement("book");
        //    writer.WriteElementString("title", "Graphics Programming using GDI+");
        //    writer.WriteElementString("author", "Mahesh Chand");
        //    writer.WriteStartElement("publication");
        //    writer.WriteElementString("publisher", "Addison-Wesley");
        //    writer.WriteEndElement();
        //    writer.WriteElementString("price", "64.95");
        //    Console.WriteLine("XML file created successfully.");
        //    writer.WriteEndElement();
        //    writer.Flush();
        //}
    }
}