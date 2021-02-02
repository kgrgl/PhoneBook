using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [HttpGet("GetContacts")]
        public async Task<IActionResult> Index()
        {
            if (_context.Contacts.Count() == 0)
            {
                return NotFound("İletişim Bilgileri Bulunamadı");
            }
            return Ok(await _context.Contacts.Include(u => u.Match).ToListAsync());
        }
        [HttpGet("GetContact")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Lütfen Parametre Gönderin!");
            }

            var contacts = await _context.Contacts.Include(u => u.Match)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contacts == null)
            {
                return NotFound("İletişim Bilgisi Bulunamadı!");
            }

            return Ok(contacts);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Create(Contacts contacts)
        {
            if (contacts.ContactType == Contacts.ContactTypes.Location)
            {
                if (!ValidateLocationValues(contacts.ContactText))
                    return NotFound("Lütfen Geçerli Bir Lokasyon Giriniz!");
            }
            else if (contacts.ContactType == Contacts.ContactTypes.Mail)
            {
                if (!new EmailAddressAttribute().IsValid(contacts.ContactText))
                    return NotFound("Lütfen Geçerli Bir Mail Adresi Giriniz!");
            }
            else if (contacts.ContactType == Contacts.ContactTypes.Phone)
            {
                if (!new PhoneAttribute().IsValid(contacts.ContactText))
                    return NotFound("Lütfen Geçerli Bir Telefon Numarası Giriniz!");
            }
            _context.Add(contacts);
            await _context.SaveChangesAsync();
            return Ok(contacts);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Edit(int id, Contacts contacts)
        {
            if (id != contacts.ID)
            {
                return NotFound("İletişim Bilgisi Bulunamadı!");
            }

            try
            {
                if (contacts.ContactType == Contacts.ContactTypes.Location)
                {
                    if (!ValidateLocationValues(contacts.ContactText))
                        return NotFound("Lütfen Geçerli Bir Lokasyon Giriniz!");
                }
                else if (contacts.ContactType == Contacts.ContactTypes.Mail)
                {
                    if (!new EmailAddressAttribute().IsValid(contacts.ContactText))
                        return NotFound("Lütfen Geçerli Bir Mail Adresi Giriniz!");
                }
                else if (contacts.ContactType == Contacts.ContactTypes.Phone)
                {
                    if (!new PhoneAttribute().IsValid(contacts.ContactText))
                        return NotFound("Lütfen Geçerli Bir Telefon Numarası Giriniz!");
                }
                _context.Update(contacts);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsExists(contacts.ID))
                {
                    return NotFound("İletişim Bilgisi Bulunamadı!");
                }
                else
                {
                    throw;
                }
            }
            return Ok(contacts);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
        private bool ValidateLocationValues(string pointValue)
        {
            bool pointResult = false;
            double price;
            try
            {
                string[] values = pointValue.Split(',');
                if (values.Length > 2)
                    pointResult = false;
                else if (!Double.TryParse(values[0], out price))
                    pointResult = false;
                else if (!Double.TryParse(values[1], out price))
                    pointResult = false;
                return true;
            }
            catch (Exception)
            {
                pointResult = false;
            }
            return pointResult;
        }
    }
}
