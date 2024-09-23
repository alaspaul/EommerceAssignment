using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.Orders;

namespace Services.Orders
{
    public class OrdersAdderService : IOrdersAdderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ILogger<OrdersAdderService> _logger;

        public OrdersAdderService(IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository, ILogger<OrdersAdderService> logger)
        {
            _ordersRepository = ordersRepository;
            _orderItemsRepository = orderItemsRepository;
            _logger = logger;
        }


        public async Task<OrderResponse> AddOrder(OrderAddRequest orderRequest)
        {
            _logger.LogInformation("Adding a new order");

            var order = orderRequest.ToOrder();
            order.OrderID = Guid.NewGuid();

            var addedOrder = await _ordersRepository.AddOrder(order);
            var addedOrderResponse = addedOrder.ToOrderResponse();

            foreach (var item in orderRequest.OrderItems)
            {
                var orderItem = item.ToOrderItem();
                orderItem.OrderItemID = Guid.NewGuid();
                orderItem.OrderID = addedOrder.OrderID;

                var addedOrderItem = await _orderItemsRepository.AddOrderItem(orderItem);
                addedOrderResponse.OrderItems.Add(addedOrderItem.ToOrderItemResponse());
            }

            _logger.LogInformation("Order has been added");

            return addedOrderResponse;
        }
    }
}