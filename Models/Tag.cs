using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}
