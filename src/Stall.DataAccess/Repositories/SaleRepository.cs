using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Context;
using Stall.DataAccess.Model;

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

    public override ICollection<Sale> Get()
    {
        return _context.Sales
            .Include(x => x.Product)
            .ToList();
    }
}