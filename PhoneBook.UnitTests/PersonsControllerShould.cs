using Microsoft.EntityFrameworkCore;
using PhoneBook.Controllers;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTests
{
    public class PersonsControllerShould
    {
        [Fact]
        public async Task CreateNewContact()
        {
            var options = new DbContextOptionsBuilder<PhoneDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_PhoneBook")
            .Options;

            using (var inMemoryContext = new PhoneDbContext(options))
            {
                var service = new PersonsController(inMemoryContext);
                await service.Create(new Persons { Ad = "Deneme", Soyad = "deneme soyadı",Firma="Test Firması" });
            }
            using (var inMemoryContext = new PhoneDbContext(options))
            {
                Assert.Equal(1, await inMemoryContext.Persons.CountAsync());
            }
        }

    }
}
