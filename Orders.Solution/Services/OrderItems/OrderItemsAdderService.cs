using Microsoft.Extensions.Logging;
using RepositoryContracts;
using System;
using System.Threading.Tasks;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;

namespace Services.OrderItems
{
    public class OrderItemsAdderService : IOrderItemsAdderService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ILogger<OrderItemsAdderService> _logger;

        public OrderItemsAdderService(IOrderItemsRepository orderItemsRepository, ILogger<OrderItemsAdderService> logger)
        {
            _orderItemsRepository = orderItemsRepository;
            _logger = logger;
        }


        public async Task<OrderItemResponse> AddOrderItem(OrderItemAddRequest orderItemRequest)
        {
            _logger.LogInformation("Adding order item");

            var orderItem = orderItemRequest.ToOrderItem();

            orderItem.OrderItemID = Guid.NewGuid();

            var addedOrderItem = await _orderItemsRepository.AddOrderItem(orderItem);

            _logger.LogInformation($"Order item has been added - Order Item ID: {addedOrderItem.OrderItemID}.");

            return addedOrderItem.ToOrderItemResponse();
        }
    }
}