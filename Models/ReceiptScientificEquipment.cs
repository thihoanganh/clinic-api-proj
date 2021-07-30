using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class ReceiptScientificEquipment
    {
        public ReceiptScientificEquipment()
        {
            ReceiptScientificEquipmentIdOrderDetails = new HashSet<ReceiptScientificEquipmentIdOrderDetail>();
        }

        public int Id { get; set; }
        public int? Amount { get; set; }
        public double? PriceBuy { get; set; }
        public DateTime? Date { get; set; }
        public int? ScientificEquipmentId { get; set; }

        public virtual ScientificEquipment ScientificEquipment { get; set; }
        public virtual ICollection<ReceiptScientificEquipmentIdOrderDetail> ReceiptScientificEquipmentIdOrderDetails { get; set; }
    }
}
