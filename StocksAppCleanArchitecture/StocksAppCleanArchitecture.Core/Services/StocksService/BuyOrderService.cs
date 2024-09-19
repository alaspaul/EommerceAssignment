using Service.Helpers;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Entities;
using RepositoryContracts;
using Microsoft.Extensions.Logging;
using ServiceContracts.StocksServiceContracts;

namespace StocksAppCleanArchitecture.Core.Services.StocksService
{
    public class BuyOrderService : IBuyOrderService
    {
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly ILogger<BuyOrderService> _logger;

        public BuyOrderService(
            IBuyOrderRepository buyOrderRepository,
            ISellOrderRepository sellOrderRepository,
            ILogger<BuyOrderService> logger)
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


        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            _logger.LogInformation("Getting Buy Orders");

            return (await _buyOrderRepository.GetBuyOrders())
                    .Select(buyorder => buyorder.ToBuyOrderResponse()).ToList();


        }

    }
}
