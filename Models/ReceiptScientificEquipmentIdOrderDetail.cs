using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class ReceiptScientificEquipmentIdOrderDetail
    {
        public int ReceiptScientificEquipmentId { get; set; }
        public int OrderDetailId { get; set; }
        public int? Amount { get; set; }

        public virtual DetailOrder OrderDetail { get; set; }
        public virtual ReceiptScientificEquipment ReceiptScientificEquipment { get; set; }
    }
}
