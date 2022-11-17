using Microsoft.AspNetCore.Identity;

namespace Stall.DataAccess.Model.Identity;

public class User : IdentityUser<int>
{
    public User() : base()
    {
        
    }
    
    public User(string name) : base(name)
    {
        
    }
}