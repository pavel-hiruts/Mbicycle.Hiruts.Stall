using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Model.Domain;
using Stall.DataAccess.Model.Identity;
using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories;

public class SaleRepository : Repository<Sale>, ISaleRepository
{
    private readonly StallContext _context;

    public SaleRepository(StallContext context) : base(context)
    {
        _context = context;
    }

    protected override Sale CreateEntity(int id)
    {
        return new Sale { Id = id };
    }

    public override async Task<ICollection<Sale>> GetAsync()
    {
        var query = _context.Sales
            .Select(x => new Sale
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
    
    public async Task<ICollection<Sale>> GetAllSalesDashboardAsync()
    {
        var query = _context.Sales
            .Select(x => new Sale
            {
                Id = x.Id,
                Product = new Product
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

    public async Task<int> AddAsync(int productId,
        DateTime date,
        int count,
        decimal price, 
        int createdBy)
    {
        var sale = new Sale
        {
            Product = new Product {Id = productId},
            Date = date,
            Price = price,
            Count = count,
            CreatedBy = new User {Id = createdBy}
        };
       
        _context.ChangeTracker.Clear();
        _context.Attach(sale.Product);
        _context.Attach(sale.CreatedBy);
        var result = await AddAsync(sale);
        _context.ChangeTracker.Clear();
        
        return result.Id;
    }

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Sales.CountAsync(x => x.Id == id);
        return result == 1;
    }

    public async Task<int> UpdateAsync(int saleId,
        int productId,
        DateTime date,
        int count,
        decimal price, 
        int updatedBy)
    {
        var sale = await GetAsync(saleId);
        sale.Product = new Product {Id = productId};
        sale.Date = date;
        sale.Price = price;
        sale.Count = count;
        sale.UpdatedBy = new User {Id = updatedBy};        
       
        _context.ChangeTracker.Clear();
        _context.Attach(sale);
        await UpdateAsync(sale);
        _context.ChangeTracker.Clear();
        
        return saleId;
    }
}