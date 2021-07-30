using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class SeminarEmail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int SeminarId { get; set; }

        public virtual Seminar Seminar { get; set; }
    }
}
