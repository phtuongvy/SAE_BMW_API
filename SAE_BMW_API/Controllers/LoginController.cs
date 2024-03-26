using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using SAE_BMW_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SAE_BMW_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<CompteClient> appUsers = new List<CompteClient>();

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] CompteClient login)
        {
            IActionResult response = Unauthorized();
            CompteClient compteClient = AuthenticateUser(login);
            if (compteClient != null)
            {
                var tokenString = GenerateJwtToken(compteClient);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = compteClient,
                });
            }
            return response;
        }
        private CompteClient AuthenticateUser(CompteClient compteClient)
        {
            return appUsers.SingleOrDefault(x => x.Email.ToUpper() == compteClient.Email.ToUpper() &&
            x.Password == compteClient.Password);
        }
        private string GenerateJwtToken(CompteClient userInfo)
        {
            var securityKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim("Email", userInfo.Email.ToString()),
                new Claim("role",userInfo.ClientRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}