using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public double? SatisfiedPercent { get; set; }
        public string Feeling { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int SeminarId { get; set; }

        public virtual Seminar Seminar { get; set; }
    }
}
