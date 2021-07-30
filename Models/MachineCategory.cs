using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class MachineCategory
    {
        public MachineCategory()
        {
            ScientificEquipments = new HashSet<ScientificEquipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ScientificEquipment> ScientificEquipments { get; set; }
    }
}
