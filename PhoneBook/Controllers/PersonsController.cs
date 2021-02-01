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
                return NotFound("Kişi Listesi Bulunamadı");
            }
            return Ok(await _context.Persons.ToListAsync());
        }
        [HttpGet("GetPerson")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Lütfen Parametre Gönderin!");
            }

            var persons = await _context.Persons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (persons == null)
            {
                return NotFound("Kişi Bilgisi Bulunamadı!");
            }

            return Ok(persons);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Create(Persons persons)
        {
            _context.Add(persons);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Ok(persons);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Edit(int id, Persons persons)
        {
            if (id != persons.ID)
            {
                return NotFound("Kişi Bilgisi Bulunamadı!");
            }

            try
            {
                _context.Update(persons);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonsExists(persons.ID))
                {
                    return NotFound("Kişi Bilgisi Bulunamadı!");
                }
                else
                {
                    throw;
                }
            }
            return Ok(persons);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persons = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(persons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.ID == id);
        }
    }
}
