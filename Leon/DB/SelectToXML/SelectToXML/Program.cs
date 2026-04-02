using Microsoft.Data.SqlClient;
using SelectToXML;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;";
const string query = @"USE [InterDB];
SELECT [id],[first_name],[middle_name],[last_name],[age],
       [height_cm],[weight_kg],[city],[country],[favorite_number]
FROM [dbo].[people]";

string outputFilePath = Path.GetFullPath(@"..\..\..\db.xml");

if (File.Exists(outputFilePath))
{
    File.Delete(outputFilePath);
}

//Stopwatch sw = Stopwatch.StartNew();

//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    connection.Open();

//    using (SqlCommand cmd = new SqlCommand(query, connection))
//    using (SqlDataReader reader = cmd.ExecuteReader())
//    using (XmlWriter writer = XmlWriter.Create(outputFilePath, new XmlWriterSettings { Indent = true }))
//    {
//        writer.WriteStartDocument();
//        writer.WriteStartElement("People");

//        while (reader.Read())
//        {
//            writer.WriteStartElement("Person");


//            writer.WriteElementString("Id", reader["id"].ToString());


//            writer.WriteStartElement("Name");
//            writer.WriteElementString("First", reader["first_name"].ToString());
//            writer.WriteElementString("Middle", reader["middle_name"].ToString());
//            writer.WriteElementString("Last", reader["last_name"].ToString());
//            writer.WriteEndElement();


//            writer.WriteStartElement("Location");
//            writer.WriteElementString("City", reader["city"].ToString());
//            writer.WriteElementString("Country", reader["country"].ToString());
//            writer.WriteEndElement();


//            writer.WriteElementString("Age", reader["age"].ToString());
//            writer.WriteElementString("HeightCm", reader["height_cm"].ToString());
//            writer.WriteElementString("WeightKg", reader["weight_kg"].ToString());
//            writer.WriteElementString("FavoriteNumber", reader["favorite_number"].ToString());

//            writer.WriteEndElement();
//        }

//        writer.WriteEndElement();
//        writer.WriteEndDocument();
//    }
//}

//sw.Stop();
//Console.WriteLine($"Time: {sw.Elapsed.TotalSeconds:F2}");





const string query2 = @"USE [InterDB];
SELECT [id],[first_name],[middle_name],[last_name],[age],
       [height_cm],[weight_kg],[city],[country],[favorite_number]
FROM [dbo].[people]";

string outputFile = Path.GetFullPath(@"..\..\..\out.xml");

using (var conn = new SqlConnection(connectionString))
{
    conn.Open();

    using (var cmd = new SqlCommand(query2, conn))
    using (var reader = cmd.ExecuteReader())
    using (var writer = XmlWriter.Create(outputFile, new XmlWriterSettings { Indent = true }))
    {
        writer.WriteStartDocument();
        writer.WriteStartElement("People");

        while (reader.Read())
        {
            writer.WriteStartElement("Person");

            WriteIfExists(reader, writer, "id");

            writer.WriteStartElement("Name");
            WriteIfExists(reader, writer, "first_name");
            WriteIfExists(reader, writer, "middle_name");
            WriteIfExists(reader, writer, "last_name");
            writer.WriteEndElement();

            writer.WriteStartElement("Location");
            WriteIfExists(reader, writer, "city");
            WriteIfExists(reader, writer, "country");
            writer.WriteEndElement();

            writer.WriteStartElement("Stats");
            WriteIfExists(reader, writer, "age");
            WriteIfExists(reader, writer, "height_cm");
            WriteIfExists(reader, writer, "weight_kg");
            WriteIfExists(reader, writer, "favorite_number");
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
}

static void WriteIfExists(SqlDataReader reader, XmlWriter writer, string column)
{
    if (!reader.HasRows) return;

    int ordinal;
    try
    {
        ordinal = reader.GetOrdinal(column);
    }
    catch
    {
        return;
    }

    if (!reader.IsDBNull(ordinal))
    {
        writer.WriteElementString(column, reader.GetValue(ordinal).ToString());
    }
}
