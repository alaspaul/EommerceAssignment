using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IBuyOrderService
    {
        /// <summary>
        /// creates a new buy order.
        /// </summary>
        /// <param name="request">Accepts an object type of BuyOrderRequest</param>
        /// <returns>Returns the newly created BuyOrder as a BuyOrderResponse</returns>
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest request);

        /// <summary>
        /// Gets the list Of Buy Orders
        /// </summary>
        /// <returns>Returns the List of BuyOrderResponse</returns>
        public Task<List<BuyOrderResponse>> GetBuyOrders();


    }
}
