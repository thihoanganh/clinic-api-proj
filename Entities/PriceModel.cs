using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class PriceModel
    {
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
    }
}
