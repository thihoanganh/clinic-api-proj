using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Seminar
    {
        public Seminar()
        {
            Feedbacks = new HashSet<Feedback>();
            SeminarRegistations = new HashSet<SeminarRegistation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Speaker { get; set; }
        public string Method { get; set; }
        public string Content { get; set; }
        public string Place { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Contact { get; set; }
        public string Poster { get; set; }

        public virtual SeminarEmail IdNavigation { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<SeminarRegistation> SeminarRegistations { get; set; }
    }
}
