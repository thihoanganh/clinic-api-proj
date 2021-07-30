using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class ReceiptMedicineIdOrderdetail
    {
        public int ReceiptMedicineId { get; set; }
        public int OrderdetailId { get; set; }
        public int? Amount { get; set; }

        public virtual DetailOrder Orderdetail { get; set; }
        public virtual ReceiptMedicine ReceiptMedicine { get; set; }
    }
}
