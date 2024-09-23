using ServiceContracts.DTO;

namespace ServiceContracts.Orders
{

    public interface IOrdersAdderService
    {
        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="orderRequest">The order details.</param>
        /// <returns>The added order.</returns>
        Task<OrderResponse> AddOrder(OrderAddRequest orderRequest);
    }
}