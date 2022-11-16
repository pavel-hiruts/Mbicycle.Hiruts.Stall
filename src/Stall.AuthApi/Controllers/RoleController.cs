using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stall.AuthApi.Domain;
using Stall.AuthApi.InputModels;

namespace Stall.AuthApi.Controllers;

[ApiController]
[Route("auth/api/role")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;

    public RoleController(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager  ?? throw new ArgumentNullException(nameof(roleManager));;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateRoleInputModel input)
    {
        var role = new Role(input.Name);
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        return CreatedAtAction("Post", new { id = role.Id }, role.Name);
    }
}