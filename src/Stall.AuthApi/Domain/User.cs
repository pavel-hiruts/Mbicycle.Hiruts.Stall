using Microsoft.AspNetCore.Identity;

namespace Stall.AuthApi.Domain;

public class User : IdentityUser<int>
{
    public User() : base()
    {
        
    }
    
    public User(string name) : base(name)
    {
        
    }
}