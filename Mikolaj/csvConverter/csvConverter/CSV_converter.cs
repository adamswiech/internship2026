using Microsoft.Data.SqlClient;
using System.Xml.Serialization;

namespace csvConverter
{
    public class CSV_converter
    {
        private readonly string _connection_string;

        public CSV_converter(string ConnectionString)
        {
            _connection_string = ConnectionString;
        }

        public long countElements()
        {
            string query = "SELECT SUM(p.row_count) AS TotalRows FROM sys.dm_db_partition_stats AS p WHERE p.object_id = OBJECT_ID('dbo.PersonalData')AND p.index_id IN (0,1);";
            using var conn = new SqlConnection(_connection_string);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            long totalRows = 0;

            if (reader.Read())
            {
                int ordinal = reader.GetOrdinal("TotalRows");
                totalRows = reader.GetInt64(ordinal);
            }

            conn.Close();

            return totalRows;
        }

        public void fetchData(long offset, long limit, int filesCount)
        {
            //addde some amendments to change code to export in xml
            string query = "";

            if (offset == 0)
            {
                query = $"SELECT * FROM PersonalData;";
            }
            else
            {
                query = $"SELECT * FROM PersonalData ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
            }

            using var conn = new SqlConnection(_connection_string);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            string outputFile = Path.Combine(projectRoot, "out.xml");
            File.WriteAllText(outputFile, string.Empty);

            using var reader = cmd.ExecuteReader();
            //List<PersonalDataModel> personalData = new List<PersonalDataModel>();

            using (StreamWriter writer = new StreamWriter(outputFile, append: true))
            {
                List<Person> personalData = new List<Person>();
                while (reader.Read())
                {
                    //PersonalDataModel pdm;
                    //personalData.Add(pdm = new PersonalDataModel
                    //{
                    //    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    //    firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    //    lastName = reader.GetString(reader.GetOrdinal("LastName")),
                    //    phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                    //    emailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                    //    country = reader.GetString(reader.GetOrdinal("Country")),
                    //    city = reader.GetString(reader.GetOrdinal("City")),
                    //    postCode = reader.GetString(reader.GetOrdinal("PostCode")),
                    //    gender = reader.GetString(reader.GetOrdinal("Gender")),
                    //    age = reader.GetInt32(reader.GetOrdinal("Age")),
                    //});

                    var person = new Person
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        PersonalData = new PersonalInfo
                        {
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age"))
                        },
                        PrivateAddress = new Address
                        {
                            Country = reader.GetString(reader.GetOrdinal("Country")),
                            City = reader.GetString(reader.GetOrdinal("City")),
                            PostCode = reader.GetString(reader.GetOrdinal("PostCode"))
                        },
                        ContactData = new Contact
                        {
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress"))
                        }
                    };

                    personalData.Add(person);



                }

                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(personalData.GetType(), new XmlRootAttribute("PersonsList"));
                x.Serialize(writer, personalData);

                conn.Close();
                reader.Close();


                //createCSV(filesCount, personalData);

                //this is version of code that support exporting data from db to xml 
                //version with exporting data from db to csv is avaliable on gh in commit "Saving data from db to CSV file works" in branch main.
            }
        }

        private void createCSV(int filesCount, List<PersonalDataModel> personalDataArray)
        {
            try
            {
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                string outputFile = Path.Combine(projectRoot, $"out{filesCount}.csv");
                File.WriteAllText(outputFile, string.Empty);

                using (StreamWriter writer = new StreamWriter(outputFile, append: true))
                {
                    if (filesCount == 1) writer.WriteLine("FirstName;LastName;PhoneNumber;EmailAddress;Country;City;PostCode;Gender;Age");
                    foreach (var personalData in personalDataArray)
                    {
                        writer.WriteLine($"{personalData.id};{personalData.firstName};{personalData.lastName};{personalData.phoneNumber};{personalData.emailAddress};{personalData.country};{personalData.city};{personalData.postCode};{personalData.gender};{personalData.age}");
                    }

                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
