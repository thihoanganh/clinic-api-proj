using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class StatisticalScientificEquipment
    {
        public String NameScientificEquipment { get; set; }
        public Double PriceBuy { get; set; }
        public Double PriceSell { get; set; }
        public int Quantity { get; set; }
    }
}
