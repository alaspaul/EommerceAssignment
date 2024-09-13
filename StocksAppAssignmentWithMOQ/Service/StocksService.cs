using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Service
{
    public class StocksService : IStocksService
    {
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly ISellOrderRepository _sellOrderRepository;

        public StocksService(
            IBuyOrderRepository buyOrderRepository,
            ISellOrderRepository sellOrderRepository)
        {
            _buyOrderRepository = buyOrderRepository;
            _sellOrderRepository = sellOrderRepository;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest request)
        {
            if (request == null) { throw new ArgumentNullException(); }


            ValidationHelper.ModelValidation(request);

            BuyOrder buyOrder = request.ToBuyOrder();

            buyOrder.BuyOrderId = Guid.NewGuid();

            await _buyOrderRepository.CreateBuyOrder(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest request)
        {
            if (request == null) { throw new ArgumentNullException(); }


            ValidationHelper.ModelValidation(request);

            SellOrder sellOrder = request.ToSellOrder();

            sellOrder.SellOrderId = Guid.NewGuid();

            await _sellOrderRepository.CreateSellOrder(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return (await _buyOrderRepository.GetBuyOrders())
                    .Select(buyorder=> buyorder.ToBuyOrderResponse()).ToList();


        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return (await _sellOrderRepository.GetSellOrders())
                     .Select(sellorder => sellorder.ToSellOrderResponse()).ToList();
        }
    }
}
