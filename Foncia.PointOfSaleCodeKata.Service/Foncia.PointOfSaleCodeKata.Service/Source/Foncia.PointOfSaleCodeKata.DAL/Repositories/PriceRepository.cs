using System;
using System.Collections.Generic;
using System.Linq;
using Foncia.PointOfSaleCodeKata.DAL.Entities;
using Foncia.PointOfSaleCodeKata.DAL.Interfaces;

namespace Foncia.PointOfSaleCodeKata.DAL.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private List<PriceItem> prices;

        public PriceRepository()
        {
            prices = new List<PriceItem>();

            // initial data
            prices.Add(new PriceItem { ProductName = "A", Count = 1, Price = 1.25M });
            prices.Add(new PriceItem { ProductName = "A", Count = 3, Price = 3M });
            prices.Add(new PriceItem { ProductName = "B", Count = 1, Price = 4.25M });
            prices.Add(new PriceItem { ProductName = "C", Count = 1, Price = 1M });
            prices.Add(new PriceItem { ProductName = "C", Count = 6, Price = 5M });
            prices.Add(new PriceItem { ProductName = "D", Count = 1, Price = 0.75M });
        }

        public IEnumerable<PriceItem> GetPrices(string productName)
        {
            return prices.Where(p => p.ProductName == productName);
        }

        public bool SetPricing(string productName, int count, decimal price)
        {
            prices.Add(new PriceItem { ProductName = productName, Count = count, Price = price });
            return true;
        }
    }
}
