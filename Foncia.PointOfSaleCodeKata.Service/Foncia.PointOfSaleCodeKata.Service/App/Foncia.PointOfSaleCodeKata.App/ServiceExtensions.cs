using Foncia.PointOfSaleCodeKata.BusinessLogic.Interfaces;
using Foncia.PointOfSaleCodeKata.BusinessLogic.Managers;
using Foncia.PointOfSaleCodeKata.DAL.Interfaces;
using Foncia.PointOfSaleCodeKata.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Foncia.PointOfSaleCodeKata.App
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterFonciaServices(
            this IServiceCollection services)
        {
            services.AddTransient<IPriceRepository, PriceRepository>();
            services.AddTransient<IPriceManager, PriceManager>();
            return services;
        }
    }
}
