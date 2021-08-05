using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class PriceMedicine
    {
        public int Id { get; set; }
        public double? Price { get; set; }
        public DateTime? Date { get; set; }
        public int? MedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }
    }
}
