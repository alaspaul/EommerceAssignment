using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{

    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderItemsRepository> _logger;


        public OrderItemsRepository(ApplicationDbContext db, ILogger<OrderItemsRepository> logger)
        {
            _db = db;
            _logger = logger;
        }


        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            _logger.LogInformation("Adding order item to the database");

            _db.OrderItem.Add(orderItem);

            await _db.SaveChangesAsync();

            _logger.LogInformation($"Order item - ID {orderItem.OrderItemID} added to the database");

            return orderItem;
        }


        /// <inheritdoc/>
        public async Task<bool> DeleteOrderItemByOrderItemId(Guid orderItemId)
        {
            _logger.LogInformation("Deleting order item from the database");

            var orderItem = await _db.OrderItem.FindAsync(orderItemId);
            if (orderItem == null)
            {
                _logger.LogWarning($"Order item not found - ID: {orderItemId}");
                return false;
            }

            _db.OrderItem.Remove(orderItem);

            await _db.SaveChangesAsync();

            _logger.LogInformation($"Order item - ID {orderItemId} deleted from the database");

            return true;
        }


        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            _logger.LogInformation("Retrieving all order items");

            var orderItems = await _db.OrderItem.OrderBy(temp => temp.OrderID).ToListAsync();

            _logger.LogInformation($"Retrieved {orderItems.Count} order items successfully");

            return orderItems;
        }


        public async Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
        {
            _logger.LogInformation("Retrieving order items by OrderId");

            var orderItems = await _db.OrderItem.Where(oi => oi.OrderID == orderId).ToListAsync();

            _logger.LogInformation($"Retrieved {orderItems.Count} order items associated with OrderId: {orderId}");

            return orderItems;
        }


        public async Task<OrderItem?> GetOrderItemByOrderItemId(Guid orderItemId)
        {
            _logger.LogInformation("Retrieving order item by OrderItemId");

            var orderItem = await _db.OrderItem.FindAsync(orderItemId);

            if (orderItem == null)
            {
                _logger.LogWarning($"Order item not found - ID: {orderItemId}");
            }
            else
            {
                _logger.LogInformation("Order item has been retrieved");
            }

            return orderItem;
        }


        public async Task<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            _logger.LogInformation("Updating order item in the database");

            var existingOrderItem = await _db.OrderItem.FindAsync(orderItem.OrderItemID);
            if (existingOrderItem == null)
            {
                throw new ArgumentException($"Order item - ID {orderItem.OrderItemID} does not exist");
            }

            existingOrderItem.OrderID = orderItem.OrderID;
            existingOrderItem.ProductName = orderItem.ProductName;
            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.UnitPrice = orderItem.UnitPrice;
            existingOrderItem.TotalPrice = orderItem.TotalPrice;

            await _db.SaveChangesAsync();

            _logger.LogInformation($"Order item - ID {orderItem.OrderItemID} updated in the database");

            return existingOrderItem;
        }
    }
}