
using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Model;
using Stall.DataAccess.Repositories;

using (var context = new StallContext())
{
    var list = context.Sales.Include(x => x.Product).ToList();

    var banana = new Product { Id = 5 };
    var orange = new Product { Name = "Orange" };

    var productRepository = new ProductRepository(context);

    banana = productRepository.Get(5);

    context.Sales.Add(new Sale { Date = DateTime.Now, Price = 5, Count = 1, Product = banana });

    context.SaveChanges();

    productRepository.Delete(3);

    productRepository.Add(banana);
    productRepository.Add(orange);


    foreach (var item in productRepository.Get())
    {
        Console.WriteLine($"{item.Id} - {item.Name}");
    }

    var product = productRepository.Get(1);
    Console.WriteLine($"{product.Id} - {product.Name}");

}