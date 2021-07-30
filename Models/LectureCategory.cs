using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class LectureCategory
    {
        public LectureCategory()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
