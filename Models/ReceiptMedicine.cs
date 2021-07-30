using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class ReceiptMedicine
    {
        public ReceiptMedicine()
        {
            ReceiptMedicineIdOrderdetails = new HashSet<ReceiptMedicineIdOrderdetail>();
        }

        public int Id { get; set; }
        public int? Amount { get; set; }
        public double? PriceBuy { get; set; }
        public DateTime? Date { get; set; }
        public int? MedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual ICollection<ReceiptMedicineIdOrderdetail> ReceiptMedicineIdOrderdetails { get; set; }
    }
}
