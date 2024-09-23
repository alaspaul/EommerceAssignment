using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;

namespace Services.Orders
{

    public class OrdersGetterService : IOrdersGetterService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsGetterService _orderItemsGetterService;
        private readonly ILogger<OrdersGetterService> _logger;

        public OrdersGetterService(IOrdersRepository ordersRepository, IOrderItemsGetterService orderItemsGetterService, ILogger<OrdersGetterService> logger)
        {
            _ordersRepository = ordersRepository;
            _orderItemsGetterService = orderItemsGetterService;
            _logger = logger;
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            _logger.LogInformation("Retrieving all orders");

            var orders = await _ordersRepository.GetAllOrders();
            var orderResponses = orders.ToOrderResponseList();
            foreach (var orderResponse in orderResponses)
            {
                orderResponse.OrderItems = await _orderItemsGetterService.GetOrderItemsByOrderId(orderResponse.OrderID);
            }

            _logger.LogInformation("All orders has been retrieved");

            return orderResponses;
        }


        public async Task<OrderResponse?> GetOrderByOrderId(Guid orderId)
        {
            _logger.LogInformation($"Retrieving order with ID: {orderId}");

            var order = await _ordersRepository.GetOrderByOrderId(orderId);
            var orderResponse = order?.ToOrderResponse();
            orderResponse.OrderItems = await _orderItemsGetterService.GetOrderItemsByOrderId(orderResponse.OrderID);

            if (orderResponse == null)
            {
                _logger.LogWarning($"Order with ID {orderId} not found");
            }
            else
            {
                _logger.LogInformation($"Order with ID {orderId} retrieved");
            }

            return orderResponse;
        }
    }
}