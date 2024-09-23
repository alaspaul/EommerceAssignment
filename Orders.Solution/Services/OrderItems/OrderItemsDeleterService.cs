using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.OrderItems;
using System;
using System.Threading.Tasks;

namespace Services.OrderItems
{

    public class OrderItemsDeleterService : IOrderItemsDeleterService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ILogger<OrderItemsDeleterService> _logger;

        public OrderItemsDeleterService(IOrderItemsRepository orderItemsRepository, ILogger<OrderItemsDeleterService> logger)
        {
            _orderItemsRepository = orderItemsRepository;
            _logger = logger;
        }



        public async Task<bool> DeleteOrderItemByOrderItemId(Guid orderItemId)
        {
            _logger.LogInformation($"Deleting order item - Order Item ID: {orderItemId}");

            var isDeleted = await _orderItemsRepository.DeleteOrderItemByOrderItemId(orderItemId);

            if (isDeleted)
            {
                _logger.LogInformation($"Order item has been deleted - ID: {orderItemId}");
            }
            else
            {
                _logger.LogWarning($"Order item not found for deletion. ID: {orderItemId}");
            }

            return isDeleted;
        }
    }
}