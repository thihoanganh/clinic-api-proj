using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class LectureComment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual User User { get; set; }
    }
}
