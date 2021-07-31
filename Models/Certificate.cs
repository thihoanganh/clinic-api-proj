using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime? Created { get; set; }
        public int? StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
