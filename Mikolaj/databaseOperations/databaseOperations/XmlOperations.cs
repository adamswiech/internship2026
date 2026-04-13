using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace databaseOperations
{
    public class XmlOperations
    {
        private readonly string _connectionString;

        public XmlOperations(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task loadFile(string xmlFilePath)
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

    }
}
