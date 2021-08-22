using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class UserQuiz
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int? TotalQuestion { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? NoAnswer { get; set; }
        public double Percent { get; set; }

        public DateTime ExaminatedDate { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual User User { get; set; }
    }
}
