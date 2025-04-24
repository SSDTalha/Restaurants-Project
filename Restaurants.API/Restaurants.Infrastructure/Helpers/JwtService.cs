using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Restaurants.Infrastructure.Helpers
{
    public class JwtService
    {
        private string SecureKey = "SnoopDogg@2025SuperSecret";

        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecureKey));

            var credential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credential);

            // Updated JwtPayload constructor to match the correct overload
            var payload = new JwtPayload(
                issuer: id.ToString(),
                audience: null,
                claims: null,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                issuedAt: DateTime.UtcNow
            );

            var SecurityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(SecurityToken);
        }
    }
}