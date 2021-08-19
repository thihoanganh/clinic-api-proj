using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class ReceiptMedicineModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double PriceBuy { get; set; }
        public DateTime Date { get; set; }
        public String NameOfMedicine { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MedicineId { get; set; }
    }
}
