namespace OptimumERP.AI.Models.PriceQuote
{
    public class ItemPriceQuoteResult
    {
        public string Item { get; set; }
        public List<PriceQuoteResult> Prices { get; set; } = new();
    }
}
