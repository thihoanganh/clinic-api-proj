using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class ReceiptScientificEquipmentModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double PriceBuy { get; set; }
        public DateTime Date { get; set; }
        public int ScientificEquipmentId { get; set; }
    }
}
