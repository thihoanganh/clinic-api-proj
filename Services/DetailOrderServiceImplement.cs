using Clinic_Web_Api.Entities;
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
        private IEnumerable<IPriceService> priceServices;
        public DetailOrderServiceImplement(ClinicDbContext _db,IEnumerable<IPriceService> _priceService)
        {
            db = _db;
            priceServices = _priceService;
        }

        public void createDetailOrder(DetailOrderModel detailOrderModel)
        {
            

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("sdfsdfsdfsdfsdf");
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

            var totalPriceBuy = this.calculatePriceBuyOfExportMedicine(receiptMedicineOrders) + this.calculatePriceBuyOfExportScientific(receiptScientificOrders);
            var totalPriceSell = this.calculatePriceSellOfExportMedicine(receiptMedicineOrders) + this.calculatePriceSellOfExportScientific(receiptScientificOrders);

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

        private double calculatePriceBuyOfExportScientific(List<ReceiptScientificEquipmentIdOrderDetail> receiptScientificsOrders)
        {
            double sumPriceBuy = 0;
            foreach(var receiptScientificOrder in receiptScientificsOrders)
            {
                var priceBuy = db.ReceiptScientificEquipments.Where(r => r.Id == receiptScientificOrder.ReceiptScientificEquipmentId).FirstOrDefault().PriceBuy;
                sumPriceBuy = (double)(sumPriceBuy + priceBuy * receiptScientificOrder.Amount);
            }
            return sumPriceBuy;
        }

        private double calculatePriceSellOfExportScientific(List<ReceiptScientificEquipmentIdOrderDetail> receiptScientificsOrders)
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

        private double calculatePriceBuyOfExportMedicine(List<ReceiptMedicineIdOrderdetail> receiptMedicineOrders)
        {
            double sumPriceBuy = 0;
            foreach (var receiptMedicineOrder in receiptMedicineOrders)
            {
                var priceBuy = db.ReceiptMedicines.Where(r => r.Id == receiptMedicineOrder.ReceiptMedicineId).FirstOrDefault().PriceBuy;
                sumPriceBuy = (double)(sumPriceBuy + priceBuy * receiptMedicineOrder.Amount);
            }
            return sumPriceBuy;
        }

        private double calculatePriceSellOfExportMedicine(List<ReceiptMedicineIdOrderdetail> receiptMedicineOrders)
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
