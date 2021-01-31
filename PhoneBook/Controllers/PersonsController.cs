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
        public async Task<List<Persons>> Index()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Persons> Details(int? id)
        {
            var persons = await _context.Persons
                .FirstOrDefaultAsync(m => m.ID == id);
            return persons;
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.ID == id);
        }
    }
}
