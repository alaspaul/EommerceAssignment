using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging;

namespace Service
{
    public class StocksService : IStocksService
    {
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly ILogger<StocksService> _logger;

        public StocksService(
            IBuyOrderRepository buyOrderRepository,
            ISellOrderRepository sellOrderRepository,
            ILogger<StocksService> logger)
        {
            _buyOrderRepository = buyOrderRepository;
            _sellOrderRepository = sellOrderRepository;
            _logger = logger;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest request)
        {
            if (request == null) { throw new ArgumentNullException(); }

            _logger.LogInformation("Creating Buy Order");
            _logger.LogInformation("Request: {@request}", request);

            ValidationHelper.ModelValidation(request);

            BuyOrder buyOrder = request.ToBuyOrder();

            buyOrder.BuyOrderId = Guid.NewGuid();

            await _buyOrderRepository.CreateBuyOrder(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest request)
        {
            if (request == null) { throw new ArgumentNullException(); }

            _logger.LogInformation("Creating Sell Order");
            _logger.LogInformation("Request: {@request}", request);

            ValidationHelper.ModelValidation(request);

            SellOrder sellOrder = request.ToSellOrder();

            sellOrder.SellOrderId = Guid.NewGuid();

            await _sellOrderRepository.CreateSellOrder(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            _logger.LogInformation("Getting Buy Orders");

            return (await _buyOrderRepository.GetBuyOrders())
                    .Select(buyorder=> buyorder.ToBuyOrderResponse()).ToList();


        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            _logger.LogInformation("Getting Sell Orders");
            return (await _sellOrderRepository.GetSellOrders())
                     .Select(sellorder => sellorder.ToSellOrderResponse()).ToList();
        }
    }
}
