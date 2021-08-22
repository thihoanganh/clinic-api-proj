using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Models
{
    public class MailSupport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }

        public string Content { get; set; }
    }
}
