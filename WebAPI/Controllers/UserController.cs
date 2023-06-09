using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO;
using Shared.Model;

namespace WebAPI.Controllers;
[ApiController] //marks this class as a Web API controller, so that the Web API framework will know about it
[Route("[controller]")]
public class UserController : ControllerBase
{
     private readonly IUserLogic userLogic;
    private readonly IConfiguration config;

    public UserController(IUserLogic userLogic, IConfiguration config) //gets access to application layer
    {
        this.userLogic = userLogic;
        this.config = config;
    }
    
    private List<Claim> GenerateClaims(User user)
    {
        string ID = user.Id.ToString();
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.PostalCode, user.Password),
            new Claim(ClaimTypes.Sid, ID)
            // new Claim("DisplayName", user.Name),
            // new Claim("Email", user.Email),
            // new Claim("Age", user.Age.ToString()),
            // new Claim("Domain", user.Domain),
            // new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };
        return claims.ToList();
    }
    
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] RegisterUserDTO registerUserDto)
    {
        try
        {
            User user = await userLogic.ValidateUser(registerUserDto);
            string token = GenerateJwt(user);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

   
    [HttpPost]
    public async Task<ActionResult<User>> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        try
        {
            User user = await userLogic.RegisterUserAsync(registerUserDto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}