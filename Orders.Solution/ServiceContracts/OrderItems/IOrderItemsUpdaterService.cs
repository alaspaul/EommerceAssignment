using System;
using System.Threading.Tasks;
using ServiceContracts.DTO;

namespace ServiceContracts.OrderItems
{

    public interface IOrderItemsUpdaterService
    {
        /// <summary>
        /// Updates an order item.
        /// </summary>
        /// <param name="orderItemRequest">The request containing the updated order item data.</param>
        /// <returns>The updated order item.</returns>
        Task<OrderItemResponse> UpdateOrderItem(OrderItemUpdateRequest orderItemRequest);
    }
}