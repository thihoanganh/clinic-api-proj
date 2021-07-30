using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Level
    {
        public Level()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Bonus { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
