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
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;

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

            var hashedPasswordUserInput = ComputeSha256Hash(user.Password);

            return comptes.Value.FirstOrDefault(x => x.Email.Trim().ToUpper() == user.Email.Trim().ToUpper() &&
            ComputeSha256Hash(x.Password) == hashedPasswordUserInput);
        }
        private string GenerateJwtToken(CompteClient userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                 new Claim("email", userInfo.Email.ToString()),
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

        // to encrypt password data
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


    }
}
