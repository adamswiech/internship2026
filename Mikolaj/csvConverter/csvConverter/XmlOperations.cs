using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace csvConverter
{
    public class XmlOperations
    {
        private readonly string _connectionString;

        public XmlOperations(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task loadFileToDb(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var personsList = doc.Descendants("Person")
                .Select(person => new PersonalDataModel
                {
                    firstName = (string)person.Element("PersonalData").Element("FirstName"),
                    lastName = (string)person.Element("PersonalData").Element("LastName"),
                    gender = (string)person.Element("PersonalData").Element("Gender"),
                    age = (int)person.Element("PersonalData").Element("Age"),
                    country = (string)person.Element("PrivateAddress").Element("Country"),
                    city = (string)person.Element("PrivateAddress").Element("City"),
                    postCode = (string)person.Element("PrivateAddress").Element("PostCode"),
                    phoneNumber = (string)person.Element("ContactData").Element("PhoneNumber"),
                    emailAddress = (string)person.Element("ContactData").Element("EmailAddress")
                }).ToList();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var person in personsList)
                {
                    var cmd = new SqlCommand(@"
                INSERT INTO PersonalData (FirstName, LastName, PhoneNumber, EmailAddress, Country, City, PostCode, Gender, Age)
                VALUES (@FirstName, @LastName, @PhoneNumber, @EmailAddress, @Country, @City, @PostCode, @Gender, @Age)",
                        connection, transaction);
                    cmd.Parameters.AddWithValue("@FirstName", person.firstName);
                    cmd.Parameters.AddWithValue("@LastName", person.lastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", person.phoneNumber);
                    cmd.Parameters.AddWithValue("@EmailAddress", person.emailAddress);
                    cmd.Parameters.AddWithValue("@Country", person.country);
                    cmd.Parameters.AddWithValue("@City", person.city);
                    cmd.Parameters.AddWithValue("@PostCode", person.postCode);
                    cmd.Parameters.AddWithValue("@Gender", person.gender);
                    cmd.Parameters.AddWithValue("@Age", person.age);

                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error: {exception.Message}");
                throw;
            }
        }

        public async Task<List<PersonalDataModel>> selectByIndex(string searchValue, string columnName)
        {
            var allowedColumns = new HashSet<string>
            {
            "FirstName", "LastName", "PhoneNumber", "EmailAddress",
            "Country", "City", "PostCode", "Gender", "Age"
            };

            var columns = columnName.Split(',').Select(c => c.Trim()).ToList();
            var values = searchValue.Split(',').Select(v => v.Trim()).ToList();

            // Validate all columns
            foreach (var col in columns)
            {
                if (!allowedColumns.Contains(col))
                    throw new ArgumentException($"Invalid column name: {col}");
            }

            if (columns.Count != values.Count)
                throw new ArgumentException("Column count and value count must match.");

            // Build WHERE clause: "LastName LIKE @Value0 AND FirstName LIKE @Value1"
            var whereParts = columns.Select((col, i) => $"{col} LIKE @Value{i}");
            var whereClause = string.Join(" AND ", whereParts);

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var results = new List<PersonalDataModel>();

            try
            {
                var selectCmd = new SqlCommand($"SELECT * FROM PersonalData WHERE {whereClause}", connection);

                for (int i = 0; i < values.Count; i++)
                {
                    selectCmd.Parameters.AddWithValue($"@Value{i}", values[i] + "%");
                }

                using (var reader = await selectCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(new PersonalDataModel
                        {
                            firstName = reader["FirstName"].ToString(),
                            lastName = reader["LastName"].ToString(),
                            phoneNumber = reader["PhoneNumber"].ToString(),
                            emailAddress = reader["EmailAddress"].ToString(),
                            country = reader["Country"].ToString(),
                            city = reader["City"].ToString(),
                            postCode = reader["PostCode"].ToString(),
                            gender = reader["Gender"].ToString(),
                            age = Convert.ToInt32(reader["Age"])
                        });
                    }
                }

                return results;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                throw;
            }
        }

        public async Task createNewIdx(string connectionString, string createIndexSql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(createIndexSql, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Index created successfully.");
                }
            }
        }


    }
}
