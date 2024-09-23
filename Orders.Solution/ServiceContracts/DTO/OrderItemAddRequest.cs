using Entities;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{

    public class OrderItemAddRequest
    {


        [Required(ErrorMessage = "Order Id is required")]
        public Guid OrderID { get; set; }


        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(50, ErrorMessage = "Product Name is required")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be Positive Value")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit Price must be Positive Value")]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Price must be Positive Value")]
        public decimal TotalPrice { get; set; }


        /// <summary>
        /// Converts the <see cref="OrderItemAddRequest"/> object to an <see cref="OrderItem"/> object.
        /// </summary>
        /// <returns>The converted <see cref="OrderItem"/> object.</returns>
        public OrderItem ToOrderItem()
        {
            return new OrderItem
            {
                OrderID = OrderID,
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice
            };
        }
    }


    /// <summary>
    /// Provides extension methods for <see cref="OrderItemAddRequest"/> objects.
    /// </summary>
    public static class OrderItemAddRequestExtensions
    {
        /// <summary>
        /// Converts a list of <see cref="OrderItemAddRequest"/> objects to a list of <see cref="OrderItem"/> objects.
        /// </summary>
        /// <param name="orderItemRequests">The list of <see cref="OrderItemAddRequest"/> objects to convert.</param>
        /// <returns>A list of <see cref="OrderItem"/> objects.</returns>
        public static List<OrderItem> ToOrderItems(this List<OrderItemAddRequest> orderItemRequests)
        {
            var orderItems = new List<OrderItem>();
            foreach (var orderItemRequest in orderItemRequests)
            {
                var orderItem = new OrderItem
                {
                    OrderID = orderItemRequest.OrderID,
                    ProductName = orderItemRequest.ProductName,
                    Quantity = orderItemRequest.Quantity,
                    UnitPrice = orderItemRequest.UnitPrice,
                    TotalPrice = orderItemRequest.TotalPrice
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }
    }

}