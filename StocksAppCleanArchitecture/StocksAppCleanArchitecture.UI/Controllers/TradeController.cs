using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using ServiceContracts.DTO;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Rotativa.AspNetCore;
using System.Collections.Generic;
using StocksApp.Filters;
using ServiceContracts.FinnhubServiceContracts;
using ServiceContracts.StocksServiceContracts;
using ServiceContracts;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
        public class TradeController : Controller
        {
            private readonly TradingOptions _tradingOptions;
            private readonly IBuyOrderService _buyOrderService;
            private readonly ISellOrderService _sellOrderService;
            private readonly IFinnhubServiceCompanyProfileService _finnhubServiceCompanyProfile;
            private readonly IFinnhubServiceStockPriceQouteService _finnhubServiceStockPriceQoute;
            private readonly IConfiguration _configuration;


            /// <summary>
            /// Constructor for TradeController that executes when a new object is created for the class
            /// </summary>
            /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
            /// <param name="stocksService">Injecting StocksService</param>
            /// <param name="finnhubService">Injecting FinnhubService</param>
            /// <param name="configuration">Injecting IConfiguration</param>
            public TradeController(IOptions<TradingOptions> tradingOptions, 
                                   IBuyOrderService buyOrderService,
                                   ISellOrderService sellOrderService,
                                   IFinnhubServiceCompanyProfileService finnhubServiceCompanyProfile,
                                   IFinnhubServiceStockPriceQouteService finnhubServiceStockPriceQoute,
                                   IConfiguration configuration)
            {
                _tradingOptions = tradingOptions.Value;
                _buyOrderService = buyOrderService;
                _sellOrderService = sellOrderService;
                _finnhubServiceCompanyProfile = finnhubServiceCompanyProfile;
                _finnhubServiceStockPriceQoute = finnhubServiceStockPriceQoute;
                _configuration = configuration;
            }


            [Route("[action]/{stockSymbol}")]
            [Route("~/[controller]/{stockSymbol}")]
            public async Task<IActionResult> Index(string stockSymbol)
            {
                //reset stock symbol if not exists
                if (string.IsNullOrEmpty(stockSymbol))
                    stockSymbol = "MSFT";


                //get company profile from API server
                Dictionary<string, object>? companyProfileDictionary = await _finnhubServiceCompanyProfile.GetCompanyProfile(stockSymbol);

                //get stock price quotes from API server
                Dictionary<string, object>? stockQuoteDictionary = await _finnhubServiceStockPriceQoute.GetStockPriceQuote(stockSymbol);


                //create model object
                StockTrade stockTrade = new StockTrade() { StockSymbol = stockSymbol };

                //load data from finnHubService into model object
                if (companyProfileDictionary != null && stockQuoteDictionary != null)
                {
                    stockTrade = new StockTrade() { 
                        StockSymbol = companyProfileDictionary["ticker"].ToString(), StockName = companyProfileDictionary["name"].ToString(), 
                        Quantity = (int)(_tradingOptions.DefaultOrderQuantity ?? 0), Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
                }

                //Send Finnhub token to view
                ViewBag.FinnhubToken = _configuration["FinnhubToken"];

                return View(stockTrade);
            }


            [Route("[action]")]
            [HttpPost]
            [TypeFilter(typeof(BuyOrderActionFilter))]
            public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
            {
                //update date of order
                buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

                //re-validate the model object after updating the date
                ModelState.Clear();
                TryValidateModel(buyOrderRequest);


                //if (!ModelState.IsValid) //added as a filter
                //{
                //    ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                //    StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, Quantity = (int)buyOrderRequest.Quantity, StockSymbol = buyOrderRequest.StockSymbol };
                //    return View("Index", stockTrade);
                //}

                //invoke service method
                BuyOrderResponse buyOrderResponse = await _buyOrderService.CreateBuyOrder(buyOrderRequest);

                return RedirectToAction(nameof(Orders));
            }


            [Route("[action]")]
            [HttpPost]
            [TypeFilter(typeof(SellOrderActionFilter))]
            public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
            {
                //update date of order
                sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

                //re-validate the model object after updating the date
                ModelState.Clear();
                TryValidateModel(sellOrderRequest);

                //if (!ModelState.IsValid)
                //{
                //    ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                //    StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = (int)sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
                //    return View("Index", stockTrade);
                //}

                //invoke service method
                SellOrderResponse sellOrderResponse = await _sellOrderService.CreateSellOrder(sellOrderRequest);

                return RedirectToAction(nameof(Orders));
            }


            [Route("[action]")]
            public async Task<IActionResult> Orders()
            {
                //invoke service methods
                List<BuyOrderResponse> buyOrderResponses = await _buyOrderService.GetBuyOrders();
                List<SellOrderResponse> sellOrderResponses = await _sellOrderService.GetSellOrders();

                //create model object
                Orders orders = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

                ViewBag.TradingOptions = _tradingOptions;

                return View(orders);
            }


            [Route("OrdersPDF")]
            public async Task<IActionResult> OrdersPDF()
            {
                //Get list of orders
                List<IOrderResponse> orders = new List<IOrderResponse>();
                orders.AddRange(await _buyOrderService.GetBuyOrders());
                orders.AddRange(await _sellOrderService.GetSellOrders());
                orders = orders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

                ViewBag.TradingOptions = _tradingOptions;

                //Return view as pdf
                return new ViewAsPdf("OrdersPDF", orders, ViewData)
                {
                    PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
                };
            }
        }
    }
