using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Role
    {
        public Role()
        {
            StaffRoles = new HashSet<StaffRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StaffRole> StaffRoles { get; set; }
    }
}
