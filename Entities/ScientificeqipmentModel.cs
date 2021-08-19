using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class ScientificeqipmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Illustration { get; set; }
        public int? InventedYear { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Brand { get; set; }
        public string Origin { get; set; }
        public string MachineCategory { get; set; }
    }
}
