using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;

namespace ServiceContracts
{
    public interface IStocksService
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

        /// <summary>
        /// creates a new sell order
        /// </summary>
        /// <param name="request">Accepts an object type of SellOrderRequest</param>
        /// <returns>Returns The newly created SellORder as a SellOrderResponse</returns>
        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest request);

        /// <summary>
        /// Gets The List of Sell Orders
        /// </summary>
        /// <returns>returns a List of SellOrderResponse</returns>
        public Task<List<SellOrderResponse>> GetSellOrders();


    }
}
