using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Helpers;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Clinic_Web_Api.Services
{
    public class SeminaService : ISeminarService
    {
        private readonly IWebHostEnvironment _evn;
        private readonly ClinicDbContext _db;
        public SeminaService(ClinicDbContext db, IWebHostEnvironment evn)
        {
            _db = db;
            _evn = evn;
        }
        public int Create(Seminar smn)
        {
            try
            {
                _db.Seminars.Add(smn);
                _db.SaveChanges();
                return smn.Id;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return -1;
            }
        }

        public int Delete(int id)
        {

            try
            {
                var smn = _db.Seminars.Where(s => s.Id == id).FirstOrDefault();
                var path = Path.Combine(_evn.WebRootPath, "lecture/attach", smn.Poster);

                if (smn != null && System.IO.File.Exists(path))
                {
                    _db.Seminars.Remove(smn);
                    _db.SaveChanges();
                    System.IO.File.Delete(path);
                    return id;
                }
                return -1;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public Seminar Find(int id)
        {
            try
            {
                return _db.Seminars.Find(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Seminar> Find(string term)
        {
            var smn = _db.Seminars.Where(s => s.Title.Contains(term)).Include(s => s.SeminarEmail).ToList();
            if (smn != null) return smn;
            return null;
        }

        public List<Seminar> FindAll()
        {
            try
            {
                return _db.Seminars.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<SeminarRegistation> FindAllRegisterOfSeminar(int smnId)
        {
            try
            {
                return _db.SeminarRegistations.Where(r => r.SeminarId == smnId).ToList();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public int Register(SeminarRegistation sr)
        {
            try
            {
                if (Find(sr.SeminarId) == null) return -1;
                else
                {
                    _db.SeminarRegistations.Add(sr);
                    _db.SaveChanges();
                    return sr.Id;
                }

            }
            catch (Exception)
            {
                return -1;
            }

        }

        public bool UpdateSeminar(Seminar smn)
        {
            var dbSmn = _db.Staff.Find(smn.Id);
            if (dbSmn == null) return false;
            else
            {
                _db.Entry(dbSmn).CurrentValues.SetValues(smn);
                _db.SaveChanges();
                return true;
            }
        }

        public int Feedback(Feedback fb)
        {
            try
            {
                fb.CreatedDate = DateTime.Now;
                if (Find(fb.SeminarId) == null) return -1;
                else
                {
                    _db.Feedbacks.Add(fb);
                    _db.SaveChanges();
                    return fb.Id;
                }

            }
            catch (Exception)
            {
                return -1;
                throw;

            }
        }

        public List<Feedback> GetSeminarFeedbacks(int smnId)
        {
            return _db.Feedbacks.Where(f => f.SeminarId == smnId).ToList();
        }
        public double? Evaluate(int smnId)
        {
            if (Find(smnId) == null) return -1;
            else return _db.Feedbacks.Where(f => f.SeminarId == smnId).Average(f => f.SatisfiedPercent);

        }

        public bool CreateEmail(SeminarEmail se)
        {
            if (Find(se.SeminarId) == null) return false;
            else
            {
                try
                {
                    _db.SeminarEmails.Add(se);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
        public bool DeleteEmail(int id)
        {
            try
            {
                _db.SeminarEmails.Remove(_db.SeminarEmails.Find(id));
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public SeminarEmail UpdateEmail(SeminarEmail se)
        {
            var dbSe = _db.SeminarEmails.Find(se.Id);
            if (dbSe == null) return null;
            else
            {
                _db.Entry(dbSe).CurrentValues.SetValues(se);
                _db.SaveChanges();
                return se;
            }
        }

        public List<SeminarEmail> GetAllEmails(int smnId)
        {
            return _db.SeminarEmails.Where(se => se.SeminarId == smnId).ToList();
        }
    }
}



