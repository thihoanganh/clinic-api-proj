using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class PriceScientificEquipment
    {
        public int Id { get; set; }
        public double? Price { get; set; }
        public DateTime? Date { get; set; }
        public int? ScientificEquipmentId { get; set; }

        public virtual ScientificEquipment ScientificEquipment { get; set; }
    }
}
