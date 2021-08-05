using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{

    public class ReceiptScientificEquipmentServiceImplement : IReceiptScientificEquipmentService
    {

        private ClinicDbContext db;

        public ReceiptScientificEquipmentServiceImplement(ClinicDbContext _db)
        {
            db = _db;
        }

        public void create(ReceiptScientificEquipmentModel receiptScientificEquipmentModel)
        {
            ReceiptScientificEquipment receiptScientificEquipment = new ReceiptScientificEquipment();
            receiptScientificEquipment.Amount = receiptScientificEquipmentModel.Amount;
            receiptScientificEquipment.Date = receiptScientificEquipmentModel.Date;
            receiptScientificEquipment.PriceBuy = receiptScientificEquipmentModel.PriceBuy;
            receiptScientificEquipment.ScientificEquipmentId = receiptScientificEquipmentModel.ScientificEquipmentId;

            db.Add(receiptScientificEquipment);
            db.SaveChanges();
        }
    }
}
