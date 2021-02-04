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
    public class PersonsControllerShouldIngtegration
    {
        private readonly HttpClient _client;

        public PersonsControllerShouldIngtegration(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetPersonsShouldDoOk()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Persons");
            var response = await _client.GetAsync(request.RequestUri);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
