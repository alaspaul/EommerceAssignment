using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;

namespace ServiceContracts.StocksServiceContracts
{
    public interface ISellOrderService
    {

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
