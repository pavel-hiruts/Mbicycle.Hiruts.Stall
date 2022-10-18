namespace Stall.DataAccess.Model
{
    public class Sale : Base.Entity
    {
        public DateTime Date { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

    }
}
