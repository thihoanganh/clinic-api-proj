using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class DetailOrderModel
    {
        public List<InfoMedicineBuyModel> InfoMedicineBuyModels { get; set; }
        public List<InfoScientificEquipmentBuyModel> InfoScientificEquipmentBuyModels { get; set; }
        public float Discount { get; set; }
        public int? CustomerId { get; set; }
    }
}
