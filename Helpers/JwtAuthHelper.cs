using Clinic_Web_Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Helpers
{

    public class JwtAuthHelper
    {
        public IConfiguration Configuration { get; }
        public JwtAuthHelper(IConfiguration conf)
        {
            Configuration = conf;
        }

        public string GenerateJwttoken(Staff staff)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
                    {
                        new Claim("Username",staff.Username),
                        new Claim(ClaimTypes.Role,staff.Role.Name)
                    };

            var tokenOptions = new JwtSecurityToken(
                    issuer: Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: credential
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
