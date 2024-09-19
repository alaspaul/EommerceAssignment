using Service.Helpers;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Entities;
using RepositoryContracts;
using Microsoft.Extensions.Logging;
using ServiceContracts.StocksServiceContracts;
using ServiceContracts;

namespace StocksAppCleanArchitecture.Core.Services.StocksService
{
    public class SellOrderService : ISellOrderService
    {
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly ILogger<SellOrderService> _logger;

        public SellOrderService(
            IBuyOrderRepository buyOrderRepository,
            ISellOrderRepository sellOrderRepository,
            ILogger<SellOrderService> logger)
        {
            _buyOrderRepository = buyOrderRepository;
            _sellOrderRepository = sellOrderRepository;
            _logger = logger;
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


        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            _logger.LogInformation("Getting Sell Orders");
            return (await _sellOrderRepository.GetSellOrders())
                     .Select(sellorder => sellorder.ToSellOrderResponse()).ToList();
        }
    }
}
