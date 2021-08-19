using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Services;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers.Admin
{
    [Route("api/price")]
    public class PriceController : Controller
    {
     
        private IEnumerable<IPriceService> priceServices;
        private dynamic priceMedicineService;
        private dynamic priceScientificService;
        public PriceController(IEnumerable<IPriceService> _priceService)
        {
            priceServices = _priceService;
            priceMedicineService = priceServices.SingleOrDefault(s => s.GetType() == typeof(PriceMedicineServiceImplement));
            priceScientificService = priceServices.SingleOrDefault(s => s.GetType() == typeof(PriceScientificEquipServiceImplement));
        }

        [HttpPost("createpricemedicine")]
        [Produces("application/json")]
        public IActionResult createPriceMedicine([FromBody] PriceModel  priceModel)
        {
            try
            {
                priceMedicineService.create(priceModel);
                return Ok(priceModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getrecentpricemedicine/{id}/{date}")]
        [Produces("application/json")]
        public IActionResult GetRecentPriceMedicine(int id,DateTime date)
        {
            try
            {
                return Ok(priceMedicineService.getRecentPriceOfProduct(id,date));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("createpricescientificEquipment")]
        [Produces("application/json")]
        public IActionResult createPriceScientificEquipment([FromBody] PriceModel priceModel)
        {
            try
            {
                priceScientificService.create(priceModel);
                return Ok(priceModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getrecentpricescientificEquipment/{id}/{date}")]
        [Produces("application/json")]
        public IActionResult GetRecentPriceScientificEquipment(int id, DateTime date)
        {
            try
            {
                return Ok(priceScientificService.getRecentPriceOfProduct(id, date));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
