using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Attachments = new HashSet<Attachment>();
            LectureComments = new HashSet<LectureComment>();
            Quizzes = new HashSet<Quiz>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sumary { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CateId { get; set; }

        public virtual LectureCategory Cate { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<LectureComment> LectureComments { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
