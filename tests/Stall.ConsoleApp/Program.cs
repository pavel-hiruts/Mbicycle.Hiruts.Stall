
using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Model;

using (var context = new StallContext())
{
    var banana = new Product { Name = "Banana" };
    var orange = new Product { Name = "Orange" };

    var bananaEntry = context.Products.Add(banana);
    var orangeEntry = context.Products.Add(orange);

    context.SaveChanges();

    banana = bananaEntry.Entity;
    orange = orangeEntry.Entity;

    var bananaSale = new Sale
    {
        Date = DateTime.Now,
        ProductId = banana.Id,
        Count = 10,
        Price = 5,
    };

    var orangeSale = new Sale
    {
        Date = DateTime.Now,
        ProductId = orange.Id,
        Count = 10,
        Price = 5,
    };

    context.Sales.Add(bananaSale);
    context.Sales.Add(orangeSale);

    context.SaveChanges();

    var query = context.Sales.Select(x => x);

    query = query.Include(x => x.Product);

    var collection = query.ToList();

    foreach (var item in collection)
    {
        Console.WriteLine($"{item.Id} - {item.Date} - {item.Product?.Name} - {item.Price} - {item.Count} - {item.Price * item.Count}");
    }
}