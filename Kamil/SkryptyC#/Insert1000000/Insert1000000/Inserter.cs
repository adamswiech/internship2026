using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace Insert1000000
{
    internal class Inserter
    {
        public void Insert()
        {
            DateTime start = DateTime.Now;
            DateTime utcStart = DateTime.UtcNow;



            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

            string insertQuery = @"BULK INSERT [dbo].[users]
           ([Imie]
           ,[DrugieImie]
           ,[Nazwisko]
           ,[IsAdmin]
           ,[Kraj]
           ,[Wojewodztwo]
           ,[Miasto]
           ,[Ulica]
           ,[NrBloku]
           ,[NrMieszkania])
     VALUES
           (@Imie
           ,@DrugieImie
           ,@Nazwisko
           ,@IsAdmin
           ,@Kraj
           ,@Wojewodztwo
           ,@Miasto
           ,@Ulica
           ,@NrBloku
           ,@NrMieszkania)";

            string[] imiona =
            {
                "Jan", "Anna", "Piotr", "Maria", "Tomasz", "Katarzyna", "Michał", "Ola",
                "Adam", "Natalia", "Łukasz", "Karolina", "Paweł", "Ewa", "Grzegorz", "Monika"
            };

            string?[] drugieImiona =
            {
                "Krzysztof", "Marcin", "Zofia",  "Joanna", "Paweł", "Aleksandra",
                "Robert", "Bartosz", "Magdalena", "Szymon", "Janusz", "Beata"
            };

            string[] nazwiska =
            {
                "Kowalski", "Nowak", "Wiśniewski", "Lewandowska", "Zieliński", "Wójcik",
                "Kamiński", "Lis", "Mazur", "Kaczmarek", "Zając", "Król", "Wróbel",
                "Jabłońska", "Pawlak", "Michalska"
            };



            string[] kraje =
            {
                "Polska", "Jugosławia", "Kurdystan", "Serbia", "Palestyna", "Kosovo", "Tajwan", "Watykan",
                "sendinelPółnocny", "Somaliland", "Sudan", "Rodezja", "Włochy", "Chiny", "Czechosłowacja", "Austrowęgry"
            };

            string[] wojewodztwa =
            {
                "Mazowieckie", "Małopolskie", "Wielkopolskie", "Dolnośląskie", "Pomorskie",
                "Łódzkie", "Śląskie", "Podkarpackie", "Lubuskie", "Zachodniopomorskie",
                "Mazowieckie", "Opolskie", "Podlaskie", "Kujawsko-Pomorskie", "Warmińsko-Mazurskie",
                "Świętokrzyskie"
            };

            string[] miasta =
            {
                "Warszawa", "Kraków", "Poznań", "Wrocław", "Gdańsk", "Łódź", "Katowice", "Rzeszów",
                "Zielona Góra", "Szczecin", "Warszawa", "Opole", "Białystok", "Toruń", "Olsztyn", "Kielce","NightCity","Viledor"
            };

            string[] ulice =
            {
                "Marszałkowska", "Floriańska", "Święty Marcin", "Rynek", "Długa", "Piotrkowska",
                "Korfantego", "3 Maja", "Wrocławska", "Wojska Polskiego", "Aleje Jerozolimskie",
                "Kościuszki", "Lipowa", "Mikołaja Kopernika", "Pieniężnego", "Sienkiewicza"
            };

            var rand = new Random();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {

                    DataTable users = new DataTable();
                    users.Columns.Add("Imie", typeof(string));
                    users.Columns.Add("DrugieImie", typeof(string));
                    users.Columns.Add("Nazwisko", typeof(string));
                    users.Columns.Add("IsAdmin", typeof(bool));
                    users.Columns.Add("Kraj", typeof(string));
                    users.Columns.Add("Wojewodztwo", typeof(string));
                    users.Columns.Add("Miasto", typeof(string));
                    users.Columns.Add("Ulica", typeof(string));
                    users.Columns.Add("NrBloku", typeof(long));
                    users.Columns.Add("NrMieszkania", typeof(long));

                    connection.Open();
                    bulkCopy.DestinationTableName = "users"; // target table in DB

                    for (int i = 0; i < 1_000_000; i++)
                    {
                        var imie = imiona[rand.NextInt64(imiona.Length - 1)];
                        var drugieImie = drugieImiona[rand.NextInt64(drugieImiona.Length - 1)];
                        var nazwisko = nazwiska[rand.NextInt64(nazwiska.Length - 1)];
                        var isAdmin = (rand.NextDouble() > 0.5);
                        var kraj = kraje[rand.NextInt64(kraje.Length - 1)];
                        var woj = wojewodztwa[rand.NextInt64(wojewodztwa.Length - 1)];
                        var miasto = miasta[rand.NextInt64(miasta.Length - 1)];
                        var ul = ulice[rand.NextInt64(ulice.Length - 1)];
                        var nrBlok = rand.NextInt64(70);
                        var nrMieszkania = rand.NextInt64(50);
                        users.Rows.Add(imie, drugieImie, nazwisko, isAdmin, kraj, woj, miasto, ul, nrBlok, nrMieszkania);
                        //Console.WriteLine($"{rowsInserted} row(s) inserted successfully.");
                    }
                    try
                    {

                        bulkCopy.ColumnMappings.Add("Imie", "Imie");
                        bulkCopy.ColumnMappings.Add("DrugieImie", "DrugieImie");
                        bulkCopy.ColumnMappings.Add("Nazwisko", "Nazwisko");
                        bulkCopy.ColumnMappings.Add("IsAdmin", "IsAdmin");
                        bulkCopy.ColumnMappings.Add("Kraj", "Kraj");
                        bulkCopy.ColumnMappings.Add("Wojewodztwo", "Wojewodztwo");
                        bulkCopy.ColumnMappings.Add("Miasto", "Miasto");
                        bulkCopy.ColumnMappings.Add("Ulica", "Ulica");
                        bulkCopy.ColumnMappings.Add("NrBloku", "NrBloku");
                        bulkCopy.ColumnMappings.Add("NrMieszkania", "NrMieszkania");

                        bulkCopy.WriteToServer(users);
                        Console.WriteLine("Bulk insert completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                }
                //Console.WriteLine("Local time: " + (DateTime.Now - start));
                //Console.WriteLine("UTC time: " + (DateTime.UtcNow - utcStart));
            }

        }

        public void InsertFromXml()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";
            
            var path = Path.GetFullPath(@"..\..\..\output.xml");

            var doc = XDocument.Load(path);
            DataTable users = ConvertXmlToDataTable(doc);


            BulkInsertDataTable(users, connectionString, "users");
        }
        private DataTable ConvertXmlToDataTable(XDocument doc)
        {
            DataTable users = new DataTable();
            users.Columns.Add("Imie", typeof(string));
            users.Columns.Add("DrugieImie", typeof(string));
            users.Columns.Add("Nazwisko", typeof(string));
            users.Columns.Add("IsAdmin", typeof(bool));
            users.Columns.Add("Kraj", typeof(string));
            users.Columns.Add("Wojewodztwo", typeof(string));
            users.Columns.Add("Miasto", typeof(string));
            users.Columns.Add("Ulica", typeof(string));
            users.Columns.Add("NrBloku", typeof(long));
            users.Columns.Add("NrMieszkania", typeof(long));

            var usersOb = doc.Root.Elements("User");
            foreach(var u in usersOb)
            {
                DataRow row = users.NewRow();

                row["IsAdmin"] = (bool?)u.Element("IsAdmin") ?? false;

                var nazwa = u.Element("Nazwa");
                row["Imie"] = nazwa?.Element("Imie")?.Value ?? "";
                row["DrugieImie"] = nazwa?.Element("DrugieImie")?.Value ?? "";
                row["Nazwisko"] = nazwa?.Element("Nazwisko")?.Value ?? "";

                var adres = u.Element("Adres");
                row["Kraj"] = adres?.Element("Kraj")?.Value ?? "";
                row["Wojewodztwo"] = adres?.Element("Wojewodztwo")?.Value ?? "";
                row["Miasto"] = adres?.Element("Miasto")?.Value ?? "";

                var mieszkanie = adres?.Element("Mieszkanie");
                row["Ulica"] = mieszkanie?.Element("Ulica")?.Value ?? "";
                row["NrBloku"] = mieszkanie?.Element("NrBloku")?.Value ?? "";
                row["NrMieszkania"] = mieszkanie?.Element("NrMieszkania")?.Value ?? "";
                users.Rows.Add(row);
            }

            return users;
        }

        private void BulkInsertDataTable(DataTable table, string connectionString, string destinationTable)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = destinationTable;

                    bulkCopy.ColumnMappings.Add("Imie", "Imie");
                    bulkCopy.ColumnMappings.Add("DrugieImie", "DrugieImie");
                    bulkCopy.ColumnMappings.Add("Nazwisko", "Nazwisko");
                    bulkCopy.ColumnMappings.Add("IsAdmin", "IsAdmin");
                    bulkCopy.ColumnMappings.Add("Kraj", "Kraj");
                    bulkCopy.ColumnMappings.Add("Wojewodztwo", "Wojewodztwo");
                    bulkCopy.ColumnMappings.Add("Miasto", "Miasto");
                    bulkCopy.ColumnMappings.Add("Ulica", "Ulica");
                    bulkCopy.ColumnMappings.Add("NrBloku", "NrBloku");
                    bulkCopy.ColumnMappings.Add("NrMieszkania", "NrMieszkania");

                    bulkCopy.WriteToServer(table);
                    Console.WriteLine("Bulk insert succes");
                }
            }
        }
    }
}
