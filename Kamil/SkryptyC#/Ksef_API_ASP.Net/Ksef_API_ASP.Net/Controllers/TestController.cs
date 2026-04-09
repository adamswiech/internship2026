using Ksef.Models;
using Ksef_API_ASP.Net.Models;
using Ksef_ASP.net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Ksef_ASP.net.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Json(new object());
    }
    [HttpGet("GetTest")]
    public ActionResult<List<FakturaDTO>> GetFaktura()
    {
        return new List<FakturaDTO>();
    }
    [HttpGet("GetPodmiot")]
    public ActionResult<PodmiotDTO> GetPodmiot()
    {
        return new PodmiotDTO(){
            Nip = "DE123456789",
            Nazwa = "German Client GmbH",
            KodKraju = "DE",
            AdresL1 = "Musterstrasse 10, 10115 Berlin"
        };
    }

}
