using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTests
{
    public class ContactsControllerShould
    {
        [Fact]
        public async Task CreateNewContact()
        {
            var options = new DbContextOptionsBuilder<PhoneBook.Models.PhoneDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_PhoneBook")
            .Options;

            using (var inMemoryContext = new PhoneDbContext(options))
            {
                var service = new PhoneBook.Controllers.ContactsController(inMemoryContext);
                await service.Create(new Contacts { ContactType = Contacts.ContactTypes.Location, ContactText = "30,40" });
            }
            using (var inMemoryContext = new PhoneDbContext(options))
            {
                Assert.Equal(1, await inMemoryContext.Contacts.CountAsync());
            }
        }
    }
}
