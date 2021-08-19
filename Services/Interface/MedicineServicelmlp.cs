using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface MedicineServicelmlp
    {
        List<Medicine> Medicinelist();
		Medicine FindDetail(int id);

		public Medicine FindById(int id);
		

		//-------------------------------------------------------------------------
		public List<Medicine> FindAll();
		public Medicine Find(int id);
		// type medice +create +delete
		public List<TypeOfMedicine> TypeMedicine();
		public TypeOfMedicine AddTypeMedicine(TypeOfMedicine typeOfMedicine);
		public void DeleteTypeMedicine(int id);
		//add+update medicine 
		public Medicine AddMedicine(Medicine medicine);
		public Medicine UpdateMedicine(Medicine medicine);
		public void DeleteMedicine(int id);
		//search
		public List<Medicine> Search(string keyword);
		public List<Medicine> SearchType(int madicineType);
		//-------------------------------------------------------------------------
	}
}
