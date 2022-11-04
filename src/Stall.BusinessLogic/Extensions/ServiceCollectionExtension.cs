using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stall.BusinessLogic.Handlers.Commands.Product;
using Stall.BusinessLogic.Handlers.Commands.Sale;
using Stall.DataAccess.Context;
using Stall.DataAccess.Repositories.Product;
using Stall.DataAccess.Repositories.Sale;

namespace Stall.BusinessLogic.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        //context
        var context = new StallContext();
        services.AddSingleton(context);
        services.AddSingleton<DbContext>(context);
            
        //repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
            
        //validators
        //services.AddScoped<IValidator<>, >();
        services.AddScoped<IValidator<AddSaleCommand>, AddSaleCommandValidator>();
        services.AddScoped<IValidator<UpdateSaleCommand>, UpdateSaleCommandValidator>();
        services.AddScoped<IValidator<DeleteSaleCommand>, DeleteSaleCommandValidator>();
        services.AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>();
        services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
        services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
    }
}