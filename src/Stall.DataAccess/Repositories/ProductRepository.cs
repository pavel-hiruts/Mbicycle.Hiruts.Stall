using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Model;
using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly StallContext _context;

    public ProductRepository(StallContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected override Product CreateEntity(int id)
    {
        return new Product { Id = id };
    }

    public async Task<int> AddAsync(string name)
    {
        var entity = new Product {Name = name};
        var result = await AddAsync(entity);
        return result.Id;
    }

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Products.CountAsync(x => x.Id == id);
        return result == 1;
    }

    public async Task<bool> ExistByName(string name)
    {
        var result = await _context.Products.CountAsync(x => x.Name == name);
        return result > 0;
    }

    public async Task<int> UpdateAsync(int id, string name)
    {
        var product = new Product {Id = id, Name = name};
        _context.ChangeTracker.Clear();
        _context.Attach(product);
        await UpdateAsync(product);
        _context.ChangeTracker.Clear();
        return id;
    }
}