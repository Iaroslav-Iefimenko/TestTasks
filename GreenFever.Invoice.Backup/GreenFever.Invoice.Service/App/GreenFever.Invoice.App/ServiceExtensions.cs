using GreenFever.Invoice.BusinessLogic.Interfaces;
using GreenFever.Invoice.BusinessLogic.Services;
using GreenFever.Invoice.Dal.Interfaces;
using GreenFever.Invoice.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFever.Invoice.App
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterGreenFeverServices(
            this IServiceCollection services)
        {
            services.AddTransient<IInvoicesProcessingRepository, InvoicesProcessingRepository>();
            services.AddTransient<IInvoicesProcessingService, InvoicesProcessingService>();
            return services;
        }
    }
}
