namespace OptimumERP.AI.Models.PriceQuote
{
    public class PriceQuoteResult
    {
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRatio { get; set; }
        public decimal DiscountRatio2 { get; set; }
        public decimal DiscountRatio3 { get; set; }
        public bool IsBest { get; set; }
    }
}
