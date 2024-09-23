using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ServiceContracts.DTO;
using RepositoryContracts;
using ServiceContracts.OrderItems;

namespace Services.OrderItems
{

    public class OrderItemsUpdaterService : IOrderItemsUpdaterService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ILogger<OrderItemsUpdaterService> _logger;

        public OrderItemsUpdaterService(IOrderItemsRepository orderItemsRepository, ILogger<OrderItemsUpdaterService> logger)
        {
            _orderItemsRepository = orderItemsRepository;
            _logger = logger;
        }


        public async Task<OrderItemResponse> UpdateOrderItem(OrderItemUpdateRequest orderItemRequest)
        {
            _logger.LogInformation($"Updating order item - Order Item ID: {orderItemRequest.OrderItemID}");


            var orderItem = orderItemRequest.ToOrderItem();


            var updatedOrderItem = await _orderItemsRepository.UpdateOrderItem(orderItem);

            _logger.LogInformation($"Order item has been updated - Order Item ID: {updatedOrderItem.OrderItemID}");


            return updatedOrderItem.ToOrderItemResponse();
        }
    }
}