using ServiceContracts.DTO;

namespace ServiceContracts.Orders
{

    public interface IOrdersUpdaterService
    {
        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderRequest">The updated order details.</param>
        /// <returns>The updated order.</returns>
        Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderRequest);
    }
}