using Entities;
using Microsoft.EntityFrameworkCore;
using Service;
using ServiceContracts;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Xunit.Abstractions;

namespace StockTest
{
    public class StocksTest
    {
        private readonly IStocksService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TradeDbContext _tradeDbContext;

        public StocksTest(ITestOutputHelper testOutputHelper) 
        {
            _stocksService = new StocksService
                      (new TradeDbContext
                      (new DbContextOptionsBuilder<TradeDbContext>().Options));
            _testOutputHelper = testOutputHelper;
        }


        #region CreateBuyOrder
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = null;

            //assert
            await Assert.ThrowsAsync<ArgumentNullException>( async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_ValidationErrors()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockName = null,
                StockSymbol = null,
                Price = 0,
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01") 
            };

            //assert
            await Assert.ThrowsAsync<ArgumentException>( async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });

        }

        [Fact]
        public async Task CreateBuyOrder_ValidRequest()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = 12,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("1999-01-01")
            };
            //act
            BuyOrderResponse response = await _stocksService.CreateBuyOrder(buyOrderRequest);

            //assert
            Assert.True(response.BuyOrderId != Guid.Empty);
        }
        #endregion

        #region GetBuyOrders
        [Fact]
        public async Task GetBuyOrders_EmptyBuyOrders()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();

            Assert.Empty(buyOrders);
        }

        [Fact]
        public async Task GetBuyOrders_AddFewBuyOrders()
        {
            List<BuyOrderRequest> buyOrders = new List<BuyOrderRequest>()
            {
                new BuyOrderRequest(){ StockSymbol = "MSFT", 
                                       StockName = "Microsoft", 
                                       DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                                       Quantity = 10,
                                       Price = 10},
                new BuyOrderRequest(){ StockSymbol = "APPL",
                                       StockName = "APPLE",
                                       DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                                       Quantity = 5,
                                       Price = 12},
            };

            List<BuyOrderResponse> buyOrderResponse = new List<BuyOrderResponse>();

            foreach(var buyorder in buyOrders)
            {
                buyOrderResponse.Add(await _stocksService.CreateBuyOrder(buyorder));
            }

            List<BuyOrderResponse> actualBuyOrderResponse = await _stocksService.GetBuyOrders();

            foreach (var buyOrder in buyOrderResponse) 
            {
                Assert.Contains(buyOrder, actualBuyOrderResponse);
            }

        }

        #endregion

        #region CreateSellOrder
        [Fact]
        public async Task CreateSellOrder_NullBuyOrder()
        {
            //arrange
            SellOrderRequest sellOrderRequest = null;

            //assert
            await Assert.ThrowsAsync<ArgumentNullException>( async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_ValidationErrors()
        {
            //arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockName = null,
                StockSymbol = null,
                Price = 0,
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            //assert
            await Assert.ThrowsAsync<ArgumentException>( async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });

        }

        [Fact]
        public async Task CreateSellOrder_ValidRequest()
        {
            //arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = 12,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("1999-01-01")
            };
            //act
            SellOrderResponse response = await _stocksService.CreateSellOrder(sellOrderRequest);

            //assert
            Assert.True(response.SellOrderId != Guid.Empty);
        }
        #endregion

        #region GetSellOrders
        [Fact]
        public async Task GetSellOrders_EmptyBuyOrders()
        {
            List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();

            Assert.Empty(sellOrders);
        }

        [Fact]
        public async void GetSellOrders_AddFewBuyOrders()
        {
            List<SellOrderRequest> sellOrders = new List<SellOrderRequest>()
            {
                new SellOrderRequest(){ StockSymbol = "MSFT",
                                       StockName = "Microsoft",
                                       DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                                       Quantity = 10,
                                       Price = 10},
                new SellOrderRequest(){ StockSymbol = "APPL",
                                       StockName = "APPLE",
                                       DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                                       Quantity = 5,
                                       Price = 12},
            };

            List<SellOrderResponse> sellOrderResponse = new List<SellOrderResponse>();

            foreach (var sellOrder in sellOrders)
            {
                sellOrderResponse.Add( await _stocksService.CreateSellOrder(sellOrder));
            }

            List<SellOrderResponse> actualSellOrderResponse = await _stocksService.GetSellOrders();

            foreach (var sellOrder in sellOrderResponse)
            {
                Assert.Contains(sellOrder, actualSellOrderResponse);
            }

        }
        #endregion

    }
}