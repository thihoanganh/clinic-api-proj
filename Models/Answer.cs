using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? Index { get; set; }
        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
