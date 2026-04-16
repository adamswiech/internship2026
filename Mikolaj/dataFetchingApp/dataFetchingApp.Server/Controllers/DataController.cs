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

        [HttpGet("filterDataByLastName")]
        public async Task<ActionResult<List<PersonalDataModel>>> FilterPersonalDataByLastName(int offset, int limit, string lastName)
        {
            var personsList = await _db.PersonalDataSet
                .FromSqlRaw(@"
            SELECT * 
            FROM PersonalData 
            WHERE LastName LIKE @Value1
            ORDER BY Id
            OFFSET @offset ROWS 
            FETCH NEXT @limit ROWS ONLY",
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@limit", limit),
                    new SqlParameter("@Value1", $"{lastName}%"))
                .AsNoTracking()
                .ToListAsync();

            return personsList;
        }


    }
}
