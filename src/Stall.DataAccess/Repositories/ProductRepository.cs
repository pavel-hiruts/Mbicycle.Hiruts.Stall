using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Model;

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

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Products.CountAsync(x => x.Id == id);
        return result == 1;
    }
}