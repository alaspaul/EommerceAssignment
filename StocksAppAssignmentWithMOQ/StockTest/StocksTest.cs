using AutoFixture;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RepositoryContracts;
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
        private readonly Mock<IBuyOrderRepository> _buyOrderRepositoryMock;
        private readonly Mock<ISellOrderRepository> _sellOrderRepositoryMock;
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly IFixture _fixture;

        public StocksTest(ITestOutputHelper testOutputHelper) 
        {
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
            _buyOrderRepositoryMock = new Mock<IBuyOrderRepository>();
            _sellOrderRepositoryMock = new Mock<ISellOrderRepository>();
            _buyOrderRepository = _buyOrderRepositoryMock.Object;
            _sellOrderRepository = _sellOrderRepositoryMock.Object;

            _stocksService = new StocksService(_buyOrderRepository, _sellOrderRepository);
        }


        #region CreateBuyOrder
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ToBeArgumentException()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = null;

            //assert
            Func<Task> act = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);
            
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_ValidationErrors_ToBeArgumentException()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(temp => temp.StockName , null as string)
                                                      .With(temp => temp.StockSymbol, null as string)
                                                      .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                                                      .Create();

            //assert
            Func <Task> act = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

            await act.Should().ThrowAsync<ArgumentException>();

        }

        [Fact]
        public async Task CreateBuyOrder_ValidRequest_ToBeSuccessful()
        {
            //arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                          .With(temp => temp.Price, 12)
                                          .With(temp => temp.Quantity, (uint)1)
                                          .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                                          .Create();
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _buyOrderRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);
            //act
            BuyOrderResponse response = await _stocksService.CreateBuyOrder(buyOrderRequest);
            buyOrder.BuyOrderId = response.BuyOrderId;

            //assert
            response.Should().Be(buyOrder.ToBuyOrderResponse());
        }
        #endregion

        #region GetBuyOrders
        [Fact]
        public async Task GetBuyOrders_EmptyBuyOrders_ToBeEmpty()
        {
            //Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            _buyOrderRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrders);
            //Act
            List<BuyOrderResponse> buyOrdersResponse = await _stocksService.GetBuyOrders();

            //Assert
            buyOrdersResponse.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBuyOrders_AddFewBuyOrders_ToBeSuccessful()
        {
            //Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>()
            {
                _fixture.Create<BuyOrder>(),
                _fixture.Create<BuyOrder>(),
                _fixture.Create<BuyOrder>()
            };

            
            List<BuyOrderResponse> buyOrderResponse = 
                buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();

            _buyOrderRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrders);
            //Act
            List<BuyOrderResponse> actualBuyOrderResponse = await _stocksService.GetBuyOrders();

            //Assert
            actualBuyOrderResponse.Should().BeEquivalentTo(buyOrderResponse);

        }

        #endregion

        #region CreateSellOrder
        [Fact]
        public async Task CreateSellOrder_NullBuyOrder_ToBeArgumentException()
        {
            //arrange
            SellOrderRequest sellOrderRequest = null;

            //assert
            Func<Task> act = async () => await _stocksService.CreateSellOrder(sellOrderRequest);
   
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_ValidationErrors_ToBeArgumentException()
        {
            //arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                      .With(temp => temp.StockName, null as string)
                                                      .With(temp => temp.StockSymbol, null as string)
                                                      .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                                                      .Create();

            //assert
            Func<Task> act = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_ValidRequest_ToBeSuccessful()
        {
            //arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                              .With(temp => temp.Price, 12)
                              .With(temp => temp.Quantity, (uint)1)
                              .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                              .Create();
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _sellOrderRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);
            //act
            SellOrderResponse response = await _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrder.SellOrderId = response.SellOrderId;

            //assert
            response.Should().Be(sellOrder.ToSellOrderResponse());
        }
        #endregion

        #region GetSellOrders
        [Fact]
        public async Task GetSellOrders_EmptyBuyOrders_ToBeEmpty()
        {
            //Arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            _sellOrderRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrders);

            //Act
            List<SellOrderResponse> sellOrdersResponse = await _stocksService.GetSellOrders();

            //Assert
            sellOrdersResponse.Should().BeEmpty();
        }

        [Fact]
        public async void GetSellOrders_AddFewBuyOrders()
        {
            //Arrange
            List<SellOrder> sellOrders = new List<SellOrder>()
            {
                _fixture.Create<SellOrder>(),
                _fixture.Create<SellOrder>(),
                _fixture.Create<SellOrder>()
            };


            List<SellOrderResponse> sellOrderResponse =
                sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();

            _sellOrderRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrders);
            //Act
            List<SellOrderResponse> actualSellOrderResponse = await _stocksService.GetSellOrders();

            //Assert
            actualSellOrderResponse.Should().BeEquivalentTo(sellOrderResponse);
        }
        #endregion

    }
}