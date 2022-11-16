using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stall.AuthApi.Domain;
using Stall.AuthApi.InputModels;

namespace Stall.AuthApi.Controllers;

[ApiController]
[Route("auth/api/user")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    
    private readonly RoleManager<Role> _roleManager;
    
    public UserController(
        UserManager<User> userManager, 
        RoleManager<Role> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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

    [HttpPut("{userId:int}/to/role/{roleId:int}")]
    public async Task<IActionResult> AddToRole(int userId, int roleId)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
        {
            return NotFound($"Could not found user with id = {userId}");
        }
        
        var role = await _roleManager.FindByIdAsync($"{roleId}");
        if (role == null)
        {
            return NotFound($"Could not found role with id = {roleId}");
        }

        var result = await _userManager.AddToRoleAsync(user, role.Name);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        return Ok();
    }
    
}