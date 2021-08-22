using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IMailSupportService
    {
        int CreateMailSupport(MailSupport ms);
        List<MailSupport> FindAll();
    }
}
