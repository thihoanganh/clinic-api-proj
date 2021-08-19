using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Helpers;
using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{

    public class DetailOrderServiceImplement : IDetailOrderService
    {
        private ClinicDbContext db;
        private StatisticalHelper statisticalHelper;
        private IEnumerable<IPriceService> priceServices;
        public DetailOrderServiceImplement(ClinicDbContext _db, IEnumerable<IPriceService> _priceService)
        {
            db = _db;
            priceServices = _priceService;
            statisticalHelper = new StatisticalHelper(db, priceServices);
        }

        public void createDetailOrder(DetailOrderModel detailOrderModel)
        {
            

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    DetailOrder detailOrder = new DetailOrder();
                    detailOrder.CustomerId = detailOrderModel.CustomerId;
                    detailOrder.Date = DateTime.Now;
                    detailOrder.Discount = detailOrderModel.Discount;

                    db.DetailOrders.Add(detailOrder);
                    db.SaveChanges();

                    //kiểm tra số lượng trước khi trừ

                    foreach (var infoMedicine in detailOrderModel.InfoMedicineBuyModels)
                    {
                        var receiptMedicines = db.ReceiptMedicines.Where(r => r.Amount > 0 && (infoMedicine.MedicineId == r.MedicineId))
                            .OrderBy(r => r.Date).ToList();
                        int amoutMedicineBuy = infoMedicine.Amount;
                        foreach (var receiptMedicine in receiptMedicines)
                        {
                            int amountReceipMedicineDetailOrder = 0;
                            if (receiptMedicine.Amount > amoutMedicineBuy)
                            {
                                receiptMedicine.Amount = (int)(receiptMedicine.Amount - amoutMedicineBuy);
                                amountReceipMedicineDetailOrder = amoutMedicineBuy;
                                amoutMedicineBuy = 0;
                                this.updateReceiptMedicine(receiptMedicine);
                                this.createReceipMedicineDetailOrder(receiptMedicine, detailOrder, amountReceipMedicineDetailOrder);
                                break;
                            }
                            else
                            {
                                amoutMedicineBuy = (int)(amoutMedicineBuy - receiptMedicine.Amount);
                                amountReceipMedicineDetailOrder = (int)receiptMedicine.Amount;
                                receiptMedicine.Amount = 0;
                                this.updateReceiptMedicine(receiptMedicine);
                                //kiểm tra id có tồn tại hay ko?
                                this.createReceipMedicineDetailOrder(receiptMedicine, detailOrder, amountReceipMedicineDetailOrder);
                            }

                        }
                        if(amoutMedicineBuy > 0)
                        {
                            throw new Exception();
                        }
                    }

                    foreach (var infoScientificEquipment in detailOrderModel.InfoScientificEquipmentBuyModels)
                    {
                        var receiptScientificEquipments = db.ReceiptScientificEquipments.Where(r => r.Amount > 0 &&
                        (infoScientificEquipment.ScientificEquipmentId == r.ScientificEquipmentId))
                        .OrderBy(r => r.Date).ToList();

                        int amoutScientificEquipmentBuy = infoScientificEquipment.Amount;
                        foreach (var receiptScientificEquipment in receiptScientificEquipments)
                        {
                            int amountReceiptScientificEquipmentDetailOrder = 0;
                            if (receiptScientificEquipment.Amount > amoutScientificEquipmentBuy)
                            {
                                receiptScientificEquipment.Amount = (int)(receiptScientificEquipment.Amount - amoutScientificEquipmentBuy);
                                amountReceiptScientificEquipmentDetailOrder = amoutScientificEquipmentBuy;
                                amoutScientificEquipmentBuy = 0;
                                this.updateReceiptScientificEquipment(receiptScientificEquipment);
                                this.createReceipScientificEquipmentDetailOrder(receiptScientificEquipment, detailOrder, amountReceiptScientificEquipmentDetailOrder);
                                break;
                            }
                            else
                            {
                                amoutScientificEquipmentBuy = (int)(amoutScientificEquipmentBuy - receiptScientificEquipment.Amount);
                                amountReceiptScientificEquipmentDetailOrder = (int)receiptScientificEquipment.Amount;
                                receiptScientificEquipment.Amount = 0;
                                this.updateReceiptScientificEquipment(receiptScientificEquipment);
                                this.createReceipScientificEquipmentDetailOrder(receiptScientificEquipment, detailOrder, amountReceiptScientificEquipmentDetailOrder);
                            }

                        }
                        if (amoutScientificEquipmentBuy > 0)
                        {
                            throw new Exception();
                        }
                    }
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error occurred.");
            }
        }


        }

        private void createReceipScientificEquipmentDetailOrder(ReceiptScientificEquipment receiptScientificEquipment, DetailOrder detailOrder, int amountReceiptScientificEquipmentDetailOrder)
        {
            ReceiptScientificEquipmentIdOrderDetail receiptScientificEquipmentIdOrderDetail = new ReceiptScientificEquipmentIdOrderDetail();
            receiptScientificEquipmentIdOrderDetail.Amount = amountReceiptScientificEquipmentDetailOrder;
            receiptScientificEquipmentIdOrderDetail.OrderDetailId = detailOrder.Id;
            receiptScientificEquipmentIdOrderDetail.ReceiptScientificEquipmentId = receiptScientificEquipment.Id;

            db.ReceiptScientificEquipmentIdOrderDetails.Add(receiptScientificEquipmentIdOrderDetail);
            db.SaveChanges();
        }

        private void updateReceiptScientificEquipment(ReceiptScientificEquipment receiptScientificEquipment)
        {
            db.Entry(receiptScientificEquipment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        private void createReceipMedicineDetailOrder(ReceiptMedicine receiptMedicine, DetailOrder detailOrder, int amountReceipMedicineDetailOrder)
        {
            ReceiptMedicineIdOrderdetail receiptMedicineIdOrderdetail = new ReceiptMedicineIdOrderdetail();
            receiptMedicineIdOrderdetail.Amount = amountReceipMedicineDetailOrder;
            receiptMedicineIdOrderdetail.OrderdetailId = detailOrder.Id;
            receiptMedicineIdOrderdetail.ReceiptMedicineId = receiptMedicine.Id;

            db.ReceiptMedicineIdOrderdetails.Add(receiptMedicineIdOrderdetail);
            db.SaveChanges();
        }

        private void updateReceiptMedicine(ReceiptMedicine receiptMedicine)
        {
            db.Entry(receiptMedicine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public DetailOrderProfit getProfit(int detaiorderId)
        {
            
            var detailOrder = db.DetailOrders.Where(d => d.Id == detaiorderId).FirstOrDefault();

            var receiptMedicineOrders = db.ReceiptMedicineIdOrderdetails.Where(r => r.OrderdetailId == detailOrder.Id).ToList();

            var receiptScientificOrders = db.ReceiptScientificEquipmentIdOrderDetails.Where(r => r.OrderDetailId == detailOrder.Id).ToList();

            var totalPriceBuy = statisticalHelper.calculatePriceBuyOfExportMedicine(receiptMedicineOrders) + statisticalHelper.calculatePriceBuyOfExportScientific(receiptScientificOrders);
            var totalPriceSell = statisticalHelper.calculatePriceSellOfExportMedicine(receiptMedicineOrders) + statisticalHelper.calculatePriceSellOfExportScientific(receiptScientificOrders);

            DetailOrderProfit detailOrderProfit = new DetailOrderProfit
            {
                Date = (DateTime)detailOrder.Date,
                PriceBuy = totalPriceBuy,
                PriceSell = totalPriceSell,
                CustomerId = (int)detailOrder.CustomerId,
                DetailOrderId = detailOrder.Id
            };
            return detailOrderProfit;
        }

        public List<StatisticalMedicine> getProfitOfMedicine(int medicineId, DateTime fromDate, DateTime toDate)
        {
            List<StatisticalMedicine> statisticalMedicines = new List<StatisticalMedicine>();
            String nameMedicine = db.Medicines.Where(m => m.Id == medicineId).Select(m => m.Name).FirstOrDefault();
            var receiptMedicines = db.ReceiptMedicines.Where(r => r.MedicineId == medicineId ).ToList();

            foreach(var receiptMedicine in receiptMedicines)
            {
                var receiptMedicineOrders = db.ReceiptMedicineIdOrderdetails.Where(r => r.ReceiptMedicineId == receiptMedicine.Id && r.Orderdetail.Date <= toDate && r.Orderdetail.Date >=  fromDate).ToList();

                if(receiptMedicineOrders.Count == 0)
                {
                    continue;
                }
                var totalPriceBuy = statisticalHelper.calculatePriceBuyOfExportMedicine(receiptMedicineOrders);
                var totalPriceSell = statisticalHelper.calculatePriceSellOfExportMedicine(receiptMedicineOrders);

                var quantity = receiptMedicineOrders.Sum(r => r.Amount);
                StatisticalMedicine statisticalMedicine = new StatisticalMedicine
                {
                    PriceBuy = totalPriceBuy,
                    PriceSell = totalPriceSell,
                    NameMedicine = nameMedicine,
                    Quantity = (int)quantity
                };

                statisticalMedicines.Add(statisticalMedicine);
            }
           

            
            return statisticalMedicines;
        }

        public List<StatisticalScientificEquipment> getProfitOfScientificEquipment(int scientificEquipmentId, DateTime fromDate, DateTime toDate)
        {
            List<StatisticalScientificEquipment> statisticalScientificEquipments = new List<StatisticalScientificEquipment>();
            String nameMedicine = db.ScientificEquipments.Where(m => m.Id == scientificEquipmentId).Select(m => m.Name).FirstOrDefault();
            var receiptScientificEquipments = db.ReceiptScientificEquipments.Where(r => r.ScientificEquipmentId == scientificEquipmentId).ToList();

            foreach (var receiptScientificEquipment in receiptScientificEquipments)
            {
                var receiptScientificEquipmentOrders = db.ReceiptScientificEquipmentIdOrderDetails.
                    Where(r => r.ReceiptScientificEquipmentId == receiptScientificEquipment.Id && r.OrderDetail.Date <= toDate && r.OrderDetail.Date >= fromDate).ToList();

                if (receiptScientificEquipmentOrders.Count == 0)
                {
                    break;
                }
                var totalPriceBuy = statisticalHelper.calculatePriceBuyOfExportScientific(receiptScientificEquipmentOrders);
                var totalPriceSell = statisticalHelper.calculatePriceSellOfExportScientific(receiptScientificEquipmentOrders);

                var quantity = receiptScientificEquipmentOrders.Sum(r => r.Amount);
                StatisticalScientificEquipment statisticalScientificEquipment = new StatisticalScientificEquipment
                {
                    PriceBuy = totalPriceBuy,
                    PriceSell = totalPriceSell,
                    NameScientificEquipment = nameMedicine,
                    Quantity = (int)quantity
                };

                statisticalScientificEquipments.Add(statisticalScientificEquipment);
            }



            return statisticalScientificEquipments;
        }

    }
}
