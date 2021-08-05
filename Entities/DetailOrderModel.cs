using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class DetailOrderModel
    {
        public List<InfoMedicineBuyModel> InfoMedicineBuyModels;
        public List<InfoScientificEquipmentBuyModel> InfoScientificEquipmentBuyModels;
        public float Discount;
        public int? CustomerId;
    }
}
