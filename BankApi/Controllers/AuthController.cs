using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankApi.Data;
using BankApi.Models;

namespace BankApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly string key = "THIS_IS_MY_SUPER_SECRET_KEY_1234567890_ABC";

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request, [FromServices] AppDbContext db)
    {
        var user = db.Users.FirstOrDefault(u => u.Username == request.Username);

        if (user == null || request.Password != "1234")
            return Unauthorized("Invalid credentials");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}