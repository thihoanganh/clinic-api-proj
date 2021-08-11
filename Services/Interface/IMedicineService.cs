
using Clinic_Web_Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IMedicineService
    {
        public List<MedicineModel> getAll();
        public List<MedicineModel> searchByName(string name);
    }
}
