using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using expenses_api.Models;
using expenses_api.Dtos;
using expenses_api.Data;
using expenses_api.Services;

namespace expenses_api.Controllers;

[EnableCors("AllowAll")]
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<TransactionsController> _logger;
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthController(ILogger<TransactionsController> logger, AppDbContext context, PasswordHasher<User> passwordHasher)
    {
        _logger = logger;
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("Ok");
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserDto payload)
    {
        var user = _context.Users
            .FirstOrDefault(n => n.Email == payload.Email);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, payload.Password);
        if(result == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody]UserDto payload)
    {
        if (_context.Users.Any(n => n.Email == payload.Email))
            return BadRequest("This email address is already taken");

        var newUser = new User()
        {
            Email = payload.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, payload.Password);

        newUser.Password = hashedPassword;

        _context.Users.Add(newUser);
        _context.SaveChanges();

        var token = GenerateJwtToken(newUser);

        return Ok(new {Token = token});
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-secure-secret-key-32-chars-long"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "dotnethow.net",
            audience: "dotnethow.net",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}