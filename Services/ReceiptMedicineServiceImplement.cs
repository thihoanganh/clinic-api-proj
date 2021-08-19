using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class ReceiptMedicineServiceImplement : IReceiptMedicineService
    {
        private ClinicDbContext db;

        public ReceiptMedicineServiceImplement(ClinicDbContext _db)
        {
            db = _db;
        }

        public void createReceipOfMedicine(ReceiptMedicineModel receiptMedicineModel)
        {
            ReceiptMedicine receiptMedicine = new ReceiptMedicine();
            receiptMedicine.Amount = receiptMedicineModel.Amount;
            receiptMedicine.Date = receiptMedicineModel.Date;
            receiptMedicine.PriceBuy = receiptMedicineModel.PriceBuy;
            receiptMedicine.MedicineId = receiptMedicineModel.MedicineId;
            receiptMedicine.Expiry = receiptMedicineModel.ExpiryDate;

            db.Add(receiptMedicine);
            db.SaveChanges();


        }

        public List<ReceiptMedicineModel> getReceipOfMedicine(int id)
        {
            return db.ReceiptMedicines.Where(r => r.MedicineId == id).Select(r => new ReceiptMedicineModel
            {
                Id = r.Id,
                Amount = (int)r.Amount,
                PriceBuy = (double)r.PriceBuy,
                Date = (DateTime)r.Date,
                NameOfMedicine = r.Medicine.Name,
                ExpiryDate = (DateTime)r.Expiry,
                MedicineId = (int)r.MedicineId
            }).ToList();
        }

     
    }
}
