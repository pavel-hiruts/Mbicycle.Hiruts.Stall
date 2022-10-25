using System.ComponentModel.DataAnnotations;

namespace Stall.DataAccess.Model;

public class Product : Base.Entity
{
    [MaxLength(50)]

    public string Name { get; set; }
}