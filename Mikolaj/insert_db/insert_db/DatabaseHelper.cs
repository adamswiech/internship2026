using Microsoft.Data.SqlClient;
using System.Data;

namespace insert_db
{
    internal class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertPerson(PersonalDataModel person)
        {
            string query = @"INSERT INTO PersonalData 
                           (FirstName, LastName, PhoneNumber, EmailAddress, Country, City, PostCode, Gender, Age) 
                           VALUES 
                           (@FirstName, @LastName, @PhoneNumber, @EmailAddress, @Country, @City, @PostCode, @Gender, @Age)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = person.firstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = person.lastName;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50).Value = person.phoneNumber;
            command.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = person.emailAddress;
            command.Parameters.Add("@Country", SqlDbType.NVarChar, 100).Value = person.country;
            command.Parameters.Add("@City", SqlDbType.NVarChar, 100).Value = person.city;
            command.Parameters.Add("@PostCode", SqlDbType.NVarChar, 20).Value = person.postCode;
            command.Parameters.Add("@Gender", SqlDbType.NVarChar, 50).Value = person.gender;
            command.Parameters.Add("@Age", SqlDbType.Int).Value = person.age;

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Insert(List<PersonalDataModel> people)
        {
            string query = @"INSERT INTO PersonalData 
                           (FirstName, LastName, PhoneNumber, EmailAddress, Country, City, PostCode, Gender, Age) 
                           VALUES 
                           (@FirstName, @LastName, @PhoneNumber, @EmailAddress, @Country, @City, @PostCode, @Gender, @Age)";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            using var command = new SqlCommand(query, connection, transaction);

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200);
            command.Parameters.Add("@Country", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@PostCode", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Gender", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Age", SqlDbType.Int);

            try
            {
                foreach (var person in people)
                {
                    command.Parameters["@FirstName"].Value = person.firstName;
                    command.Parameters["@LastName"].Value = person.lastName;
                    command.Parameters["@PhoneNumber"].Value = person.phoneNumber;
                    command.Parameters["@EmailAddress"].Value = person.emailAddress;
                    command.Parameters["@Country"].Value = person.country;
                    command.Parameters["@City"].Value = person.city;
                    command.Parameters["@PostCode"].Value = person.postCode;
                    command.Parameters["@Gender"].Value = person.gender;
                    command.Parameters["@Age"].Value = person.age;

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}