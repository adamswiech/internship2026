using Microsoft.Data.SqlClient;

namespace csvConverter
{
    public class CSV_converter
    {
        private readonly string _connection_string;
        private List<PersonalDataModel> _personalData = new List<PersonalDataModel>();

        public CSV_converter(string ConnectionString)
        {
            _connection_string = ConnectionString;
        }

        public void fetchData()
        {
            string query = "SELECT * FROM PersonalData";
            using var conn = new SqlConnection(_connection_string);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                _personalData.Add(new PersonalDataModel
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    lastName = reader.GetString(reader.GetOrdinal("LastName")),
                    phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                    emailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                    country = reader.GetString(reader.GetOrdinal("Country")),
                    city = reader.GetString(reader.GetOrdinal("City")),
                    postCode = reader.GetString(reader.GetOrdinal("PostCode")),
                    gender = reader.GetString(reader.GetOrdinal("Gender")),
                    age = reader.GetInt32(reader.GetOrdinal("Age")),
                });
            }
        }

        public void createCSV()
        {
            try
            {
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                string outputFile = Path.Combine(projectRoot, "out.csv");
                File.WriteAllText(outputFile, string.Empty);

                using (StreamWriter writer = new StreamWriter(outputFile, append: true))
                {
                    writer.WriteLine("FirstName;LastName;PhoneNumber;EmailAddress;Country;City;PostCode;Gender;Age");
                    foreach (var personalData in _personalData)
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
