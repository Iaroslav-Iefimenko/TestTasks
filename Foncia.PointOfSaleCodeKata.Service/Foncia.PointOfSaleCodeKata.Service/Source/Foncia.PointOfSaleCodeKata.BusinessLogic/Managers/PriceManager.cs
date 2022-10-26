using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Foncia.PointOfSaleCodeKata.BusinessLogic.Interfaces;
using Foncia.PointOfSaleCodeKata.DAL.Entities;
using Foncia.PointOfSaleCodeKata.DAL.Interfaces;
using Foncia.PointOfSaleCodeKata.Dto;

namespace Foncia.PointOfSaleCodeKata.BusinessLogic.Managers
{
    public class PriceManager : IPriceManager
    {
        private readonly IPriceRepository _priceRepository;

        public PriceManager([NotNull] IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public decimal CalculateTotal(List<BasketItemDto> items)
        {
            decimal totalPrice = 0;

            foreach (var item in items)
            {
                IEnumerable<PriceItem> prices = _priceRepository.GetPrices(item.ProductName);                
                var productsCount = item.Count;

                // apply discount once, if we have it
                if (prices.Where(x => x.Count > 1 && x.Count <= item.Count).Any())
                {
                    int discountCount = prices
                            .Where(x => x.Count > 1 && x.Count <= item.Count)
                            .Select(x => x.Count)
                            .Max();

                    decimal discountPrice = prices
                        .Where(x => x.Count == discountCount)
                        .Select(x => x.Price)                        
                        .First();

                    totalPrice += discountPrice;
                    productsCount -= discountCount;
                }

                // add price for other elements without discount
                if (productsCount > 0)
                {
                    decimal oneItemPrice = prices
                        .Where(x => x.Count == 1)
                        .Select(x => x.Price)
                        .FirstOrDefault();

                    if (oneItemPrice == 0)
                    {
                        throw new ArgumentException("No price for one " + item.ProductName);
                    }

                    totalPrice += oneItemPrice * productsCount;
                }
            }

            return totalPrice;
        }

        public bool SetPricing(string ProductName, int count, decimal price)
        {
            return _priceRepository.SetPricing(ProductName, count, price);
        }
    }
}
