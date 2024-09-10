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
        private readonly List<BuyOrderResponse> _buyOrders;
        private readonly List<SellOrderResponse> _sellOrders;
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
            _buyOrders = _stocksService.GetBuyOrders();
            _sellOrders = _stocksService.GetSellOrders();
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
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            _buyOrders.Add(buyOrderResponse);

            return Redirect("/");
        }

        [Route("/Trade/SellOrder")]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            _sellOrders.Add(sellOrderResponse);

            return Redirect("/");
        }


        [Route("/Orders")]
        public IActionResult Orders()
        {
            ViewBag.BuyOrders = _buyOrders;
            ViewBag.SellOrders = _sellOrders;


            return View();
        }
    }
}
