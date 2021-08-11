using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LectureId { get; set; }
        public string OriginName { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}
