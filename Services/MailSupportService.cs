using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class MailSupportService : IMailSupportService
    {

        private readonly ClinicDbContext _db;
        public MailSupportService(ClinicDbContext db)
        {
            _db = db;
        }
        public int CreateMailSupport(MailSupport ms)
        {
            try
            {
                _db.MailSupports.Add(ms);
                _db.SaveChanges();
                return ms.Id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public List<MailSupport> FindAll()
        {
            return _db.MailSupports.ToList();
        }
    }
}
