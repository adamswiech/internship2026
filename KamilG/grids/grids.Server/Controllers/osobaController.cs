using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using grids.Server;
using grids.Server.Models;

[ApiController]
[Route("api/[controller]")]
    public class OsobaController : ControllerBase
    {
        private readonly GridDbContext _context;

        public OsobaController(GridDbContext context)
        {
            _context = context;
        }

    [HttpGet]
        public async Task<ActionResult<List<Osoba>>> Index([FromQuery] int page = 1, [FromQuery] int pageSize = 100)
        {
            var data = await _context.Osoba
                .AsNoTracking()
                .OrderBy(o => o.id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(data);
        }
    [HttpGet("{id}")]
        public async Task<ActionResult<Osoba>> Details(int id)
        {
            var osoba = await _context.Osoba
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return Ok(osoba);
        }

        
        

        // GET: osoba/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: osoba/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("id,firstname,lastname,city,email")] osoba osoba)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(osoba);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(osoba);
    //    }

    //}
}
