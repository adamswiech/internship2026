using dataFetchingApp.Server.Data;
using dataFetchingApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace dataFetchingApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DataController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DataController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("fetchData")]
        public async Task<ActionResult<List<PersonalDataModel>>> FetchPersonalData(int offset, int limit)
        {
            var personsList = await _db.PersonalDataSet
                .FromSqlRaw(@"
            SELECT * 
            FROM PersonalData 
            ORDER BY Id
            OFFSET @offset ROWS 
            FETCH NEXT @limit ROWS ONLY",
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@limit", limit))
                .AsNoTracking()
                .ToListAsync();

            return personsList;
        }

        [HttpGet("filterData")]
        public async Task<ActionResult<List<PersonalDataModel>>> FilterPersonalData(int offset, int limit, string firstName, string lastName)
        {
            var personsList = await _db.PersonalDataSet
                .FromSqlRaw(@"
            SELECT * 
            FROM PersonalData 
            WHERE FirstName LIKE @Value0 AND LastName LIKE @Value1
            ORDER BY Id
            OFFSET @offset ROWS 
            FETCH NEXT @limit ROWS ONLY",
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@limit", limit),
                    new SqlParameter("@Value0", $"{firstName}%"),
                    new SqlParameter("@Value1", $"{lastName}%"))
                .AsNoTracking()
                .ToListAsync();

            return personsList;
        }
    }
}


//public async Task<List<PersonalDataModel>> selectByIndex(string searchValue, string columnName)
//{
//    var allowedColumns = new HashSet<string>
//            {
//            "FirstName", "LastName", "PhoneNumber", "EmailAddress",
//            "Country", "City", "PostCode", "Gender", "Age"
//            };

//    var columns = columnName.Split(',').Select(c => c.Trim()).ToList();
//    var values = searchValue.Split(',').Select(v => v.Trim()).ToList();

//    // Validate all columns
//    foreach (var col in columns)
//    {
//        if (!allowedColumns.Contains(col))
//            throw new ArgumentException($"Invalid column name: {col}");
//    }

//    if (columns.Count != values.Count)
//        throw new ArgumentException("Column count and value count must match.");

//    // Build WHERE clause: "LastName LIKE @Value0 AND FirstName LIKE @Value1"
//    var whereParts = columns.Select((col, i) => $"{col} LIKE @Value{i}");
//    var whereClause = string.Join(" AND ", whereParts);

//    using var connection = new SqlConnection(_connectionString);
//    await connection.OpenAsync();

//    var results = new List<PersonalDataModel>();

//    try
//    {
//        var selectCmd = new SqlCommand($"SELECT * FROM PersonalData WHERE {whereClause}", connection);

//        for (int i = 0; i < values.Count; i++)
//        {
//            selectCmd.Parameters.AddWithValue($"@Value{i}", values[i] + "%");
//        }

//        using (var reader = await selectCmd.ExecuteReaderAsync())
//        {
//            while (await reader.ReadAsync())
//            {
//                results.Add(new PersonalDataModel
//                {
//                    firstName = reader["FirstName"].ToString(),
//                    lastName = reader["LastName"].ToString(),
//                    phoneNumber = reader["PhoneNumber"].ToString(),
//                    emailAddress = reader["EmailAddress"].ToString(),
//                    country = reader["Country"].ToString(),
//                    city = reader["City"].ToString(),
//                    postCode = reader["PostCode"].ToString(),
//                    gender = reader["Gender"].ToString(),
//                    age = Convert.ToInt32(reader["Age"])
//                });
//            }
//        }

//        return results;
//    }
//    catch (Exception exception)
//    {
//        Console.WriteLine($"Error: {exception.Message}");
//        throw;
//    }
//}