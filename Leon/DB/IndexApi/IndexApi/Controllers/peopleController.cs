using IndexApi.Services;
using IndexApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace IndexApi.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly peopleServices _service;

        public PeopleController(peopleServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedDTO<Person>>> GetPeople(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? firstName = null,
            [FromQuery] string? lastName = null,
            [FromQuery] string? orderBy = null)
        {
            if (pageSize < 1)
            {
                return BadRequest("page size must be greater than 0");
            }
            if (page < 1)
            {
                return BadRequest("page must be greater than 0");
            }
            var result = await _service.GetPeople(page, pageSize, firstName, lastName, orderBy);
            return Ok(result);
        }
    }
}
