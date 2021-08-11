using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface ISeminarService
    {
        int Create(Seminar smn);
        int Delete(int id);
        bool UpdateSeminar(Seminar smn);
        Seminar Find(int id);
        List<Seminar> Find(string term);
        (List<Seminar> smns, int totalPage, int totalStaffs) FindAll(int page);
        int Register(SeminarRegistation sr);
        List<SeminarRegistation> FindAllRegisterOfSeminar(int smnId);
        int Feedback(Feedback fb);
        List<Feedback> GetSeminarFeedbacks(int smnId);
        double? Evaluate(int smnId);
        bool CreateEmail(SeminarEmail se);
        SeminarEmail GetAllEmails(int smnId);
        SeminarEmail UpdateEmail(SeminarEmail se);
        bool DeleteEmail(int id);

    }
}
