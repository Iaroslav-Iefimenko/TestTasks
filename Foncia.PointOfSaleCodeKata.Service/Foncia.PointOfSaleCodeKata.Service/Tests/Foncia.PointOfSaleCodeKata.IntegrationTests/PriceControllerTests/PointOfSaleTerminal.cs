using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Foncia.PointOfSaleCodeKata.Dto;
using Foncia.PointOfSaleCodeKata.Dto.Requests;
using Foncia.PointOfSaleCodeKata.IntegrationTests.BaseTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Foncia.PointOfSaleCodeKata.IntegrationTests.PriceControllerTests
{
    public class PointOfSaleTerminal : BaseWebApiTests
    {
        private List<BasketItemDto> _basket;
        
        public PointOfSaleTerminal(WebApplicationFactory<Startup> fixture) : base(fixture)
        {
            _basket = new List<BasketItemDto>();
        }

        public void Scan(string productName)
        {
            if (_basket.Any(x => x.ProductName == productName))
            {
                _basket.First(x => x.ProductName == productName).Count++;
            }
            else
            {
                _basket.Add(new BasketItemDto { ProductName = productName, Count = 1 });
            }
        }

        public void Clear()
        {
            _basket.Clear();
        }

        public async Task<decimal> CalculateTotal()
        {
            var req = new CalculateTotalRequest
            {
                BasketItems = _basket
            };

            string content = JsonConvert.SerializeObject(req);
            StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync("/api/price/calculate-total", httpContent);
            var resStr = await response.Content.ReadAsStringAsync();
            return decimal.Parse(resStr, NumberStyles.Number, CultureInfo.InvariantCulture);
        }
    }
}
