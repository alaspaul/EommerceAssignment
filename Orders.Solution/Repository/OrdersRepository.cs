using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Entities;
using RepositoryContracts;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{

    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrdersRepository> _logger;


        public OrdersRepository(ApplicationDbContext db, ILogger<OrdersRepository> logger)
        {
            _db = db;
            _logger = logger;
        }


        public async Task<Order> AddOrder(Order order)
        {
            _logger.LogInformation("Adding order to the database");

            _db.Order.Add(order);

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order has been added");

            return order;
        }


        public async Task<bool> DeleteOrderByOrderId(Guid orderId)
        {
            _logger.LogInformation($"Deleting order - ID: {orderId}");

            var order = await _db.Order.FindAsync(orderId);
            if (order == null)
            {
                _logger.LogWarning($"Order not found - ID: {orderId}");
                return false;
            }

            _db.Order.Remove(order);

            await _db.SaveChangesAsync();

            _logger.LogInformation($"Order has been deleted - ID: {orderId}");

            return true;
        }


        public async Task<List<Order>> GetAllOrders()
        {
            _logger.LogInformation("Retrieving all orders");

         var orders = await _db.Order.OrderByDescending(temp => temp.OrderDate).ToListAsync();

            _logger.LogInformation($"Retrieved {orders.Count} orders");

            return orders;
        }


        public async Task<List<Order>> GetFilteredOrders(Expression<Func<Order, bool>> predicate)
        {
            _logger.LogInformation("Retrieving filtered orders");

         var filteredOrders = await _db.Order.Where(predicate)
                .OrderByDescending(temp => temp.OrderDate).ToListAsync();

            _logger.LogInformation($"Retrieved {filteredOrders.Count} filtered orders");

            return filteredOrders;
        }


        public async Task<Order?> GetOrderByOrderId(Guid orderId)
        {
            _logger.LogInformation($"Retrieving order - ID: {orderId}");

            var order = await _db.Order.FindAsync(orderId);

            if (order == null)
            {
                _logger.LogWarning($"Order not found - ID: {orderId}.");
            }
            else
            {
                _logger.LogInformation($"Order has been retrieved - ID: {orderId}");
            }

            return order;
        }


        public async Task<Order> UpdateOrder(Order order)
        {
            _logger.LogInformation($"Updating order - ID: {order.OrderID}");

            var existingOrder = await _db.Order.FindAsync(order.OrderID);
            if (existingOrder == null)
            {
                _logger.LogWarning($"Order not found - ID: {order.OrderID}");
                return order;
            }

            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.TotalAmount = order.TotalAmount;

            await _db.SaveChangesAsync();

            _logger.LogInformation($"Order has been updated - ID: {order.OrderID}");

            return order;
        }
    }
}