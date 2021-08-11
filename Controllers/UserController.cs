using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IEmailSender _mailSender;
        public UserController(IUserService userService, IEmailSender mailSender)
        {
            _userService = userService;
            _mailSender = mailSender;
        }
        [HttpPost]
        public IActionResult CreateStaff([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { result = "error", msg = "user is empty" });
            }
            else
            {
                var userId = _userService.CreateUser(user);
                if (userId != -1)
                {
                    return Ok(new { result = "success", user_id = userId });
                }
                return BadRequest(new { result = "error", msg = "Cannot create user" });
            }
        }

        [HttpGet("exist")]
        public IActionResult CheckUserExist(string username)
        {
            var user = _userService.IsUserExist(username);
            if (user != null) return Ok(new { exist = true, username = user.Username, email = user.Email });
            else return Ok(new { exist = false });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rs = _userService.Delete(id);
            if (rs != -1)
            {
                return Ok(new { delId = rs });
            }
            return NotFound(new { result = "error", msg = "user not found" });
        }

        [HttpGet]
        public IActionResult GetAll(int page)
        {
            var rs = _userService.FindAll(page);
            return Ok(new { users = rs.users, count = rs.users.Count(), total_page = rs.totalPage, total_users = rs.totalUsers });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.Find(id);
            if (user == null)
            {
                return NotFound(new { result = "error", msg = "Object not found" });
            }
            return Ok(user);
        }

        [HttpGet("search")]
        public IActionResult GetByName(string q)
        {
            var user = _userService.Find(q);
            if (user == null)
            {
                return NotFound(new { result = "error", msg = "Object not found" });
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            var rs = _userService.Update(user);
            if (rs) return Ok(new { result = "success", msg = "Update successfully" });
            else return NotFound(new { result = "error", msg = "Object not found" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(new { login = false });
            }
            else
            {
                var loginToken = _userService.Login(loginModel.Username, loginModel.Password);
                if (loginToken != null) return Ok(new { login = true, token = loginToken });
            }
            return BadRequest(new { login = false });

        }

        [HttpGet("verify")]
        public IActionResult SentEmailCode(string email)
        {
            var code = GenerateRandomNo();
            _mailSender.SendEmailAsync(email, "Clinic-Reset Password", $"Your code verify is: <b>{code}</b>. Please don't share this for anyone !");
            return Ok(new { status = true, code = code });

        }

        private int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        [HttpPost("password/update")]
        public IActionResult UpdateUsePassword([FromBody] UpdateUserPassword model)
        {
            var rs = _userService.UpdateUserPassword(model.Username, model.Password);
            if (rs) return Ok(new { status = true });
            else return Ok(new { status = false });
        }
    }
}
