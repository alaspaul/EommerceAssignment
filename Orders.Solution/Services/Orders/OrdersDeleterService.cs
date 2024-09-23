using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.Orders;

namespace Services.Orders
{

    public class OrdersDeleterService : IOrdersDeleterService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILogger<OrdersDeleterService> _logger;


        public OrdersDeleterService(IOrdersRepository ordersRepository, ILogger<OrdersDeleterService> logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
        }


        public async Task<bool> DeleteOrderByOrderId(Guid orderId)
        {
            _logger.LogInformation($"Deleting order - ID: {orderId}");

            var isDeleted = await _ordersRepository.DeleteOrderByOrderId(orderId);

            if (isDeleted)
            {
                _logger.LogInformation($"Order has been deleted - ID: {orderId}");
            }
            else
            {
                _logger.LogWarning($"Order not found for deletion. ID: {orderId}");
            }

            return isDeleted;
        }
    }
}