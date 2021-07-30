using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class ScientificEquipment
    {
        public ScientificEquipment()
        {
            ReceiptScientificEquipments = new HashSet<ReceiptScientificEquipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Illustration { get; set; }
        public int? InventedYear { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
        public int? BrandId { get; set; }
        public int? OriginId { get; set; }
        public int? MachineCategoryId { get; set; }
        public int? Priceid { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual MachineCategory MachineCategory { get; set; }
        public virtual Origin Origin { get; set; }
        public virtual Price Price { get; set; }
        public virtual ICollection<ReceiptScientificEquipment> ReceiptScientificEquipments { get; set; }
    }
}
