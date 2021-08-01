using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class SeminarRegistation
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public int SeminarId { get; set; }

        public virtual Seminar Seminar { get; set; }

    }
}
