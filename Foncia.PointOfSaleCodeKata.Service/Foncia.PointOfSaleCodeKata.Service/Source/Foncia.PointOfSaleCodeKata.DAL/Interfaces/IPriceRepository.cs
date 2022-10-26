using System.Collections.Generic;
using Foncia.PointOfSaleCodeKata.DAL.Entities;

namespace Foncia.PointOfSaleCodeKata.DAL.Interfaces
{
    public interface IPriceRepository
    {
        bool SetPricing(string productName, int count, decimal price);

        IEnumerable<PriceItem> GetPrices(string productName);
    }
}
