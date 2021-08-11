using Clinic_Web_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using Clinic_Web_Api.Services.Interface;
using Clinic_Web_Api.Entities;
using Newtonsoft.Json;
using System.IO;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeminarController : ControllerBase
    {
        private readonly IWebHostEnvironment _evn;
        private readonly ISeminarService _smnService;
        private readonly IEmailSender _mailSender;


        public SeminarController(IWebHostEnvironment evn, ISeminarService smnService, IEmailSender mailSender)
        {
            _evn = evn;
            _smnService = smnService;
            _mailSender = mailSender;
        }
        [HttpPost]
        public IActionResult Create([FromForm] IFormFile poster, [FromForm] string seminar)
        {
            var smn = JsonConvert.DeserializeObject<Seminar>(seminar);
            if (smn != null && poster != null)
            {
                var fileHelper = new FileHelper(_evn);
                var uploadFile = fileHelper.UploadPoster(poster, "seminar/image");
                if (uploadFile != null)
                {
                    // upload poster success
                    smn.Poster = uploadFile;
                    var rs = _smnService.Create(smn);
                    if (rs != -1)
                    {
                        return Ok(new { result = "success", id = rs });
                    }
                }
            }
            return BadRequest(new { result = "error", msg = "can not create" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rs = _smnService.Delete(id);
            if (rs != -1)
            {
                return Ok(new { delId = rs });
            }
            return NotFound(new { msg = "Staff not found" });
        }

        [HttpGet]
        public IActionResult GetAll(int page)
        {
            var rs = _smnService.FindAll(page);


            return Ok(new
            {
                result = rs.smns.Select(s => new
                {
                    id = s.Id,
                    title = s.Title,
                    speaker = s.Speaker,
                    method = s.Method,
                    content = s.Content,
                    place = s.Place,
                    startAt = s.StartAt,
                    endAt = s.EndAt,
                    contact = s.Content,
                    poster = Path.Combine("https://localhost:5001", @"seminar/image", s.Poster),
                    seminarEmail = _smnService.GetAllEmails(s.Id),
                    totalFeedback = s.Feedbacks.Count(),
                    totalRegistered = s.SeminarRegistations.Count(),
                    evaluate = _smnService.Evaluate(s.Id)
                }).ToList()
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var staff = _smnService.Find(id);
            if (staff == null)
            {
                return NotFound(new { msg = "Object not found" });
            }
            return Ok(staff);
        }

        [HttpGet("search")]
        public IActionResult GetByName(string q)
        {
            var staff = _smnService.Find(q);
            if (staff == null)
            {
                return NotFound(new { result = "error", msg = "Object not found" });
            }
            return Ok(staff);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Seminar smn)
        {
            var rs = _smnService.UpdateSeminar(smn);
            if (rs) return Ok(new { result = "success", msg = "Update successfully" });
            else return NotFound(new { result = "error", msg = "Object not found" });
        }

        [HttpPost("register")]
        public IActionResult RegisterSeminar([FromBody] SeminarRegistation sr)
        {
            var rs = _smnService.Register(sr);
            if (rs == -1) return BadRequest(new { result = "error", msg = "can not regist seminar for this user" });
            else
            {
                // register successfully 
                // send an email for that user
                var mail = _smnService.GetAllEmails(sr.SeminarId);
                if (mail != null)
                {
                    _mailSender.SendEmailAsync(sr.Email, mail.Title, mail.Content);
                }

                return Ok(new { result = "success", id = rs });
            }
        }

        [HttpGet("{id}/registers")]
        public IActionResult GetRegistereds(int id)
        {
            var rs = _smnService.FindAllRegisterOfSeminar(id);
            return Ok(new { result = rs, count = rs.Count() });
        }

        [HttpPost("feedback")]
        public IActionResult Feedback(Feedback fb)
        {
            var rs = _smnService.Feedback(fb);
            if (rs != -1) return Ok(new { result = "success", id = rs });
            else return BadRequest(new { result = "error", msg = "can not create feedback" });
        }

        [HttpGet("{id}/feedbacks")]
        public IActionResult GetFeedbacks(int id)
        {
            var rs = _smnService.GetSeminarFeedbacks(id);
            return Ok(new { result = rs, count = rs.Count() });
        }

        [HttpGet("{id}/evaluate")]
        public IActionResult GetEvaluate(int id)
        {
            var rs = _smnService.Evaluate(id);
            if (rs == -1) return BadRequest(new { result = "error", msg = "seminar not found" });
            else return Ok(new { result = "success", percent = rs });
        }

        [HttpPost("{id}/email")]
        public IActionResult CreateEmail(SeminarEmail se)
        {
            if (_smnService.CreateEmail(se)) return Ok(new { result = "success" });
            else return BadRequest(new { result = "error", msg = "can not create email" });
        }

        [HttpDelete("email/{id}")]
        public IActionResult DeleteEmail(int id)
        {
            if (_smnService.DeleteEmail(id)) return Ok(new { result = "success" });
            else return BadRequest(new { result = "error", msg = "can not delete email" });
        }

        [HttpPut("email")]
        public IActionResult UpdateEmail(SeminarEmail se)
        {
            var rs = _smnService.UpdateEmail(se);
            if (rs == null) return BadRequest(new { result = "error", msg = "can not update email" });
            else return Ok(new { result = "success", email = rs });
        }

        [HttpGet("{id}/emails")]
        public IActionResult GetEmails(int id)
        {
            var rs = _smnService.GetAllEmails(id);
            return Ok(new { emails = rs });
        }

    }
}
