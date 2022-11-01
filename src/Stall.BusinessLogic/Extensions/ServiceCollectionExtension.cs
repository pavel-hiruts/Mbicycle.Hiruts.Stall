using Microsoft.Extensions.DependencyInjection;
using Stall.DataAccess.Context;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton(new StallContext());
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            
            //services.AddScoped<ISalesService, SalesService>();
        }
    }
}
