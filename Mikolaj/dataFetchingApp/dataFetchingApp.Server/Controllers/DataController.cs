using dataFetchingApp.Server.Data;
using dataFetchingApp.Server.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<PersonalDataModel>> fetchData(int offset)
        {
            return _db.PersonalDataSet.ToList();
        }
    }
}
