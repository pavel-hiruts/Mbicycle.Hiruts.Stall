using Stall.DataAccess.Model.Domain.Base;

namespace Stall.DataAccess.Model.Domain;

public class Sale : Entity
{
    public DateTime Date { get; set; }

    public Product Product { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}