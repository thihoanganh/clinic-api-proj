using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportMailController : ControllerBase
    {
        private readonly IMailSupportService _mailService;
        public SupportMailController(IMailSupportService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost]
        public IActionResult CreateMailSupport(MailSupport ms)
        {
            var rs = _mailService.CreateMailSupport(ms);
            if (rs != -1)
            {
                return Ok(new { status = true });
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(_mailService.FindAll());
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

    }
}
