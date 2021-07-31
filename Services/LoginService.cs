using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Helpers;
using Microsoft.Extensions.Configuration;

namespace Clinic_Web_Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly ClinicDbContext _db;
        public IConfiguration _config { get; }
        public LoginService(ClinicDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        /// <summary>
        /// login method will return a token if login verified
        /// else return null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string username, string password)
        {
            try
            {
                var staff = _db.Staff.Where(s => s.Username == username && s.Password == password).FirstOrDefault();
                if (staff != null)
                {
                    //login verified
                    //generate token for that user
                    var jwtHelper = new JwtAuthHelper(_config);
                    var token = jwtHelper.GenerateJwttoken(staff);
                    return token;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
