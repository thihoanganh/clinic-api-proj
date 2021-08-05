using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class PriceScientificEquipServiceImplement : IPriceService
    {

        private ClinicDbContext db;

        public PriceScientificEquipServiceImplement(ClinicDbContext _db)
        {
            db = _db;
        }
        public void create(PriceModel priceModel)
        {
            PriceScientificEquipment priceScientificEquipment = new PriceScientificEquipment();
            priceScientificEquipment.Price = priceModel.Price;
            priceScientificEquipment.Date = priceModel.Date;
            priceScientificEquipment.ScientificEquipmentId = priceModel.ProductId;
            db.PriceScientificEquipments.Add(priceScientificEquipment);
            db.SaveChanges();
        }

        public PriceModel getRecentPriceOfProduct(int productId, DateTime date)
        {
            return db.PriceScientificEquipments.Where(p => p.ScientificEquipmentId == productId && p.Date < date).Select(p => new PriceModel
            {
                Price = (double)p.Price,
                Date = (DateTime)p.Date,
                ProductId = (int)p.ScientificEquipmentId
            }).OrderByDescending(p => p.Date).FirstOrDefault();
        }
    }
}
