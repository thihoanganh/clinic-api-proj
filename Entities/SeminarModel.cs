using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class SeminarModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Speaker { get; set; }
        public string Method { get; set; }
        public string Content { get; set; }
        public string Place { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Contact { get; set; }
        public IFormFile Poster { get; set; }
    }
}
