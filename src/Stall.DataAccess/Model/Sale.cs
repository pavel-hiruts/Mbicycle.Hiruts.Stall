using Stall.DataAccess.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Stall.DataAccess.Model
{
    public class Sale : Base.Model
    {
        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }
    }
}
