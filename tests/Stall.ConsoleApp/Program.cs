using Stall.DataAccess.Context;
using Stall.DataAccess.Model;
using Stall.DataAccess.Repositories;
using Stall.DataAccess.UnitOfWork;

await using var context = new StallContext();

var unitOfWork = new UnitOfWork(context);
var productRepo = new ProductRepository(context);
var saleRepo = new SaleRepository(context);

try
{
    await unitOfWork.BeginTransactionAsync();

    var potatoes = new Product { Name = "Potatoes" };

    await productRepo.AddAsync(potatoes);

    //potatoes = null;

    Console.WriteLine(potatoes.Id);

    await saleRepo.AddAsync(new Sale { Date = DateTime.Now, Price = 10, Count = 5, Product = potatoes });

    await unitOfWork.CommitTransactionAsync();
}
catch
{
    await unitOfWork.RollbackTransactionAsync();
}