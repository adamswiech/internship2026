using Microsoft.Data.SqlClient;

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
            string query = $"SELECT * FROM PersonalData ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
            using var conn = new SqlConnection(_connection_string);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            List<PersonalDataModel> personalData = new List<PersonalDataModel>();

            while (reader.Read())
            {
                personalData.Add(new PersonalDataModel
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

            createCSV(filesCount, personalData);
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
