using Stall.DataAccess.Context;
using Stall.DataAccess.Model;
using Stall.DataAccess.Repositories;
using Stall.DataAccess.UnitOfWork;

using var context = new StallContext();

var unitOfWork = new UnitOfWork(context);
var productRepo = new ProductRepository(context);
var saleRepo = new SaleRepository(context);

try
{
    unitOfWork.BeginTransaction();

    var potatoes = new Product { Name = "Potatoes" };

    productRepo.Add(potatoes);

    //potatoes = null;

    Console.WriteLine(potatoes.Id);

    saleRepo.Add(new Sale { Date = DateTime.Now, Price = 10, Count = 5, Product = potatoes });

    unitOfWork.CommitTransaction();
}
catch
{
    unitOfWork.RollbackTransaction();
}