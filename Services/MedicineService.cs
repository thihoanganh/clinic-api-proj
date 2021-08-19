using Clinic_Web_Api.Helpers;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace Clinic_Web_Api.Services
{
    public class MedicineService :MedicineServicelmlp
    {
        private readonly ClinicDbContext _db;
        public IConfiguration _config { get; }

        public MedicineService(ClinicDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

		public List<Medicine> Medicinelist()
		{
            return _db.Medicines.Include(s => s.Brand).Include(s => s.Origin).Include(s => s.Price).Include(s => s.TypeOf).AsNoTracking().ToList();
        }

		public Medicine FindDetail(int id)
		{
			var medicine = _db.Medicines.Find(id);
			if (medicine != null)
			{
				_db.Entry(medicine).Reference(s => s.Brand).Load();
				_db.Entry(medicine).Reference(s => s.Origin).Load();
				_db.Entry(medicine).Reference(s => s.Price).Load();
				_db.Entry(medicine).Reference(s => s.TypeOf).Load();
				return medicine;
			}

			return null;
		}

		public Medicine FindById(int id)
		{
			return _db.Medicines.AsNoTracking().SingleOrDefault(p => p.Id == id);
		}
		

		//------------------------------------------------------------------------------------------------------------------
		public List<Medicine> FindAll()
		{
            return _db.Medicines.ToList();
			
		}
		//find
		public Medicine Find(int id)
		{
			return _db.Medicines.Where(p => p.Id == id).Select(m => new Medicine
			{
				Id = m.Id,
				Name = m.Name,
				Illustration = m.Illustration,
				Ingredient = m.Ingredient,
				PresentationFormat = m.PresentationFormat,
				Point = m.Point,
				Using = m.Using,
				SpecialWarning = m.SpecialWarning,
				Quantity = m.Quantity,
				DateOfManufacture = m.DateOfManufacture,
				Expiry = m.Expiry,
				Status = m.Status,
				OriginId = m.OriginId,
				TypeOfId = m.TypeOfId,
				BrandId = m.BrandId,
				PriceId = m.PriceId,


			}).FirstOrDefault();
		}
		//list typeb medicine
		public List<TypeOfMedicine> TypeMedicine()
		{
			return _db.TypeOfMedicines.Select(t => new TypeOfMedicine
			{
				Id = t.Id,
				Category = t.Category
			}).ToList();
		}
		//add type medicine
		public TypeOfMedicine AddTypeMedicine(TypeOfMedicine typeOfMedicine)
		{
			_db.TypeOfMedicines.Add(typeOfMedicine);
			_db.SaveChanges();
			return new TypeOfMedicine
			{
				Category = typeOfMedicine.Category
			};
		}
		//delete type medicine
		public void DeleteTypeMedicine(int id)
		{
			_db.TypeOfMedicines.Remove(_db.TypeOfMedicines.Find(id));
			_db.SaveChanges();
			
		}
		//ADD+update+delete medicine
		public Medicine AddMedicine(Medicine medicine)
		{
			_db.Medicines.Add(medicine);
			_db.SaveChanges();
			return new Medicine
			{
				Id = medicine.Id,
				Name = medicine.Name,
				Illustration = medicine.Illustration,
				Ingredient = medicine.Ingredient,
				PresentationFormat = medicine.PresentationFormat,
				Point = medicine.Point,
				Using = medicine.Using,
				SpecialWarning = medicine.SpecialWarning,
				Quantity = medicine.Quantity,
				DateOfManufacture = medicine.DateOfManufacture,
				Expiry = medicine.Expiry,
				Status = medicine.Status,
				OriginId = medicine.OriginId,
				TypeOfId = medicine.TypeOfId,
				BrandId = medicine.BrandId,
				PriceId = medicine.PriceId,
			};
		}

		public Medicine UpdateMedicine(Medicine medicine)
		{
			_db.Entry(medicine).State = EntityState.Modified;
			_db.SaveChanges();

			return new Medicine
			{
				Id = medicine.Id,
				Name = medicine.Name,
				Illustration = medicine.Illustration,
				Ingredient = medicine.Ingredient,
				PresentationFormat = medicine.PresentationFormat,
				Point = medicine.Point,
				Using = medicine.Using,
				SpecialWarning = medicine.SpecialWarning,
				Quantity = medicine.Quantity,
				DateOfManufacture = medicine.DateOfManufacture,
				Expiry = medicine.Expiry,
				Status = medicine.Status,
				OriginId = medicine.OriginId,
				TypeOfId = medicine.TypeOfId,
				BrandId = medicine.BrandId,
				PriceId = medicine.PriceId,

			};
		}

		public void DeleteMedicine(int id)
		{
			_db.Medicines.Remove(_db.Medicines.Find(id));
			_db.SaveChanges();
			
		}
		//search 
		public List<Medicine> Search(string keyword)
		{
			return FindAll().Where(p => p.Name.Contains(keyword)).ToList();
		}

		public List<Medicine> SearchType(int madicineType)
		{
			return FindAll().Where(a => a.TypeOfId == madicineType).ToList();
		}

		
		//-----------------------------------------------------------------------------------------------
	}
}

