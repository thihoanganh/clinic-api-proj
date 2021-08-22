using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface ILectureService
    {
        int CreateCategory(LectureCategory lc);
        LectureCategory CategoryRenaming(int cateId, string newName);
        List<LectureCategory> FindAllCate();
        int DeleteLectureCate(int id);
        List<Attachment> CreateAttachments(List<Attachment> atms);
        Attachment CreateAttachment(Attachment atm);
        bool DeleteAttachment(int id);
        List<Attachment> GetAttachments(int lecId);
        int CreateLecture(Lecture lecture, int staffid);
        int DeleteLecture(int id);
        Lecture UpdateLecture(Lecture lecture);
        dynamic FindLecture(int lecId);
        (List<Quiz> quizzes, int totalPage, int totalQuiz) FindAllQuizzes(int page);
        (dynamic lecs, int totalPage, int totalLec) FindAllLecture(int page);
        List<Lecture> FindByCate(int cateId);

        Quiz CreateQuiz(Quiz qz);

        List<UserQuiz> GetUserQuiz(int id);
        int DeleteQuiz(int id);
        dynamic GetLectureQuizzes(int lecId);
        UserQuiz CreateUserQuiz(int quizId, int userId, int[] userAnswers);
        double GetLectureQuizPercent(int lecId, int userId);
        int CreateComment(LectureComment lc);
        int DeleteComment(int cmtId);
        dynamic GetLectureComments(int lecId);
        Quiz GetRandomQuiz(int lecId, int userId);
        List<Lecture> SearchLecture(string term);

        bool UpdateQuiz(Quiz quiz);
        dynamic FindQuiz(int id);

    }
}
