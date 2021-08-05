using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class UserQuizInput
    {
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int[] Answers { get; set; }
    }
}
