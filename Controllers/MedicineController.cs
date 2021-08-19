using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/Medicine")]
    public class MedicineController : Controller
    {
        private IMedicineService medicineService;

        public MedicineController(IMedicineService _medicineService)
        {
            medicineService = _medicineService;

        }


        [HttpGet("getall")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(medicineService.getAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("searchbyname/{name}")]
        [Produces("application/json")]
        public IActionResult GetAll(string name)
        {
            try
            {
                return Ok(medicineService.searchByName(name));
            }
            catch
            {
                return BadRequest();
            }
        }
       
    }
}
