using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class UpdateUserPassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
