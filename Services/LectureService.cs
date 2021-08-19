using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return _db.LectureCategories.Include(l => l.Lectures).ToList();
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
        public (dynamic lecs, int totalPage, int totalLec) FindAllLecture(int page)
        {
            try
            {
                var Size = 6;
                var TotalLec = _db.Lectures.Count();
                var TotalPage = (int)Math.Ceiling(((double)TotalLec / Size));
                return (_db.Lectures.Include(lec => lec.Cate).Include(lec => lec.Quizzes).Include(l => l.Attachments).Include(lec => lec.LectureComments).OrderByDescending(l => l.Id).Skip((page - 1) * Size).Take(Size).Select(lec => new
                {
                    id = lec.Id,
                    name = lec.Name,
                    sumary = lec.Sumary,
                    content = lec.Content,
                    createdby = lec.CreatedBy,
                    createddate = lec.CreateDate,
                    modifyby = lec.ModifyBy,
                    modifydate = lec.ModifyDate,
                    cateid = lec.CateId,
                    cate_name = lec.Cate.Name,
                    total_quizzes = lec.Quizzes.Count,
                    total_comments = lec.LectureComments.Count,
                    total_attachs = lec.Attachments.Count
                }), TotalPage, TotalLec);
            }
            catch (Exception)
            {
                return (null, -1, -1);
                throw;
            }

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

        public (List<Quiz> quizzes, int totalPage, int totalQuiz) FindAllQuizzes(int page)
        {
            try
            {
                var Size = 6;
                var TotalQuiz = _db.Quizzes.Count();
                var TotalPage = (int)Math.Ceiling(((double)TotalQuiz / Size));
                return (_db.Quizzes.Include(q => q.Level).OrderByDescending(l => l.Id).Skip((page - 1) * Size).Take(Size).ToList(), TotalPage, TotalQuiz);
            }
            catch (Exception)
            {
                return (null, -1, -1);
                throw;
            }

        }

        public dynamic GetLectureQuizzes(int lecId)
        {
            try
            {
                return _db.Quizzes.Where(q => q.LectureId == lecId).Select(q => new { id = q.Id, duration = q.Duration, level_name = q.Level.Name, total_question = q.TotalQuestion }).ToList();
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
                throw;
            }

        }

        public bool UpdateQuiz(Quiz quiz)
        {
            try
            {
                var dbQuiz = _db.Quizzes.Where(q => q.Id == quiz.Id).FirstOrDefault();
                if (dbQuiz == null) return false;
                else
                {
                    _db.Entry(dbQuiz).CurrentValues.SetValues(quiz); // this only work with scalar properties
                    _db.SaveChanges();
                    UpdateQuestion(quiz.Questions.ToList());
                    foreach (var question in quiz.Questions)
                    {
                        UpdateAnswer(question.Answers.ToList());
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
                throw;
            }
        }
        private void UpdateQuestion(List<Question> questions)
        {
            foreach (var qs in questions)
            {
                var dbQuestion = _db.Questions.Where(q => q.Id == qs.Id).FirstOrDefault();
                if (dbQuestion == null)
                {
                    _db.Questions.Add(qs);

                }
                else
                {
                    dbQuestion.Name = qs.Name;

                }

            }
            _db.SaveChanges();
        }

        private void UpdateAnswer(List<Answer> answers)
        {
            foreach (var answer in answers)
            {
                var dbAnswer = _db.Answers.Where(q => q.Id == answer.Id).FirstOrDefault();
                if (dbAnswer == null)
                {
                    _db.Answers.Add(answer);
                }
                else
                {
                    dbAnswer.Content = answer.Content;
                    dbAnswer.IsCorrect = answer.IsCorrect;
                }
            }
            _db.SaveChanges();
        }



        public double GetLectureQuizPercent(int lecId, int userId)
        {
            var quizzesOfLecture = _db.Quizzes.Where(q => q.LectureId == lecId).Include(q => q.UserQuizzes).ToList();
            var count = 0;
            double percent = 0;
            quizzesOfLecture.ForEach(q =>
            {
                var userQuiz = q.UserQuizzes.Where(uq => uq.UserId == userId).FirstOrDefault();
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
            return _db.LectureComments.Where(c => c.LectureId == lecId).OrderByDescending(c => c.Id).Select(cmt => new
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
                if (quiz.Id != 0) return _db.Quizzes.Where(q => q.Id == quiz.Id).Include(q => q.Level).Include(q => q.Questions).ThenInclude(q => q.Answers).FirstOrDefault();
                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }


        }
        public List<Lecture> SearchLecture(string term)
        {
            try
            {
                return _db.Lectures.Where(l => l.Name.Contains(term)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public int DeleteQuiz(int id)
        {
            try
            {
                var quiz = _db.Quizzes.Find(id);
                if (quiz == null) return -1;
                else
                {
                    _db.Quizzes.Remove(quiz);
                    _db.SaveChanges();
                    return quiz.Id;
                }
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public dynamic FindQuiz(int id)
        {
            return _db.Quizzes.Where(q => q.Id == id).Include(q => q.Level).Include(q => q.Lecture).Include(q => q.Questions).Select(q => new
            {
                duration = q.Duration,
                lectureid = q.LectureId,
                lecture_name = q.Lecture.Name,
                level_name = q.Level.Name,
                levelid = q.LevelId,
                questions = q.Questions.Select(q => new
                {
                    id = q.Id,
                    name = q.Name,
                    answers = q.Answers.Select(a => new
                    {
                        id = a.Id,
                        content = a.Content,
                        iscorrect = a.IsCorrect
                    })
                })
            });
        }

        private bool CheckAnswer(int answerId)
        {
            return (bool)_db.Answers.Find(answerId).IsCorrect;
        }


    }
}
