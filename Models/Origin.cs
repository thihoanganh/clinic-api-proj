using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Origin
    {
        public Origin()
        {
            Medicines = new HashSet<Medicine>();
            ScientificEquipments = new HashSet<ScientificEquipment>();
        }

        public int Id { get; set; }
        public string Origin1 { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<ScientificEquipment> ScientificEquipments { get; set; }
    }
}
