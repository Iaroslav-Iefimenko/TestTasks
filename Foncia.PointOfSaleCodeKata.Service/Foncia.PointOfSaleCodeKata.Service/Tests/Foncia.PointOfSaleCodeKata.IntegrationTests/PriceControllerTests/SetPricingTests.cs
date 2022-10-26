using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Foncia.PointOfSaleCodeKata.Dto.Requests;
using Foncia.PointOfSaleCodeKata.IntegrationTests.BaseTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Foncia.PointOfSaleCodeKata.IntegrationTests.PriceControllerTests
{
    public class SetPricingTests : BaseWebApiTests
    {
        private const string RequestUri = "/api/price/set-pricing";

        public SetPricingTests(WebApplicationFactory<Startup> fixture) : base (fixture)
        {
        }

        [Fact]
        public async Task ShouldAddPriceItemToRepository()
        {
            // arrange
            var req = new SetPricingRequest
            {
                ProductName = "test product",
                Count = 1,
                Price = 4.5M
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // act
            HttpResponseMessage response = await Client.PostAsync(RequestUri, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var resStr = await response.Content.ReadAsStringAsync();
            bool res;
            Assert.True(bool.TryParse(resStr, out res), "Result is not Boolean value");
            Assert.True(res, "Add price operation is not successful");
        }

        [Fact]
        public async Task ShouldRejectEmptyProductName()
        {
            // arrange
            var req = new SetPricingRequest
            {
                ProductName = "",
                Count = 1,
                Price = 4.5M
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // act
            HttpResponseMessage response = await Client.PostAsync(RequestUri, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ShouldRejectNegativeProductsCount()
        {
            // arrange
            var req = new SetPricingRequest
            {
                ProductName = "product",
                Count = -1,
                Price = 4.5M
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // act
            HttpResponseMessage response = await Client.PostAsync(RequestUri, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ShouldRejectZeroProductsCount()
        {
            // arrange
            var req = new SetPricingRequest
            {
                ProductName = "product",
                Count = 0,
                Price = 4.5M
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // act
            HttpResponseMessage response = await Client.PostAsync(RequestUri, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ShouldRejectNegativePrice()
        {
            // arrange
            var req = new SetPricingRequest
            {
                ProductName = "product",
                Count = 1,
                Price = -4.5M
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // act
            HttpResponseMessage response = await Client.PostAsync(RequestUri, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ShouldRejectZeroPrice()
        {
            // arrange
            var req = new SetPricingRequest
            {
                ProductName = "product",
                Count = 1,
                Price = 0
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // act
            HttpResponseMessage response = await Client.PostAsync(RequestUri, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
