using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class LectureService : ILectureService
    {
        private readonly ClinicDbContext _db;
        private readonly IWebHostEnvironment _evn;
        public IConfiguration _config { get; }
        public LectureService(ClinicDbContext db, IConfiguration config, IWebHostEnvironment evn)
        {
            _db = db;
            _config = config;
            _evn = evn;
        }
        public int CreateCategory(LectureCategory lc)
        {
            try
            {
                _db.LectureCategories.Add(lc);
                _db.SaveChanges();
                return lc.Id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }
        public LectureCategory CategoryRenaming(int cateId, string newName)
        {
            var lectureCate = _db.LectureCategories.Find(cateId);
            if (lectureCate == null) return null;
            else
            {
                lectureCate.Name = newName;
                _db.SaveChanges();
                return lectureCate;
            }
        }
        public List<LectureCategory> FindAllCate()
        {
            return _db.LectureCategories.ToList();
        }

        public int DeleteLectureCate(int id)
        {
            try
            {
                _db.LectureCategories.Remove(_db.LectureCategories.Find(id));
                _db.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public List<Attachment> CreateAttachments(List<Attachment> atms)
        {
            try
            {
                _db.Attachments.AddRange(atms);
                _db.SaveChanges();
                return atms;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Attachment CreateAttachment(Attachment atm)
        {
            try
            {
                _db.Attachments.Add(atm);
                _db.SaveChanges();
                return atm;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public bool DeleteAttachment(int id)
        {
            try
            {
                var atm = _db.Attachments.Find(id);
                var path = Path.Combine(_evn.WebRootPath, "lecture/attach", atm.Name);
                if (atm != null && System.IO.File.Exists(path))
                {
                    _db.Attachments.Remove(atm);
                    _db.SaveChanges();
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;


            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public List<Attachment> GetAttachments(int lecId)
        {
            try
            {
                return _db.Attachments.Where(att => att.LectureId == lecId).ToList();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public int CreateLecture(Lecture lecture, int staffid)
        {
            try
            {
                var staff = _db.Staff.Find(staffid);
                if (staff != null)
                {
                    lecture.CreateDate = DateTime.Now;
                    lecture.CreatedBy = staff.Username;
                    _db.Lectures.Add(lecture);
                    _db.SaveChanges();
                    return lecture.Id;
                }
                return -1;

            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        public int DeleteLecture(int id)
        {
            try
            {
                _db.Lectures.Remove(_db.Lectures.Find(id));
                _db.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        public Lecture UpdateLecture(Lecture lecture)
        {
            try
            {
                var lec = _db.Lectures.Find(lecture.Id);
                if (lec != null)
                {
                    _db.Entry(lec).CurrentValues.SetValues(lecture);
                    _db.SaveChanges();
                    return lecture;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
        public dynamic FindLecture(int lecId)
        {
            return _db.Lectures.Where(l => l.Id == lecId).Include(lec => lec.Cate).Select(lec => new { id = lec.Id, name = lec.Name, sumary = lec.Sumary, content = lec.Content, createdby = lec.CreatedBy, createddate = lec.CreateDate, modifyby = lec.ModifyBy, modifydate = lec.ModifyDate, cateid = lec.CateId, cate_name = lec.Cate.Name }); ;
        }
        public dynamic FindAllLecture()
        {
            return _db.Lectures.Include(lec => lec.Cate).Include(lec => lec.Quizzes).Include(lec => lec.LectureComments).Select(lec => new { id = lec.Id, name = lec.Name, sumary = lec.Sumary, content = lec.Content, createdby = lec.CreatedBy, createddate = lec.CreateDate, modifyby = lec.ModifyBy, modifydate = lec.ModifyDate, cateid = lec.CateId, cate_name = lec.Cate.Name, total_quizzes = lec.Quizzes.Count, total_comments = lec.LectureComments.Count });
        }
        public List<Lecture> FindByCate(int cateId)
        {
            return _db.Lectures.Where(l => l.CateId == cateId).ToList();
        }

        public Quiz CreateQuiz(Quiz qz)
        {
            try
            {
                qz.TotalQuestion = qz.Questions.Count;
                _db.Quizzes.Add(qz);
                _db.SaveChanges();
                _db.Entry(qz).Reference(qz => qz.Questions).Load();

                return qz;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
        public dynamic GetLectureQuizzes(int lecId)
        {
            try
            {
                return _db.Quizzes.Where(q => q.LectureId == lecId).Select(q => new { duration = q.Duration, level = q.Level.Name, total_question = q.TotalQuestion }).ToList();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public UserQuiz CreateUserQuiz(int quizId, int userId, int[] userAnswers)
        {
            try
            {
                var quiz = _db.Quizzes.Where(q => q.Id == quizId).Include(q => q.Level).Include(q => q.Questions).FirstOrDefault();
                var quizBonus = quiz.Level.Bonus; // each level with each own bonus
                var totalQuestion = quiz.Questions.Count();
                var correctAnswer = 0;
                var noAnswer = totalQuestion - userAnswers.Length;
                userAnswers.ToList().ForEach(answerId =>
                {
                    if (CheckAnswer(answerId)) correctAnswer++;
                });
                double resultPercent = ((double)correctAnswer / (double)totalQuestion) * 100;
                resultPercent = resultPercent + (resultPercent * quizBonus) / 100;

                var userQuiz = new UserQuiz()
                {
                    UserId = userId,
                    QuizId = quizId,
                    TotalQuestion = totalQuestion,
                    CorrectAnswer = correctAnswer,
                    NoAnswer = noAnswer,
                    Percent = resultPercent
                };

                _db.UserQuizzes.Add(userQuiz);
                _db.SaveChanges();
                return userQuiz;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        public double GetLectureQuizPercent(int lecId, int userId)
        {
            var quizzesOfLecture = _db.Quizzes.Where(q => q.LectureId == lecId).Include(q => q.UserQuiz).ToList();
            var count = 0;
            double percent = 0;
            quizzesOfLecture.ForEach(q =>
            {
                var userQuiz = q.UserQuiz.Where(uq => uq.UserId == userId).FirstOrDefault();
                if (userQuiz != null)
                {
                    percent += userQuiz.Percent;
                    count++;
                }
            });
            return (double)percent / (double)count;
        }
        public int CreateComment(LectureComment lc)
        {
            try
            {
                lc.CreatedDate = DateTime.Now;
                _db.LectureComments.Add(lc);
                _db.SaveChanges();
                return lc.Id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }
        public int DeleteComment(int cmtId)
        {
            try
            {
                _db.LectureComments.Remove(_db.LectureComments.Find(cmtId));
                _db.SaveChanges();
                return cmtId;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }
        public dynamic GetLectureComments(int lecId)
        {
            return _db.LectureComments.Where(c => c.LectureId == lecId).Select(cmt => new
            {
                id = cmt.Id,
                content = cmt.Content,
                createddate = cmt.CreatedDate,
                createdby = cmt.User.Username
            }).ToList();
        }
        public Quiz GetRandomQuiz(int lecId, int userId)
        {

            var quiz = _db.Quizzes.FromSqlInterpolated($"exec GetUserQuiz @UserId={userId},@LectureId={lecId}").AsEnumerable().FirstOrDefault();
            try
            {
                if (quiz.Id != 0) return _db.Quizzes.Where(q => q.Id == quiz.Id).Include(q => q.Questions).ThenInclude(q => q.Answers).FirstOrDefault();
                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }


        }




        private bool CheckAnswer(int answerId)
        {
            return _db.Answers.Find(answerId).IsCorrect;
        }


    }
}
