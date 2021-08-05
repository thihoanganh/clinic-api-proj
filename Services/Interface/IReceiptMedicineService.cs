﻿using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IReceiptMedicineService
    {
        public List<ReceiptMedicineModel> getReceipOfMedicine(int id);
        public void createReceipOfMedicine(ReceiptMedicineModel receiptMedicineModel);
    }
}
