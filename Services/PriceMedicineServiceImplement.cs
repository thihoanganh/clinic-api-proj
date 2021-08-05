using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class PriceMedicineServiceImplement : IPriceService
    {
        private ClinicDbContext db;

        public PriceMedicineServiceImplement(ClinicDbContext _db)
        {
            db = _db;
        }

        public void create(PriceModel priceModel)
        {
            PriceMedicine priceMedicine = new PriceMedicine();
            priceMedicine.Price = priceModel.Price;
            priceMedicine.Date = priceModel.Date;
            priceMedicine.MedicineId = priceModel.ProductId;
            db.PriceMedicines.Add(priceMedicine);
            db.SaveChanges();
        }

        public PriceModel getRecentPriceOfProduct(int productId, DateTime date)
        {
            return db.PriceMedicines.Where(p => p.MedicineId == productId && p.Date < date).Select(p => new PriceModel
            {
                Price = (double)p.Price,
                Date = (DateTime)p.Date,
                ProductId = (int)p.MedicineId
            }).OrderByDescending(p => p.Date).FirstOrDefault();

        }
    }
}
