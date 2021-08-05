using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
