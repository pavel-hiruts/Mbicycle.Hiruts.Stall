using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stall.AuthApi.Domain;

namespace Stall.AuthApi.Context;

public class IdentityContext : IdentityDbContext<User, Role, int>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-0ITBT14;Database=StallDb;Trusted_Connection=True;");
    }
}