using Entities;
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

        public StocksTest(ITestOutputHelper testOutputHelper) 
        {
            _stocksService = new StocksService();
            _testOutputHelper = testOutputHelper;
        }


        #region CreateBuyOrder
        [Fact]
        public void CreateBuyOrder_NullBuyOrder()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = null;

            //assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_ValidationErrors()
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
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });

        }

        [Fact]
        public void CreateBuyOrder_ValidRequest()
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
            BuyOrderResponse response = _stocksService.CreateBuyOrder(buyOrderRequest);

            //assert
            Assert.True(response.BuyOrderId != Guid.Empty);
        }
        #endregion

        #region GetBuyOrders
        [Fact]
        public void GetBuyOrders_EmptyBuyOrders()
        {
            List<BuyOrderResponse> buyOrders = _stocksService.GetBuyOrders();

            Assert.Empty(buyOrders);
        }

        [Fact]
        public void GetBuyOrders_AddFewBuyOrders()
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
                buyOrderResponse.Add(_stocksService.CreateBuyOrder(buyorder));
            }

            List<BuyOrderResponse> actualBuyOrderResponse = _stocksService.GetBuyOrders();

            foreach (var buyOrder in buyOrderResponse) 
            {
                Assert.Contains(buyOrder, actualBuyOrderResponse);
            }

        }

        #endregion

        #region CreateSellOrder
        [Fact]
        public void CreateSellOrder_NullBuyOrder()
        {
            //arrange
            SellOrderRequest sellOrderRequest = null;

            //assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_ValidationErrors()
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
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });

        }

        [Fact]
        public void CreateSellOrder_ValidRequest()
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
            SellOrderResponse response = _stocksService.CreateSellOrder(sellOrderRequest);

            //assert
            Assert.True(response.SellOrderId != Guid.Empty);
        }
        #endregion

        #region GetSellOrders
        [Fact]
        public void GetSellOrders_EmptyBuyOrders()
        {
            List<SellOrderResponse> sellOrders = _stocksService.GetSellOrders();

            Assert.Empty(sellOrders);
        }

        [Fact]
        public void GetSellOrders_AddFewBuyOrders()
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
                sellOrderResponse.Add(_stocksService.CreateSellOrder(sellOrder));
            }

            List<SellOrderResponse> actualSellOrderResponse = _stocksService.GetSellOrders();

            foreach (var sellOrder in sellOrderResponse)
            {
                Assert.Contains(sellOrder, actualSellOrderResponse);
            }

        }
        #endregion

    }
}