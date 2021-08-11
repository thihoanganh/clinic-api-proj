using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Entities
{
    public class MedicineModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Illustration { get; set; }
        public string Ingredient { get; set; }
        public string PresentationFormat { get; set; }
        public string Point { get; set; }
        public string Using { get; set; }
        public string SpecialWarning { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? Expiry { get; set; }
        public bool? Status { get; set; }
        public string? Origin { get; set; }
        public string? TypeOf { get; set; }
        public string? Brand { get; set; }
    }
}
