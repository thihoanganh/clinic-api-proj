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
        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public IActionResult GetAll()
        {
            var rs = _userService.FindAll();
            return Ok(new { result = rs, count = rs.Count() });
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
    }
}
