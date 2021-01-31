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
        public async Task<List<Contacts>> Index()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contacts> Details(int? id)
        {
            var contacts = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ID == id);
            return contacts;
        }
        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
