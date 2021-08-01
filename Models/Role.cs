using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Role
    {
        public Role()
        {
            Staffs = new HashSet<Staff>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Staff> Staffs { get; set; }
    }
}
