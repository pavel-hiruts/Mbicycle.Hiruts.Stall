using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories.Sale;

public class SaleRepository : Repository<Model.Sale>, ISaleRepository
{
    private readonly StallContext _context;

    public SaleRepository(StallContext context) : base(context)
    {
        _context = context;
    }

    protected override Model.Sale CreateEntity(int id)
    {
        return new Model.Sale { Id = id };
    }

    public override async Task<ICollection<Model.Sale>> GetAsync()
    {
        var query = _context.Sales
            .Select(x => new Model.Sale
            {
                Id = x.Id,
                Product = x.Product,
                Count = x.Count,
                Price = x.Price,
                Date = x.Date,
            });
            
            var result = await query.ToListAsync();
        
        return result;
    }
    
    public async Task<ICollection<Model.Sale>> GetAllSalesDashboardAsync()
    {
        var query = _context.Sales
            .Select(x => new Model.Sale
            {
                Id = x.Id,
                Product = new Model.Product
                {
                    Name = x.Product.Name,
                },
                Count = x.Count,
                Price = x.Price,
                Date = x.Date,
            });
            
        var result = await query.ToListAsync();
        
        return result;
    }

    public async Task<int> AddAsync(
        int productId, 
        DateTime date, 
        int count, 
        decimal price)
    {
        var sale = new Model.Sale
        {
            Product = new Model.Product {Id = productId},
            Date = date,
            Price = price,
            Count = count,
        };
       
        _context.ChangeTracker.Clear();
        _context.Attach(sale.Product);
        var result = await AddAsync(sale);
        _context.ChangeTracker.Clear();
        
        return result.Id;
    }

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Sales.CountAsync(x => x.Id == id);
        return result == 1;
    }

    public async Task<int> UpdateAsync(
        int saleId, 
        int productId, 
        DateTime date, 
        int count, 
        decimal price)
    {
        var sale = new Model.Sale
        {
            Id = saleId,
            Product = new Model.Product {Id = productId},
            Date = date,
            Price = price,
            Count = count,
        };
       
        _context.ChangeTracker.Clear();
        _context.Attach(sale);
        await UpdateAsync(sale);
        _context.ChangeTracker.Clear();
        
        return saleId;
    }
}