using Insert1000000.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace Insert1000000
{
    internal class Selector
    {   
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";
        
        
        
        public void SelectToXML()
        {
            string query = "select * from users ";
            var path = Path.GetFullPath(@"..\..\..\output.xml");

            this.SelectToXML(query, path);
        }

        public void SelectToXML(string query, string path)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                { 
                    List<User> users = new List<User>();
                    connection.Open();
                    File.WriteAllText(path, "");
                    SqlDataReader reader = command.ExecuteReader();

                    object[] output = new object[11];

                    using (StreamWriter sw = new StreamWriter(path, append: true))
                    {
                        while (reader.Read())
                        {
                            reader.GetValues(output);
                            users.Add(new User()
                            {
                                Id = (int)output[0],
                                Nazwa = new Nazwa()
                                {
                                    Imie = (string)output[1],
                                    DrugieImie = (string)output[2],
                                    Nazwisko = (string)output[3],
                                },
                                IsAdmin = (bool)output[4],
                                Adres = new Adres()
                                {
                                    Kraj = (string)output[5],
                                    Wojewodztwo = (string)output[6],
                                    Miasto = (string)output[7],
                                    Mieszkanie = new Mieszkanie()
                                    {
                                        Ulica = (string)output[8],
                                        NrBloku = (int)output[9],
                                        NrMieszkania = (int)output[10]
                                    }
                                }
                            });
                        }
                    }
                    reader.Close();
                    XmlSerializer serializer = new XmlSerializer(typeof(List<User>),new XmlRootAttribute("Users"));
                    File.WriteAllText(path, "");
                    using (StreamWriter sw = new StreamWriter(path, append: true))
                        serializer.Serialize(sw, users);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }




        public void Select()
        {
            string query = "select  * from users";
            var path = Path.GetFullPath(@"..\..\..\output.csv");
            this.Select(query, path);
        }
        public void Select(string query, string path)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    
                    connection.Open();
                    File.WriteAllText(path, "");
                    SqlDataReader reader = command.ExecuteReader();

                    object[] output = new object[reader.FieldCount];
                    var firstRecord = true;
                    for (int i = 0; i < reader.FieldCount; i++)
                        output[i] = reader.GetName(i);
                    using (StreamWriter sw = new StreamWriter(path, append: true))
                    {
                        //Console.WriteLine(path);
                        while (reader.Read())
                        {
                            reader.GetValues(output);
                            sw.WriteLine(String.Join(";", output));
                        }
                    }
                    
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }
        public void SelectMultiThread(int threadCount)
        {

            long rowsNumber = 0;
            List<string> querys = new List<string>();
            List<string> filesPaths = new List<string>();
            List<Thread> threads = new List<Thread>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryCountRows = @"
                    SELECT SUM(p.row_count) AS TotalRows
                    FROM sys.dm_db_partition_stats AS p
                    WHERE p.object_id = OBJECT_ID('dbo.users')AND p.index_id IN (0,1);";
                SqlCommand command = new SqlCommand(queryCountRows, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                        rowsNumber =  reader.GetInt64(0);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            } //get rowsNumber

            long rowsSelectedOnThread = rowsNumber / threadCount;
            for (int i = 0; i < threadCount; i++)
            {
                string query = "";
                if (i == threadCount - 1)
                    query = $"select * from users ORDER BY id OFFSET {rowsSelectedOnThread * i} ROWS"; // last thread selects all of remaining rows
                else
                    query = $"SELECT * FROM users ORDER BY id OFFSET {rowsSelectedOnThread * i} ROWS FETCH NEXT {rowsSelectedOnThread} ROWS ONLY";

                string path = Path.GetFullPath(@"..\..\..\output" + i.ToString() + ".csv");
                filesPaths.Add(path);
                threads.Add(new Thread(() => Select(new String(query), path))); ;
            } //prepere tasks

            foreach (Thread t in threads)
                t.Start();

            foreach (Thread t in threads)
                t.Join();


            string destFilePath = Path.GetFullPath(@"..\..\..\output.csv");
            File.WriteAllText(destFilePath, "");
            using (TextWriter tw = new StreamWriter(destFilePath, true))
            {
                foreach (string filePath in filesPaths)
                {
                    using (TextReader tr = new StreamReader(filePath))
                    {
                        tw.WriteLine(tr.ReadToEnd());
                        tr.Close();
                        tr.Dispose();
                    }
                    //Console.WriteLine("File Processed : " + filePath);
                }

                tw.Close();
                tw.Dispose();
            } //merge files
        }


        public void Select2(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    object[] output = new object[reader.FieldCount];
                    var firstRecord = true;
                    for (int i = 0; i < reader.FieldCount; i++)
                        output[i] = reader.GetName(i);


                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }
    }
}
