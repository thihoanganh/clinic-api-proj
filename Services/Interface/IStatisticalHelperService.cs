using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IStatisticalHelperService
    {
        public double calculatePriceBuyOfExportScientific(List<ReceiptScientificEquipmentIdOrderDetail> receiptScientificsOrders);
        public double calculatePriceSellOfExportScientific(List<ReceiptScientificEquipmentIdOrderDetail> receiptScientificsOrders);
        public double calculatePriceBuyOfExportMedicine(List<ReceiptMedicineIdOrderdetail> receiptMedicineOrders);
        public double calculatePriceSellOfExportMedicine(List<ReceiptMedicineIdOrderdetail> receiptMedicineOrders);
    }
}
