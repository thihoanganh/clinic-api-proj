using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Clinic_Web_Api.Entities;
namespace Clinic_Web_Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpPost]
        public IActionResult CreateStaff([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest(new { msg = "Staff is empty" });
            }
            else
            {
                var staffId = _staffService.CreateStaff(staff);
                if (staffId != -1)
                {
                    return Ok(new { staff_id = staffId });
                }
                return BadRequest(new { msg = "Cannot create staff." });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rs = _staffService.Delete(id);
            if (rs != -1)
            {
                return Ok(new { delId = rs });
            }
            return NotFound(new { msg = "Staff not found" });
        }

        [HttpGet]
        public IActionResult GetAll(int page)
        {
            var rs = _staffService.FindAll(page);
            return Ok(new { staffs = rs.staffs, count = rs.staffs.Count(), total_page = rs.totalPage, total_staffs = rs.totalStaffs });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var staff = _staffService.Find(id);
            if (staff == null)
            {
                return NotFound(new { msg = "Object not found" });
            }
            return Ok(staff);
        }

        [HttpGet("search")]
        public IActionResult GetByName(string q)
        {
            var staff = _staffService.Find(q);
            if (staff == null)
            {
                return NotFound(new { result = "error", msg = "Object not found" });
            }
            return Ok(staff);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Staff staff)
        {
            var rs = _staffService.Update(staff);
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
                var loginToken = _staffService.Login(loginModel.Username, loginModel.Password);
                if (loginToken != null) return Ok(new { login = true, token = loginToken });
            }
            return BadRequest(new { login = false });

        }

        [HttpGet("exist")]
        public IActionResult CheckStaffExist(string username)
        {
            var staff = _staffService.IsStaffExist(username);
            if (staff != null) return Ok(new { exist = true, username = staff.Username, email = staff.Email });
            else return Ok(new { exist = false });

        }

        [HttpGet("positions")]
        public IActionResult GetAllPositions()
        {
            var rs = _staffService.FindAllPosition().Select(p => new { id = p.Id, name = p.Name, salary = p.Salary, allowance = p.Allowance });
            return Ok(new { positions = rs });
        }
    }
}
