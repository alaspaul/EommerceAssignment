namespace StocksApp.Models
{
    public class Stock
    {
        public string? StockSymbol { get; set; }

        public double CurrentPrice { get; set; }

        public double LowestPrioce {  get; set; }

        public double HighestPrioce { get; set; }

        public double OpenPrice { get; set; }
    }
}
