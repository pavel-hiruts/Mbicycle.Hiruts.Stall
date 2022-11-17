using System.ComponentModel.DataAnnotations;
using Stall.DataAccess.Model.Domain.Base;

namespace Stall.DataAccess.Model.Domain;

public class Product : Entity
{
    [MaxLength(50)]
    public string Name { get; set; }
}