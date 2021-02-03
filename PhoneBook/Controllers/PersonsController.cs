using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
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

            return Ok(await _context.Persons.Include(u => u.contacts).ToListAsync());
        }
        [HttpGet("GetPersonByLocation")]
        public async Task<IActionResult> GetPersonCountByLocation([FromQuery] string ltd, [FromQuery] string lgt)
        {
            List<Persons> nList = _context.Persons.Include(u => u.contacts).ToList();
            List<PersonInfoView> results = (from p in nList
                                            join ps in _context.Contacts on p.ID equals ps.Match.ID
                                            where (int)ps.ContactType == 3
                                            select new PersonInfoView()
                                            {
                                                Ad = p.Ad,
                                                Soyad = p.Soyad,
                                                Firma = p.Firma,
                                                pLtd = ps.ContactText.Split(',', StringSplitOptions.None)[0].ToString().Trim(),
                                                pLgt = ps.ContactText.Split(',', StringSplitOptions.None)[1].ToString().Trim()
                                            }).Where(x => x.pLtd == ltd).Where(x => x.pLgt == lgt).ToList();
            return Ok(results);
        }
        [HttpGet("GetLocaitonsWithPersonCount")]
        public async Task<IActionResult> GetLocaitonsWithPersonCount()
        {
            List<LocationInfoView> listResult = (from c in _context.Contacts
                                                 join p in _context.Persons on c.Match.ID equals p.ID
                                                 where (int)c.ContactType == 3
                                                 orderby c.ID
                                                 select new LocationInfoView
                                                 {
                                                     locationPoint = c.ContactText,
                                                     personCount = (from pt in _context.Contacts
                                                                    where pt.ContactText == c.ContactText
                                                                    select pt).Count(),
                                                     phoneCount = (from pk in (from p in _context.Persons.Include(u => u.contacts)
                                                                               join pt in _context.Contacts on p.ID equals pt.Match.ID
                                                                               where (int)pt.ContactType == 3
                                                                               where pt.ContactText == c.ContactText
                                                                               select p)
                                                                   join pr in _context.Contacts on pk.ID equals pr.Match.ID
                                                                   where (int)pr.ContactType == 1
                                                                   select pk).Count(),
                                                     kisiler = (from p in _context.Persons.Include(u => u.contacts)
                                                                join pt in _context.Contacts on p.ID equals pt.Match.ID
                                                                where (int)pt.ContactType == 3
                                                                where pt.ContactText == c.ContactText
                                                                select p).ToList()

                                                 }).ToList();
            return Ok(listResult);
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
        public class PersonInfoView
        {
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Firma { get; set; }
            public string pLtd { get; set; }
            public string pLgt { get; set; }
        }
        public class LocationInfoView
        {
            public string locationPoint { get; set; }
            public int personCount { get; set; }
            public int phoneCount { get; set; }
            public List<Persons> kisiler { get; set; }
        }

    }

}
