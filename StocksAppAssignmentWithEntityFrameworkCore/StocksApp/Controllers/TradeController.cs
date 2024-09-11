using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;
using ServiceContracts;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;

namespace StocksApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly FinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IStocksService _stocksService;
        public TradeController
            (FinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptions,
            IConfiguration configuration,
            IStocksService stocksService) 
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
            _configuration = configuration;
            _stocksService = stocksService;
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


        [Route("/Trade/BuyOrder")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            return Redirect("/");
        }

        [Route("/Trade/SellOrder")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            return Redirect("/");
        }


        [Route("/Orders")]
        public async Task<IActionResult> Orders()
        {
            ViewBag.BuyOrders = await _stocksService.GetBuyOrders();
            ViewBag.SellOrders = await _stocksService.GetSellOrders();


            return View();
        }
    }
}
