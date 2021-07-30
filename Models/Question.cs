using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CorrectAnsw { get; set; }
        public int? QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
