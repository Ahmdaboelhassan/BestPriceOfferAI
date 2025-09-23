namespace OptimumERP.AI.Models.PriceQuote
{
    public class ItemPriceQuoteRaw
    {
        public string Item { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRatio { get; set; }
        public decimal DiscountRatio2 { get; set; }
        public decimal DiscountRatio3 { get; set; }
        public string CompanyName { get; set; }
    }
}
