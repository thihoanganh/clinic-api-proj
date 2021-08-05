using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Entities;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IReceiptScientificEquipmentService
    {
        public void create(ReceiptScientificEquipmentModel receiptScientificEquipmentModel);
    }
}
