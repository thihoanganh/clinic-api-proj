using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS10_WebApplication_MVC_DB.Helpers;

namespace Clinic_Web_Api.Services
{
	public class ScientificService : ScientificServicelmlp
	{
		private readonly ClinicDbContext _db;
		public IConfiguration _config { get; }
		private IWebHostEnvironment webHostEnvironment;

		public ScientificService(ClinicDbContext db, IConfiguration config, IWebHostEnvironment _webHostEnvironment)
		{
			_db = db;
			_config = config;
			webHostEnvironment = _webHostEnvironment;
		}
		//--------------------------------------------------------------
		public ScientificEquipment FindDetail(int id)
		{
			var scientific = _db.ScientificEquipments.Find(id);
			if (scientific != null)
			{
				_db.Entry(scientific).Reference(s => s.Brand).Load();
				_db.Entry(scientific).Reference(s => s.Origin).Load();
				_db.Entry(scientific).Reference(s => s.Price).Load();
				_db.Entry(scientific).Reference(s => s.MachineCategory).Load();
				return scientific;
			}

			return null;
		}

		public List<ScientificEquipment> Scientificlist()
		{
			return _db.ScientificEquipments.Include(s => s.Brand).Include(s => s.Origin).Include(s => s.Price).Include(s => s.MachineCategory).AsNoTracking().ToList();
		}


		public ScientificEquipment FindById(int id)
		{
			return _db.ScientificEquipments.AsNoTracking().SingleOrDefault(p => p.Id == id);
		}

		


			//---------------------------------------------------------------------------------------------------------------------------------------------------------------
			//findall
		public List<ScientificEquipment> FindAll()
		{
			return _db.ScientificEquipments.ToList();
		}
		//find
		public ScientificEquipment Find(int id)
		{
			return _db.ScientificEquipments.Where(p => p.Id == id).Select(s => new ScientificEquipment
			{
				Id = s.Id,
				Name = s.Name,
				Illustration = s.Illustration,
				InventedYear = s.InventedYear,
				Description = s.Description,
				Status = s.Status,
				Quantity = s.Quantity,
				BrandId = s.BrandId,
				OriginId = s.OriginId,
				MachineCategoryId = s.MachineCategoryId,
				Priceid = s.Priceid,


			}).FirstOrDefault();
		}


		//combobox

		public List<Brand> Brand()
		{
			return _db.Brands.Select(b => new Brand
			{
				Id = b.Id,
				Brand1 = b.Brand1,

			}).ToList();
		}
		public List<MachineCategory> MachineCategory()
		{
			return _db.MachineCategories.Select(m => new MachineCategory
			{
				Id = m.Id,
				Name = m.Name

			}).ToList();
		}

		public List<Origin> Orgin()
		{
			return _db.Origins.Select(m => new Origin
			{
				Id = m.Id,
				Origin1 = m.Origin1

			}).ToList();
		}

		public List<Price> Price()
		{
			return _db.Prices.Select(m => new Price
			{
				Id = m.Id,
				Price1 = m.Price1,
				Date = m.Date

			}).ToList();
		}

		public List<TypeOfMedicine> TypeOfMedicine()
		{
			return _db.TypeOfMedicines.Select(m => new TypeOfMedicine
			{
				Id = m.Id,
				Category = m.Category

			}).ToList();
		}

		//search
		public List<ScientificEquipment> Search(string keyword)
		{
			return FindAll().Where(s => s.Name.Contains(keyword)).ToList();
		}

		public List<ScientificEquipment> SearchType(int machineType)
		{
			return FindAll().Where(a => a.MachineCategoryId==machineType).ToList();
		}
		//delete
		public void Delete(int id)
		{
			_db.ScientificEquipments.Remove(_db.ScientificEquipments.Find(id));
			_db.SaveChanges();
			
		}
		//create thiet bi
		public ScientificEquipment Create(ScientificEquipment scientific)
		{
			_db.ScientificEquipments.Add(scientific);
			_db.SaveChanges();
			return new ScientificEquipment
			{
				Id=scientific.Id,
				Name=scientific.Name,
				Illustration=scientific.Illustration,
				InventedYear=scientific.InventedYear,
				Description=scientific.Description,
				Status=scientific.Status,
				Quantity=scientific.Quantity,
				BrandId=scientific.BrandId,
				OriginId=scientific.OriginId,
			    MachineCategoryId=scientific.MachineCategoryId,
				Priceid=scientific.Priceid,

			};
		}

		
		//update thiet bi
		public ScientificEquipment Update(ScientificEquipment scientific)
		{
			_db.Entry(scientific).State = EntityState.Modified;
			_db.SaveChanges();

			return new ScientificEquipment
			{
				Id = scientific.Id,
				Name = scientific.Name,
				Illustration = scientific.Illustration,
				InventedYear = scientific.InventedYear,
				Description = scientific.Description,
				Status = scientific.Status,
				Quantity = scientific.Quantity,
				BrandId = scientific.BrandId,
				OriginId = scientific.OriginId,
				MachineCategoryId = scientific.MachineCategoryId,
				Priceid = scientific.Priceid

			};
		}
		//add+delete barand
		public Brand AddBrand(Brand brand)
		{
			_db.Brands.Add(brand);
			_db.SaveChanges();
			return new Brand
			{
				Brand1 = brand.Brand1
			};
		}

		public void DeleteBrand(int id)
		{
			_db.Brands.Remove(_db.Brands.Find(id));
			_db.SaveChanges();
		
		}
		//add+delete origin
		public Origin AddOrigin(Origin origin)
		{
			_db.Origins.Add(origin);
			_db.SaveChanges();
			return new Origin
			{
				Origin1 = origin.Origin1
			};
		}

		public void DeleteOrigin(int id)
		{
			_db.Origins.Remove(_db.Origins.Find(id));
			_db.SaveChanges();
			Debug.WriteLine("id:" + id);
		}
		//add+delete machineCategory
		public MachineCategory AddMachineCategory(MachineCategory machineCategory)
		{
			_db.MachineCategories.Add(machineCategory);
			_db.SaveChanges();
			return new MachineCategory
			{
				Name = machineCategory.Name
			};
		}

		public void DeleteMachineCategory(int id)
		{
			_db.MachineCategories.Remove(_db.MachineCategories.Find(id));
			_db.SaveChanges();
			Debug.WriteLine("id:" + id);
		}
		//add+delete price
		public Price AddPrice(Price price)
		{
			_db.Prices.Add(price);
			_db.SaveChanges();
			return new Price
			{
				Price1 = price.Price1,
				Date = price.Date
			};
		}

		public void DeletePrice(int id)
		{
			_db.Prices.Remove(_db.Prices.Find(id));
			_db.SaveChanges();
			Debug.WriteLine("id:" + id);
		}

		



		//----------------------------------------------------------------------------------------------------------
	}
}
