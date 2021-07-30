using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Medicines = new HashSet<Medicine>();
            ScientificEquipments = new HashSet<ScientificEquipment>();
        }

        public int Id { get; set; }
        public string Brand1 { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<ScientificEquipment> ScientificEquipments { get; set; }
    }
}
