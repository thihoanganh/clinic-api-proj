using Clinic_Web_Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IDetailOrderService
    {
        public void createDetailOrder(DetailOrderModel detailOrderModel);
        public DetailOrderProfit getProfit(int detaiorderId);

        public List<StatisticalMedicine> getProfitOfMedicine(int medicineId, DateTime fromDate, DateTime toDate);
        public List<StatisticalScientificEquipment> getProfitOfScientificEquipment(int scientificEquipmentId, DateTime fromDate, DateTime toDate);
    }
}
