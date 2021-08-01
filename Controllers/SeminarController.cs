using Clinic_Web_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using Clinic_Web_Api.Services.Interface;
using Clinic_Web_Api.Entities;
using Newtonsoft.Json;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeminarController : ControllerBase
    {
        private readonly IWebHostEnvironment _evn;
        private readonly ISeminarService _smnService;


        public SeminarController(IWebHostEnvironment evn, ISeminarService smnService)
        {
            _evn = evn;
            _smnService = smnService;
        }
        [HttpPost]
        public IActionResult Create([FromForm] IFormFile poster, [FromForm] string seminar)
        {
            var smn = JsonConvert.DeserializeObject<Seminar>(seminar);
            if (smn != null && poster != null)
            {
                var fileHelper = new FileHelper(_evn);
                var uploadFile = fileHelper.UploadFile(poster, "seminar/image");
                if (uploadFile != null)
                {
                    // upload poster success
                    smn.Poster = uploadFile;
                    var rs = _smnService.Create(smn);
                    if (rs != -1)
                    {
                        return Ok(new { result = "success", id = rs });
                    }
                }
            }
            return BadRequest(new { result = "error", msg = "can not create" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rs = _smnService.Delete(id);
            if (rs != -1)
            {
                return Ok(new { delId = rs });
            }
            return NotFound(new { msg = "Staff not found" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rs = _smnService.FindAll();
            return Ok(new { result = rs, count = rs.Count() });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var staff = _smnService.Find(id);
            if (staff == null)
            {
                return NotFound(new { msg = "Object not found" });
            }
            return Ok(staff);
        }

        [HttpGet("search")]
        public IActionResult GetByName(string q)
        {
            var staff = _smnService.Find(q);
            if (staff == null)
            {
                return NotFound(new { result = "error", msg = "Object not found" });
            }
            return Ok(staff);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Seminar smn)
        {
            var rs = _smnService.UpdateSeminar(smn);
            if (rs) return Ok(new { result = "success", msg = "Update successfully" });
            else return NotFound(new { result = "error", msg = "Object not found" });
        }

        [HttpPost("register")]
        public IActionResult RegisterSeminar([FromBody] SeminarRegistation sr)
        {
            var rs = _smnService.Register(sr);
            if (rs == -1) return BadRequest(new { result = "error", msg = "can not regist seminar for this user" });
            return Ok(new { result = "success", id = rs });
        }

        [HttpGet("{id}/registers")]
        public IActionResult FindRegistered(int id)
        {
            var rs = _smnService.FindAllRegisterOfSeminar(id);
            return Ok(new { result = rs, count = rs.Count() });
        }




    }
}
