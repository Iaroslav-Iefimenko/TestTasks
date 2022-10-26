using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace Foncia.PointOfSaleCodeKata.IntegrationTests.BaseTests
{
    public class BaseWebApiTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public BaseWebApiTests(WebApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
        }
    }
}
