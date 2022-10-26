using System.Collections.Generic;
using Foncia.PointOfSaleCodeKata.Dto;

namespace Foncia.PointOfSaleCodeKata.BusinessLogic.Interfaces
{
    public interface IPriceManager
    {
        decimal CalculateTotal(List<BasketItemDto> items);

        bool SetPricing(string ProductName, int count, decimal price);
    }
}
