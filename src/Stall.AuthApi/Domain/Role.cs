using Microsoft.AspNetCore.Identity;

namespace Stall.AuthApi.Domain;

public class Role : IdentityRole<int>
{
    public Role() : base()
    {
        
    }

    public Role(string name) : base(name)
    {
        
    }
}