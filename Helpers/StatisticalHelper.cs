using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services;
using Clinic_Web_Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Helpers
{
    public class StatisticalHelper
    {
        private ClinicDbContext db;
        private IEnumerable<IPriceService> priceServices;
        public StatisticalHelper(ClinicDbContext _db, IEnumerable<IPriceService> _priceService)
        {
            db = _db;
            priceServices = _priceService;
        }

        public StatisticalHelper()
        {
        }

        public double calculatePriceBuyOfExportScientific(List<ReceiptScientificEquipmentIdOrderDetail> receiptScientificsOrders)
        {
            double sumPriceBuy = 0;
            foreach (var receiptScientificOrder in receiptScientificsOrders)
            {
                var priceBuy = db.ReceiptScientificEquipments.Where(r => r.Id == receiptScientificOrder.ReceiptScientificEquipmentId).FirstOrDefault().PriceBuy;
                sumPriceBuy = (double)(sumPriceBuy + priceBuy * receiptScientificOrder.Amount);
            }
            return sumPriceBuy;
        }

        public double calculatePriceSellOfExportScientific(List<ReceiptScientificEquipmentIdOrderDetail> receiptScientificsOrders)
        {
            var priceScientificService = priceServices.SingleOrDefault(s => s.GetType() == typeof(PriceScientificEquipServiceImplement));
            double sumPriceSell = 0;
            foreach (var receiptScientificOrder in receiptScientificsOrders)
            {
                var receiptScientific = db.ReceiptScientificEquipments.Where(r => r.Id == receiptScientificOrder.ReceiptScientificEquipmentId).FirstOrDefault();
                var detailOder = db.DetailOrders.Where(r => r.Id == receiptScientificOrder.OrderDetailId).FirstOrDefault();
                var idProduct = receiptScientific.ScientificEquipmentId;
                var date = detailOder.Date;
                var priceSell = priceScientificService.getRecentPriceOfProduct((int)idProduct, (DateTime)date).Price;
                sumPriceSell = (double)(sumPriceSell + priceSell * receiptScientificOrder.Amount);
            }
            return sumPriceSell;
        }

        public double calculatePriceBuyOfExportMedicine(List<ReceiptMedicineIdOrderdetail> receiptMedicineOrders)
        {
            double sumPriceBuy = 0;
            foreach (var receiptMedicineOrder in receiptMedicineOrders)
            {
                ReceiptMedicine receiptMedicine = db.ReceiptMedicines.Where(r => r.Id == receiptMedicineOrder.ReceiptMedicineId).FirstOrDefault();
                var priceBuy = receiptMedicine.PriceBuy;
                sumPriceBuy = (double)(sumPriceBuy + priceBuy * receiptMedicineOrder.Amount);
            }
            return sumPriceBuy;
        }

        public double calculatePriceSellOfExportMedicine(List<ReceiptMedicineIdOrderdetail> receiptMedicineOrders)
        {
            var priceMedicineService = priceServices.SingleOrDefault(s => s.GetType() == typeof(PriceMedicineServiceImplement));
            double sumPriceSell = 0;
            foreach (var receiptMedicineOrder in receiptMedicineOrders)
            {
                var receiptMedicine = db.ReceiptMedicines.Where(r => r.Id == receiptMedicineOrder.ReceiptMedicineId).FirstOrDefault();
                var detailOder = db.DetailOrders.Where(r => r.Id == receiptMedicineOrder.OrderdetailId).FirstOrDefault();
                var idProduct = receiptMedicine.MedicineId;
                var date = detailOder.Date;
                var priceSell = priceMedicineService.getRecentPriceOfProduct((int)idProduct, (DateTime)date).Price;
                sumPriceSell = (double)(sumPriceSell + priceSell * receiptMedicineOrder.Amount);
            }
            return sumPriceSell;
        }
    }
}
