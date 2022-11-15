using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stall.AuthApi.Domain;
using Stall.AuthApi.InputModels;

namespace Stall.AuthApi.Controllers;

[ApiController]
[Route("api/jwt")]
public class JwtController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    
    private readonly SignInManager<User> _signInManager;
    
    public JwtController(
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }
    
    [HttpPost("get")]
    public async Task<IActionResult> Post(GetJwtTokenInputModel input)
    {
        var user = await _userManager.FindByNameAsync(input.Name);
        if (user == null)
        {
            return NotFound($"User '{input.Name}' not found");
        }

        var passValidation = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
        if (!passValidation.Succeeded)
        {
            return BadRequest("Invalid password");
        }

        var token = await GetToken(user);
        
        return Ok(token);
    }

    private async Task<string> GetToken(User user)
    {
        
        var claims = new List<Claim>
        {
            new (ClaimsIdentity.DefaultNameClaimType, user.UserName)
        };

        var roles = (await _userManager.GetRolesAsync(user));
        claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
        
        var jwt = new JwtSecurityToken(
            issuer: "some issuer",
            audience: "some audience",
            notBefore: DateTime.Now,
            claims: claims,
            expires:  DateTime.Now.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("the_secret_key_123451234512345")), SecurityAlgorithms.HmacSha256));
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return token;
    }
}