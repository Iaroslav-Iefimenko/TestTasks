using Foncia.PointOfSaleCodeKata.BusinessLogic.Interfaces;
using Foncia.PointOfSaleCodeKata.Dto.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Foncia.PointOfSaleCodeKata.App.Controllers
{
    [Route("api/price")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceManager _priceManager;
        
        public PriceController([NotNull] IPriceManager priceManager)
        {
            _priceManager = priceManager;
        }

        [HttpPost]
        [Route("set-pricing")]
        public ActionResult SetPricing([FromBody] SetPricingRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            bool res = _priceManager.SetPricing(request.ProductName, request.Count, request.Price);
            return Ok(res);
        }

        [HttpPost]
        [Route("calculate-total")]
        public ActionResult CalculateTotal([FromBody] CalculateTotalRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            decimal res = _priceManager.CalculateTotal(request.BasketItems);
            return Ok(res);
        }
    }
}
