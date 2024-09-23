using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceContracts.DTO;
using RepositoryContracts;
using ServiceContracts.OrderItems;

namespace Services.OrderItems
{

    public class OrderItemsGetterService : IOrderItemsGetterService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ILogger<OrderItemsGetterService> _logger;


        public OrderItemsGetterService(IOrderItemsRepository orderItemsRepository, ILogger<OrderItemsGetterService> logger)
        {
            _orderItemsRepository = orderItemsRepository;
            _logger = logger;
        }


        public async Task<List<OrderItemResponse>> GetAllOrderItems()
        {
            _logger.LogInformation("Retrieving all order items");

            var orderItems = await _orderItemsRepository.GetAllOrderItems();

            _logger.LogInformation("All order items has been retrieved");

            return orderItems.ToOrderItemResponseList();
        }


        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsByOrderId(Guid orderId)
        {
            _logger.LogInformation($"Retrieving order item By Order ID: {orderId}");

            var orderItems = await _orderItemsRepository.GetOrderItemsByOrderId(orderId);

            _logger.LogInformation($"Order items has been retrieved for Order ID: {orderId}");

            return orderItems.ToOrderItemResponseList();
        }


        /// <inheritdoc />
        public async Task<OrderItemResponse?> GetOrderItemByOrderItemId(Guid orderItemId)
        {
            _logger.LogInformation($"Retrieving order item by Order Item ID: {orderItemId}");

            var orderItem = await _orderItemsRepository.GetOrderItemByOrderItemId(orderItemId);

            if (orderItem == null)
            {
                _logger.LogWarning($"Order item not found for Order Item ID: {orderItemId}");
            }
            else
            {
                _logger.LogInformation($"Order item has been retrieved  - Order Item ID: {orderItemId}");
            }

            // Convert the order item to a response DTO
            return orderItem?.ToOrderItemResponse();
        }
    }
}