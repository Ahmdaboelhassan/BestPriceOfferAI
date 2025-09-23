using OptimumERP.AI.Models.PriceQuote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimumERP.AI.Extensions
{
    public static class RawExtensions
    {
        public static decimal GetFinalPrice(this ItemPriceQuoteRaw raw)
        {
            var totalPrice = raw.Price * raw.Qty;

            var discValue = (1 - (1 - raw.DiscountRatio) * (1 - raw.DiscountRatio2) * (1 - raw.DiscountRatio3)) * totalPrice;

            return totalPrice - discValue;
        }
    }
    
}
