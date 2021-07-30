using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Price
    {
        public Price()
        {
            Medicines = new HashSet<Medicine>();
            ScientificEquipments = new HashSet<ScientificEquipment>();
        }

        public int Id { get; set; }
        public double? Price1 { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<ScientificEquipment> ScientificEquipments { get; set; }
    }
}
