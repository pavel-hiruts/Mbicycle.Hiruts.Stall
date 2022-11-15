using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stall.AuthApi.Domain;
using Stall.AuthApi.InputModels;

namespace Stall.AuthApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    
    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateUserInputModel input)
    {
        var user = new User(input.Name);
        var result = await _userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        return CreatedAtAction("Post", new { id = user.Id }, user.UserName);
    }
}