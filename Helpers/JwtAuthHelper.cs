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

        public string GenerateJwttoken(Object obj)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            if (obj.GetType() == typeof(Staff))
            {
                // require two claim with object of staff 
                // username & role
                var staff = obj as Staff;
                claims.Add(new Claim("Username", staff.Username));
                claims.Add(new Claim("IsAdmin", staff.Role.Name == "Admin" ? "true" : "false", "bool"));
                claims.Add(new Claim(ClaimTypes.Role, staff.Role.Name));
            }
            else
            {
                // it's user
                // just claims with username of user
                var user = obj as User;
                claims.Add(new Claim("Username", user.Username));
            }


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
