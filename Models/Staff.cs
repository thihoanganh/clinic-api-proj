using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Certificates = new HashSet<Certificate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
        public DateTime? WokingStart { get; set; }

        public virtual Position Position { get; set; }
        public int? PositionId { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
