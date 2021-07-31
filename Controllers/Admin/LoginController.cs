using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Services;
using Clinic_Web_Api.Services.Interface;

namespace Clinic_Web_Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(new { login = false });
            }
            else
            {
                var loginToken = _loginService.Login(loginModel.Username, loginModel.Password);
                if (loginToken != null) return Ok(new { login = true, token = loginToken });
            }
            return BadRequest(new { login = false });

        }
    }
}
