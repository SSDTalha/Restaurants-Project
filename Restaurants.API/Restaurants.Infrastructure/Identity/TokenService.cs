﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;


namespace Restaurants.Infrastructure.Identity
{
    public class TokenService(IConfiguration config) : ITokenService
    {

        public string CreateToken(User user)
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot acess token key from appsetting");
            if (tokenKey.Length < 64)
            {
                throw new Exception("Your token key needs to be longer");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.UserName)

            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}