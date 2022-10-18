using Stall.DataAccess.Context;
using Stall.DataAccess.Model;

namespace Stall.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StallContext context) : base(context)
        {
        }

        protected override Product CreateEntity(int id)
        {
            return new Product { Id = id };
        }
    }
}
