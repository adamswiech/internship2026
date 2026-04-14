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
    }
}
