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
    public class ContactsController : Controller
    {
        private readonly PhoneDbContext _context;

        public ContactsController(PhoneDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_context.Contacts.Count() == 0)
            {
                return NotFound("Liste bulunamadı");
            }
            return Ok(await _context.Contacts.ToListAsync());
        }
        [HttpGet("GetContact")]
        public async Task<IActionResult> Details(int? id)
        {
            var contacts = await _context.Contacts
               .FirstOrDefaultAsync(m => m.ID == id);
            if (contacts == null)
            {
                return NotFound("Kişi Bulunamadı");
            }
            return Ok(contacts);
        }
        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
