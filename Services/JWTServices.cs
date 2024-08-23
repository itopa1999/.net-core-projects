using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using onboardingAPI.Interfaces;
using onboardingAPI.Models;

namespace onboardingAPI.Services
{
    public class JWTServices: IJwtRepository
    {
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    public JWTServices(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
    }
    
  
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName),
                new Claim (JwtRegisteredClaimNames.Email, appUser.Email)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public int GenerateToken()
        {
            Random random = new Random();
            return random.Next(100000, 1000000);
        }

        public int GenerateAccountNumber()
        {
            Random random = new Random();
            return random.Next(100000000, 1000000000);
        }
    }

    



    
}
