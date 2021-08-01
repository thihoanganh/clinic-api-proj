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
        List<Seminar> FindAll();
        int Register(SeminarRegistation sr);
        List<SeminarRegistation> FindAllRegisterOfSeminar(int smnId);
    }
}
