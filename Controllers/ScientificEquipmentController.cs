using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers
{
    [Route("api/scientificequipment")]
    public class ScientificEquipmentController : Controller
    {
        private IScientificEquipmentService scientificEquipmentService;

        public ScientificEquipmentController(IScientificEquipmentService _scientificEquipmentService)
        {
            scientificEquipmentService = _scientificEquipmentService;

        }
        [HttpGet("getall")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(scientificEquipmentService.getAll());
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
                return Ok(scientificEquipmentService.searchByName(name));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
