using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stall.DataAccess.Model.Domain;
using Stall.DataAccess.Model.Identity;

namespace Stall.DataAccess.Context;

public sealed class StallContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-0ITBT14;Database=StallDb;Trusted_Connection=True;");
    }

        
}