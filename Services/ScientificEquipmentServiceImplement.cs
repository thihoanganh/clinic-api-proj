using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services
{
    public class ScientificEquipmentServiceImplement : IScientificEquipmentService
    {
        private ClinicDbContext db;

        public ScientificEquipmentServiceImplement(ClinicDbContext _db)
        {
            db = _db;
        }
        public List<ScientificeqipmentModel> getAll()
        {
            return db.ScientificEquipments.Select(m => new ScientificeqipmentModel
            {
                Id = m.Id,
                Name = m.Name,
                Illustration = m.Illustration,
                InventedYear = m.InventedYear,
                Description = m.Description,
                Status = (bool)m.Status,
                Brand = m.Brand.Brand1,
                Origin = m.Origin.Origin1,
                MachineCategory = m.MachineCategory.Name
            }).ToList();
        }

        public List<ScientificeqipmentModel> searchByName(string name)
        {
            return db.ScientificEquipments.Where(m => m.Name.Contains(name)).Select(m => new ScientificeqipmentModel
            {
                Id = m.Id,
                Name = m.Name,
                Illustration = m.Illustration,
                InventedYear = m.InventedYear,
                Description = m.Description,
                Status = (bool)m.Status,
                Brand = m.Brand.Brand1,
                Origin = m.Origin.Origin1,
                MachineCategory = m.MachineCategory.Name
            }).ToList();
        }
    }
}
