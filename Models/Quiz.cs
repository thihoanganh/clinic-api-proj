using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
            UserQuiz = new HashSet<UserQuiz>();
        }

        public int Id { get; set; }
        public int? Duration { get; set; }
        public int? LevelId { get; set; }
        public int? LectureId { get; set; }
        public int? TotalQuestion { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserQuiz> UserQuiz { get; set; }
    }
}
