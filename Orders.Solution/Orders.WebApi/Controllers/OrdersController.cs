using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceContracts.DTO;
using ServiceContracts.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing orders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersAdderService _ordersAdderService;
        private readonly IOrdersGetterService _ordersGetterService;
        private readonly IOrdersUpdaterService _ordersUpdaterService;
        private readonly IOrdersDeleterService _ordersDeleterService;
        private readonly ILogger<OrdersController> _logger;


        public OrdersController(
            IOrdersAdderService ordersAdderService,
            IOrdersGetterService ordersGetterService,
            IOrdersUpdaterService ordersUpdaterService,
            IOrdersDeleterService ordersDeleterService,
            ILogger<OrdersController> logger)
        {
            _ordersAdderService = ordersAdderService;
            _ordersGetterService = ordersGetterService;
            _ordersUpdaterService = ordersUpdaterService;
            _ordersDeleterService = ordersDeleterService;
            _logger = logger;
        }


        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A list of orders.</returns>
        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {
            _logger.LogInformation("Retrieving all orders");

            var orders = await _ordersGetterService.GetAllOrders();

            _logger.LogInformation("All orders retrieved successfully");

            return Ok(orders);
        }


        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The retrieved order, or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(Guid id)
        {
            _logger.LogInformation($"Retrieving order with ID: {id}");

            var order = await _ordersGetterService.GetOrderByOrderId(id);

            if (order == null)
            {
                _logger.LogWarning($"Order - ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Order - ID {id} has been retrieved");

            return Ok(order);
        }


        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="orderRequest">The order details.</param>
        /// <returns>The added order.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> AddOrder(OrderAddRequest orderRequest)
        {
            _logger.LogInformation("Adding a new order");

            var addedOrder = await _ordersAdderService.AddOrder(orderRequest);

            _logger.LogInformation($"Order - ID {addedOrder.OrderID} has been added");

            return CreatedAtAction(nameof(GetOrderById), new { id = addedOrder.OrderID }, addedOrder);
        }


        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="orderRequest">The updated order details.</param>
        /// <returns>The updated order.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> UpdateOrder(Guid id, OrderUpdateRequest orderRequest)
        {
            if (id != orderRequest.OrderID)
            {
                _logger.LogWarning($"ID ({id}) and order request ({orderRequest.OrderID}) not the same");
                return BadRequest();
            }

            _logger.LogInformation($"Updating order - ID: {id}");

            var updatedOrder = await _ordersUpdaterService.UpdateOrder(orderRequest);

            _logger.LogInformation($"Order - ID {id} has been updated");

            return Ok(updatedOrder);
        }


        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>No content if deletion is successful, or NotFound if order not found.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            _logger.LogInformation($"Deleting order with ID: {id}");

            var isDeleted = await _ordersDeleterService.DeleteOrderByOrderId(id);

            if (!isDeleted)
            {
                _logger.LogWarning($"Order - ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Order - ID {id} has been deleted");

            return NoContent();
        }
    }
}