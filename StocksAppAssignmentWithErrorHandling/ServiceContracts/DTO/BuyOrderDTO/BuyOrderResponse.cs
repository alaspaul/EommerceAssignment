using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO.BuyOrderDTO
{
    public class BuyOrderResponse : IOrderResponse
    {
        public Guid BuyOrderId { get; set; }

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

            if (obj.GetType() != typeof(BuyOrderResponse))
            {
                return false;
            }

            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;

            return (BuyOrderId == buyOrderResponse.BuyOrderId &&
                    StockName == buyOrderResponse.StockName &&
                    StockSymbol == buyOrderResponse.StockSymbol &&
                    Price == buyOrderResponse.Price &&
                    Quantity == buyOrderResponse.Quantity &&
                    DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder);
        }
    }

    public static class BuyOrderExtensions
    {
        /// <summary>
        /// Turns a BuyOrder Object into a BuyOrderResponse
        /// </summary>
        /// <param name="buyOrder">Accepts a BuyOrder Object</param>
        /// <returns>Returns a BuyOrderResponse with given buyOrder Object</returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderId = buyOrder.BuyOrderId,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                Price = buyOrder.Price,
                Quantity = buyOrder.Quantity,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };
        }

    }
}
