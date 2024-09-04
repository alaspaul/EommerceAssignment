using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;

namespace StocksApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly FinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IConfiguration _configuration;
        public TradeController
            (FinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptions,
            IConfiguration configuration) 
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
            _configuration = configuration;
        }


        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }
            Dictionary<string, object>? dictQoute = await _finnhubService.
                GetStockPriceQoute(_tradingOptions.Value.DefaultStockSymbol);

            Dictionary<string, object>? dictComp = await _finnhubService.
                GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockName = Convert.ToString(dictComp["name"].ToString()),
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                Price = Convert.ToDouble(dictQoute["c"].ToString())
            };

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            return View(stockTrade);
        }
    }
}
