using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers.Admin
{
    [Route("api/receipmedicine")]
    public class ReceipMedicineController : Controller
    {
        private IReceiptMedicineService receiptMedicineService;

        public ReceipMedicineController(IReceiptMedicineService _receiptMedicineService)
        {
            receiptMedicineService = _receiptMedicineService;
           
        }

        [HttpGet("getreceipofmedicine/{id}")]
        [Produces("application/json")]
        public IActionResult GetReceipOfMedicine(int id)
        {
            try
            {
                var receiptMedicines = receiptMedicineService.getReceipOfMedicine(id);
                return Ok(receiptMedicines);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("createreceiptmedicine")]
        [Produces("application/json")]
        public IActionResult CreateReceiptMedicine([FromBody] ReceiptMedicineModel receiptMedicineModel)
        {
            try
            {
                receiptMedicineService.createReceipOfMedicine(receiptMedicineModel);
                return Ok(receiptMedicineModel);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}