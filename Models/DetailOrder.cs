using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class DetailOrder
    {
        public DetailOrder()
        {
            ReceiptMedicineIdOrderdetails = new HashSet<ReceiptMedicineIdOrderdetail>();
            ReceiptScientificEquipmentIdOrderDetails = new HashSet<ReceiptScientificEquipmentIdOrderDetail>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public double? Discount { get; set; }
        public int? DiscountEventId { get; set; }
        public int? CustomerId { get; set; }

        public virtual User Customer { get; set; }
        public virtual DiscountEvent DiscountEvent { get; set; }
        public virtual ICollection<ReceiptMedicineIdOrderdetail> ReceiptMedicineIdOrderdetails { get; set; }
        public virtual ICollection<ReceiptScientificEquipmentIdOrderDetail> ReceiptScientificEquipmentIdOrderDetails { get; set; }
    }
}
