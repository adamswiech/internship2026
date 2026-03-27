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

        public void Insert(List<PersonalDataModel> people)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "dbo.PersonalData";

            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.firstName), "FirstName");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.lastName), "LastName");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.phoneNumber), "PhoneNumber");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.emailAddress), "EmailAddress");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.country), "Country");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.city), "City");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.postCode), "PostCode");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.gender), "Gender");
            bulkCopy.ColumnMappings.Add(nameof(PersonalDataModel.age), "Age");

            bulkCopy.WriteToServer(ToDataTable(people));
        }

        private DataTable ToDataTable(List<PersonalDataModel> people)
        {
            var table = new DataTable();

            table.Columns.Add("firstName", typeof(string));
            table.Columns.Add("lastName", typeof(string));
            table.Columns.Add("phoneNumber", typeof(string));
            table.Columns.Add("emailAddress", typeof(string));
            table.Columns.Add("country", typeof(string));
            table.Columns.Add("city", typeof(string));
            table.Columns.Add("postCode", typeof(string));
            table.Columns.Add("gender", typeof(string));
            table.Columns.Add("age", typeof(int));

            foreach (var p in people)
            {
                table.Rows.Add(
                    p.firstName,
                    p.lastName,
                    p.phoneNumber,
                    p.emailAddress,
                    p.country,
                    p.city,
                    p.postCode,
                    p.gender,
                    p.age
                );
            }

            return table;
        }
    }
}