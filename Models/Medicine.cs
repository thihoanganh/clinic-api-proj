using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            ReceiptMedicines = new HashSet<ReceiptMedicine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Illustration { get; set; }
        public string Ingredient { get; set; }
        public string PresentationFormat { get; set; }
        public string Point { get; set; }
        public string Using { get; set; }
        public string SpecialWarning { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? Expiry { get; set; }
        public bool? Status { get; set; }
        public int? OriginId { get; set; }
        public int? TypeOfId { get; set; }
        public int? BrandId { get; set; }


        public virtual Brand Brand { get; set; }
        public virtual Origin Origin { get; set; }
        public virtual Price Price { get; set; }
        public virtual TypeOfMedicine TypeOf { get; set; }
        public virtual ICollection<ReceiptMedicine> ReceiptMedicines { get; set; }
        public virtual ICollection<PriceMedicine> PriceMedicines { get; set; }
    }
}
