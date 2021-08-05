using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers.Admin
{
    [Route("api/detailorder")]
    public class DetailOrderController : Controller
    {
        private IDetailOrderService detailOrderService;

        public DetailOrderController(IDetailOrderService _detailOrderService)
        {
            detailOrderService = _detailOrderService;
        }

        [HttpGet("showdetailorder")]
        [Produces("application/json")]
        public IActionResult ShowDetailOrder()
        {
            try
            {
                var detailOrderModel = new DetailOrderModel
                {
                    CustomerId = 1,
                    Discount = 0,
                    InfoMedicineBuyModels = new List<InfoMedicineBuyModel>
                    {
                        new InfoMedicineBuyModel
                        {
                            MedicineId = 1,
                            Amount = 10
                        },
                        new InfoMedicineBuyModel
                        {
                            MedicineId = 2,
                            Amount = 15
                        }
                    },
                    InfoScientificEquipmentBuyModels = new List<InfoScientificEquipmentBuyModel>
                    {
                        new InfoScientificEquipmentBuyModel
                        {
                            ScientificEquipmentId = 1,
                            Amount = 1
                        },
                        new InfoScientificEquipmentBuyModel
                        {
                            ScientificEquipmentId = 1,
                            Amount = 2
                        }
                    }

                };
                return Ok(detailOrderModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("createdetailorder")]
        [Produces("application/json")]
        public IActionResult CreateDetailOrder([FromBody] DetailOrderModel detailOrderModel)
        {
            try
            {
                detailOrderService.createDetailOrder(detailOrderModel);
                return Ok(detailOrderModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("orderprofit/{idorder}")]
        [Produces("application/json")]
        public IActionResult OrderProfit(int idorder)
        {
            try
            {

                return Ok(detailOrderService.getProfit(idorder));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
