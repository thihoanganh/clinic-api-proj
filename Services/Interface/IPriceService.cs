using Clinic_Web_Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IPriceService
    {
        public void create(PriceModel priceModel);
        public PriceModel getRecentPriceOfProduct(int productId, DateTime date);
    }
}
