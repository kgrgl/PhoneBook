using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.IntegrationTests
{
    public class ContactsControllerShouldIntegration : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public ContactsControllerShouldIntegration(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetContactsShouldDoOk()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Contacts/GetContacts");
            var response = await _client.GetAsync(request.RequestUri);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
