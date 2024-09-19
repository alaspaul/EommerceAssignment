using Entities;
using ServiceContracts.DTO.BuyOrderDTO;

namespace ServiceContracts.DTO.SellOrderDTO
{
    public class SellOrderResponse : IOrderResponse
    {
        public Guid SellOrderId { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime? DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public OrderType TypeOfOrder => OrderType.BuyOrder;

        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }

            if (obj.GetType() != typeof(SellOrderResponse))
            {
                return false;
            }

            SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;

            return (SellOrderId == sellOrderResponse.SellOrderId &&
                    StockName == sellOrderResponse.StockName &&
                    StockSymbol == sellOrderResponse.StockSymbol &&
                    Price == sellOrderResponse.Price &&
                    Quantity == sellOrderResponse.Quantity &&
                    DateAndTimeOfOrder == sellOrderResponse.DateAndTimeOfOrder);
        }

    }

    public static class SellOrderExtensions
    {
        /// <summary>
        /// Turns a SellOrder Object into a SellOrderResponse
        /// </summary>
        /// <param name="sellOrder">Accepts a SellOrder Object</param>
        /// <returns>Returns a SellOrderResponse with given SellOrder Object</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderId= sellOrder.SellOrderId,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                Price = sellOrder.Price,
                Quantity = sellOrder.Quantity,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                TradeAmount = sellOrder.Price * sellOrder.Quantity
            };
        }

    }
}
