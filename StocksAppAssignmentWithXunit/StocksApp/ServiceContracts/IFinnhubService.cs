namespace StocksApp.ServiceContracts
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>?> GetStockPriceQoute(string stockSymbol);

        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}
