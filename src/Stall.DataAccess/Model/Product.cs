using Stall.DataAccess.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stall.DataAccess.Model
{
    public class Product : Base.Model
    {
        [MaxLength(50)]

        public string Name { get; set; }
    }
}
