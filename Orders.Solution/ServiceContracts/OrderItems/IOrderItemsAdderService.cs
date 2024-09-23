using System;
using System.Threading.Tasks;
using ServiceContracts.DTO;

namespace ServiceContracts.OrderItems
{

    public interface IOrderItemsAdderService
    {
        /// <summary>
        /// Adds an order item.
        /// </summary>
        /// <param name="orderItemRequest">The request containing the order item data.</param>
        /// <returns>The added order item.</returns>
        Task<OrderItemResponse> AddOrderItem(OrderItemAddRequest orderItemRequest);
    }
}