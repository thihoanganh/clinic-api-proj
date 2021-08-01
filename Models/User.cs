using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class User
    {
        public User()
        {
            DetailOrders = new HashSet<DetailOrder>();
            Feedbacks = new HashSet<Feedback>();
            LectureComments = new HashSet<LectureComment>();
            UserQuizzes = new HashSet<UserQuiz>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<LectureComment> LectureComments { get; set; }
        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }

    }
}
