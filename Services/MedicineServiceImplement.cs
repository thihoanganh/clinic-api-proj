using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class MedicineServiceImplement : IMedicineService
    {
        private ClinicDbContext db;

        public MedicineServiceImplement(ClinicDbContext _db)
        {
            db = _db;
        }
        public List<MedicineModel> getAll()
        {
            return db.Medicines.Select(m => new MedicineModel
            {
                Id = m.Id,
                Name = m.Name,
                Illustration = m.Illustration,
                Ingredient = m.Ingredient,
                PresentationFormat = m.PresentationFormat,
                Point = m.Point,
                Using = m.Using,
                SpecialWarning = m.SpecialWarning,
                DateOfManufacture = m.DateOfManufacture,
                Expiry = m.Expiry,
                Status = m.Status,
                Origin = m.Origin.Origin1,
                TypeOf = m.TypeOf.Category,
                Brand = m.Brand.Brand1


            }).ToList();
        }

        public List<MedicineModel> searchByName(string name)
        {
            return db.Medicines.Where(m => m.Name.Contains(name)).Select(m => new MedicineModel
            {
                Id = m.Id,
                Name = m.Name,
                Illustration = m.Illustration,
                Ingredient = m.Ingredient,
                PresentationFormat = m.PresentationFormat,
                Point = m.Point,
                Using = m.Using,
                SpecialWarning = m.SpecialWarning,
                DateOfManufacture = m.DateOfManufacture,
                Expiry = m.Expiry,
                Status = m.Status,
                Origin = m.Origin.Origin1,
                TypeOf = m.TypeOf.Category,
                Brand = m.Brand.Brand1


            }).ToList();
        }
    }
}
