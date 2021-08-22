using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Helpers;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lecService;
        private readonly IWebHostEnvironment _evn;
        public LectureController(ILectureService lecService, IWebHostEnvironment evn)
        {
            _lecService = lecService;
            _evn = evn;
        }
        [HttpPost("category")]
        public IActionResult CreateCate(LectureCategory lc)
        {
            var rs = _lecService.CreateCategory(lc);
            if (rs == -1) return BadRequest(new { result = "error", mgs = "can not create new lecture category" });
            else return Ok(new { result = "success", id = rs });
        }

        [HttpPut("category/{id}")]
        public IActionResult CateRenaming(int id, string name)
        {
            var rs = _lecService.CategoryRenaming(id, name);
            if (rs == null) return BadRequest(new { result = "error", mgs = "can not rename this category" });
            else return Ok(new { result = "success", category = new { id = rs.Id, name = rs.Name } });
        }

        [HttpGet("categories")]
        public IActionResult FindAllCategory()
        {
            var rs = _lecService.FindAllCate();
            return Ok(new
            {
                categories = rs.Select(l => new
                {
                    id = l.Id,
                    name = l.Name,
                    total_lectures = l.Lectures.Count()
                })
            });
        }
        [HttpDelete("category/{id}")]
        public IActionResult CateDelete(int id)
        {
            var rs = _lecService.DeleteLectureCate(id);
            if (rs == -1) return BadRequest(new { result = "error", msg = "Can not delete. Maybe cause of constrain. Check again" });
            else return Ok(new { result = "success", id = rs });
        }

        [HttpPost("attach")]
        public IActionResult CreateAttachment([FromForm] IFormFile attach, [FromForm] string lectureId)
        {
            if (attach == null || lectureId == null)
            {
                return BadRequest(new { result = "error", mgs = "lack of data" });
            }
            else
            {

                // upload attachment to server
                var fileHelper = new FileHelper(_evn);
                var validate = fileHelper.FileValidate((long)Math.Pow(1024, 3), new string[] { ".jpg", ".txt", ".png", ".docx", ".xlsx", ".mp3", ".mp4" }, attach);
                if (validate)
                {
                    var atm = fileHelper.UploadFile(attach, "lecture/attach");
                    atm.LectureId = int.Parse(lectureId);
                    var rs = _lecService.CreateAttachment(atm);
                    if (rs != null) return Ok(new { result = "success" });
                }
            }
            return BadRequest(new { result = "error", msg = "server can not serve data input" });
        }

        [HttpPost("attachs")]
        public IActionResult CreateAttachments([FromForm] List<IFormFile> attachs, [FromForm] string lectureId)
        {
            if (attachs == null || lectureId == null)
            {
                return BadRequest(new { result = "error", mgs = "lack of data" });
            }
            else
            {

                // upload attachment to server
                var fileHelper = new FileHelper(_evn);
                //validate file 
                var validate = true;
                attachs.ForEach(att =>
                {
                    var rs = fileHelper.FileValidate((long)Math.Pow(1024, 3), new string[] { ".jpg", ".txt", ".png", ".docx", ".xlsx", ".mp3", ".mp4" }, att);
                    if (rs == false) validate = false;
                });

                if (validate)
                {
                    // verified 
                    var atms = fileHelper.UploadFiles(attachs, "lecture/attach"); // upload
                    atms.ForEach(atm => atm.LectureId = int.Parse(lectureId));
                    var rs = _lecService.CreateAttachments(atms);//save db 
                    if (rs != null) return Ok(new { result = "success", created_count = rs.Count });
                }
            }
            return BadRequest(new { result = "error", msg = "server can not serve data input" });
        }

        [HttpDelete("attach/{id}")]
        public IActionResult DeleteAttach(int id)
        {
            if (_lecService.DeleteAttachment(id))
            {
                return Ok(new { result = "success" });
            }
            else return BadRequest(new { result = "error", msg = "can not delete attachment" });
        }

        [HttpGet("{id}/attachs/zip")]
        public IActionResult GetZipAttachs(int id)
        {
            var atms = _lecService.GetAttachments(id);
            if (atms.Count != 0)
            {
                var fileHelper = new FileHelper(_evn);
                var zipStream = fileHelper.Zip(atms);
                return File(zipStream, "application/octet-stream", "lecture_" + id.ToString() + ".zip");
            }
            return NotFound(new { result = "error", msg = "no attachments was found" });
        }

        [HttpGet("{id}/attachs")]
        public IActionResult GetAttachs(int id)
        {
            var atms = _lecService.GetAttachments(id);
            return Ok(new { result = atms.Select(atm => new { id = atm.Id, name = atm.Name, original_name = atm.OriginName, type = atm.Type, size = atm.Size }), count = atms.Count() });
        }

        [HttpPost]
        public IActionResult CreateLecture(Lecture lecture, int staffid)
        {
            if (lecture != null)
            {
                var rs = _lecService.CreateLecture(lecture, staffid);
                if (rs != -1) return Ok(new { result = "success", lec_id = rs });
            }
            return BadRequest(new { result = "error", msg = "can not create lecture" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLecture(int id)
        {
            var rs = _lecService.DeleteLecture(id);
            if (id != 0 && rs != -1) return Ok(new { result = "success", id = rs });
            return BadRequest(new { result = "error", msg = "can not delete" });
        }
        [HttpPut]
        public IActionResult UpdateLecture(Lecture lec)
        {
            if (lec != null)
            {
                var rs = _lecService.UpdateLecture(lec);
                if (rs != null) return Ok(new { result = "success", lecture = rs });
            }
            return BadRequest(new { result = "error", msg = "can not update lecture" });
        }

        [HttpGet("{id}")]
        public IActionResult FindLecture(int id)
        {
            if (id != 0)
            {
                var rs = _lecService.FindLecture(id);
                if (rs != null) return Ok(new { result = rs });
            }
            return NotFound(new { error = "object not found" });
        }

        [HttpGet("all")]
        public IActionResult FindAllLecture(int page)
        {
            if (page == -1)
            {
                return BadRequest();
            }
            var rs = _lecService.FindAllLecture(page);
            return Ok(new { result = rs.lecs, totalPage = rs.totalPage, totalLectures = rs.totalLec });
        }

        [HttpGet("search")]
        public IActionResult LectureSearch(string term)
        {
            var rs = _lecService.SearchLecture(term);
            return Ok(new { result = rs.Select(l => new { id = l.Id, name = l.Name }), total_result = rs.Count() });
        }

        [HttpGet]
        public IActionResult FindByCate(int cateid)
        {
            if (cateid != 0)
            {
                var rs = _lecService.FindByCate(cateid);
                return Ok(new { result = rs });
            }
            return BadRequest(new { result = "error", msg = "object not found" });

        }
        [HttpGet("quiz/{id}")]
        public IActionResult GetQuizById(int id)
        {
            if (id == 0) return BadRequest();
            else
            {
                return Ok(_lecService.FindQuiz(id));

            }
        }
        [HttpPost("quiz")]
        public IActionResult CreateQuiz([FromBody] Quiz qz)
        {
            var answerValidate = true;
            qz.Questions.ToList().ForEach(q => { if (q.Answers.Count < 2) answerValidate = false; });
            if (qz == null || qz.Questions.Count < 2 || !answerValidate) return BadRequest(new { result = "error", msg = "a quiz must be more than 5 questions with up to 2 answers " }); // require a quiz must be more than 5 questions // each with up to 2 answer
            else
            {
                _lecService.CreateQuiz(qz);
                return Ok(new { result = "success" });

            }

        }
        [HttpPut("quiz")]
        public IActionResult UpdateQuiz([FromBody] Quiz quiz)
        {
            var rs = _lecService.UpdateQuiz(quiz);
            if (rs) return Ok(new { result = "success", msg = "Update successfully" });
            else return NotFound(new { result = "error", msg = "Object not found" });
        }
        [HttpDelete("quiz/{id}")]
        public IActionResult DeleteQuiz(int id)
        {
            var delId = _lecService.DeleteQuiz(id);
            if (delId == -1) return BadRequest();
            else
            {
                return Ok(new { status = "success", delId = delId });
            }
        }

        [HttpGet("{id}/quizzes")]
        public IActionResult FindAllLectureQuizzes(int id)
        {
            if (id != 0)
            {
                var rs = _lecService.GetLectureQuizzes(id);
                return Ok(new { quizzes = rs, count = rs.Count });
            }
            return BadRequest(new { result = "error", msg = "object not found" });
        }


        [HttpPost("quiz/submit")]

        public IActionResult CreateUserQuiz([FromBody] UserQuizInput uq)
        {
            if (uq.UserId != 0 && uq.QuizId != 0)
            {
                var quizResult = _lecService.CreateUserQuiz(uq.QuizId, uq.UserId, uq.Answers);
                if (quizResult != null) return Ok(new
                {
                    result = new
                    {
                        total_question = quizResult.TotalQuestion,
                        correct_answer = quizResult.CorrectAnswer,
                        no_answer = quizResult.NoAnswer,
                        percent = quizResult.Percent
                    }
                });
            }
            return BadRequest(new { result = "error", msg = "can not initial an user quiz" });
        }

        [HttpGet("quiz/percent")]
        public IActionResult GetLectureQuizPercent(int lecid, int userid)
        {
            if (lecid != 0 && userid != 0)
            {
                var percent = _lecService.GetLectureQuizPercent(lecid, userid);
                return Ok(new { result = "success", percent = percent });
            }
            return BadRequest(new { result = "error" });

        }

        [HttpGet("random/quiz")]
        public IActionResult GetRandomLectureQuiz(int lecid, int userid)
        {
            var rs = _lecService.GetRandomQuiz(lecid, userid);
            if (rs == null) return Ok(new { status = false, msg = "There no quiz for this user in this lecture. Try another one !" });
            return Ok(new { status = true, result = rs, total_question = rs.Questions.Count });
        }

        [HttpGet("user/{id}/quiz")]
        public IActionResult GetUserQuiz(int id)
        {
            return Ok(_lecService.GetUserQuiz(id).Select(q => new
            {
                quizId = q.QuizId,
                userId = q.UserId,
                totalQuestion = q.TotalQuestion,
                correctAnswer = q.CorrectAnswer,
                noAnswer = q.NoAnswer,
                percent = q.Percent,
                examinatedDate = q.ExaminatedDate,
                levelName = q.Quiz.Level.Name
            }));
        }

        [HttpGet("quizzes")]
        public IActionResult GetAllQuizzes(int page)
        {
            var rs = _lecService.FindAllQuizzes(page);
            if (rs.quizzes == null) return Ok(new { status = false, msg = "Some trouble was occurs. Try again !" });
            else
            {
                return Ok(new
                {
                    status = true,
                    result = rs.quizzes.Select(s => new
                    {
                        id = s.Id,
                        duration = s.Duration,
                        level_id = s.LevelId,
                        level_name = s.Level.Name,
                        total_question = s.TotalQuestion
                    }),
                    total_quiz = rs.totalQuiz,
                    total_page = rs.totalPage
                });
            }
        }

        [HttpPost("comment")]
        public IActionResult CreateComment(LectureComment lc)
        {
            if (lc != null)
            {
                var rs = _lecService.CreateComment(lc);
                if (rs != -1) return Ok(new { result = "success", id = rs });
            }
            return BadRequest(new { result = "error", msg = "can not create comment" });
        }

        [HttpDelete("comment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            if (id != 0)
            {
                var rs = _lecService.DeleteComment(id);
                if (rs != -1) return Ok(new { result = "success" });
            }
            return NotFound(new { result = "error", msg = "Object not found. Can not delete " });
        }

        [HttpGet("{id}/comment")]
        public IActionResult GetLectureComments(int id)
        {
            if (id != 0)
            {
                dynamic cmts = _lecService.GetLectureComments(id);
                return Ok(new { cmts });
            }
            return BadRequest(new { result = "error", msg = "Can not serve your request" });
        }

    }
}
