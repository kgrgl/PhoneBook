using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly PhoneDbContext _context;

        public PersonsController(PhoneDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_context.Persons.Count() == 0)
            {
                return NotFound("Liste bulunamadı");
            }
            return Ok(await _context.Persons.ToListAsync());
        }
        [HttpGet("GetPerson")]
        public async Task<IActionResult> Details(int? id)
        {
            var persons = await _context.Persons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (persons==null)
            {
                return NotFound("Kişi Bulunamadı");
            }
            return Ok(persons);
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.ID == id);
        }
    }
}
