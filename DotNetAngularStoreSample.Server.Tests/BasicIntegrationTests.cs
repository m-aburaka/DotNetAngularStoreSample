using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DotNetAngularStoreSample.Server.Tests
{
    public class BasicIntegrationTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BasicIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();

            var url = "/api/Customers/GetAll";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}
