using Microsoft.AspNetCore.Identity;

namespace Stall.DataAccess.Model.Identity;

public class Role : IdentityRole<int>
{
    public Role() : base()
    {
        
    }

    public Role(string name) : base(name)
    {
        
    }
}