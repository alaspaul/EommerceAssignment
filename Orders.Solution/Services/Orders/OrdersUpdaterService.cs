using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.Orders;

namespace Services.Orders
{

    public class OrdersUpdaterService : IOrdersUpdaterService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILogger<OrdersUpdaterService> _logger;


        public OrdersUpdaterService(IOrdersRepository ordersRepository, ILogger<OrdersUpdaterService> logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
        }


        public async Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderRequest)
        {
            _logger.LogInformation($"Updating order with ID: {orderRequest.OrderID}");

            var order = orderRequest.ToOrder();
            var updatedOrder = await _ordersRepository.UpdateOrder(order);
            var updatedOrderResponse = updatedOrder.ToOrderResponse();

            _logger.LogInformation($"Order with ID {orderRequest.OrderID} has been updated");

            return updatedOrderResponse;
        }
    }
}