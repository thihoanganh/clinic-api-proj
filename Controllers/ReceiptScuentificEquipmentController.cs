using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Services.Interface;

namespace Clinic_Web_Api.Controllers.Admin
{
    [Route("api/receiptscuentificequipment")]
    public class ReceiptScuentificEquipmentController : Controller
    {
        private IReceiptScientificEquipmentService receiptScientificEquipmentService;

        public ReceiptScuentificEquipmentController(IReceiptScientificEquipmentService _receiptScientificEquipmentService)
        {
            receiptScientificEquipmentService = _receiptScientificEquipmentService;

        }
        [HttpPost("create")]
        [Produces("application/json")]
        public IActionResult Create([FromBody]ReceiptScientificEquipmentModel  receiptScientificEquipmentModel)
        {
            try
            {
                receiptScientificEquipmentService.create(receiptScientificEquipmentModel);
                return Ok(receiptScientificEquipmentModel);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
