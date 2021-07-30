using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class DiscountEvent
    {
        public DiscountEvent()
        {
            DetailOrders = new HashSet<DetailOrder>();
        }

        public int Id { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double? MoneyCondition { get; set; }

        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
