using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SAE_API.Models.EntityFramework;
using SAE_API.Models;
using System;
using SAE_API.Repository;

namespace SAE_API.Controllers.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataRepository<CompteClient> dataRepository;

        public LoginController(IConfiguration config, IDataRepository<CompteClient> dataRepo)
        {
            _config = config;
            dataRepository = dataRepo;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CompteClient login)
        {
            IActionResult response = Unauthorized();
            CompteClient user = await AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        private async Task<CompteClient> AuthenticateUser(CompteClient user)
        {
            var comptes = await dataRepository.GetAllAsync();

            return comptes.Value.SingleOrDefault(x => x.Email.ToUpper() == user.Email.ToUpper() &&
           x.Password == user.Password);
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
