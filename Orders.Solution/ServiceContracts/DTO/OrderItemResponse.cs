using Entities;
using System;
using System.Collections.Generic;

namespace ServiceContracts.DTO
{

    public class OrderItemResponse
    {

        public Guid OrderItemID { get; set; }



        public Guid OrderID { get; set; }



        public string? ProductName { get; set; }



        public int Quantity { get; set; }



        public decimal UnitPrice { get; set; }



        public decimal TotalPrice { get; set; }
    }


    /// <summary>
    /// Provides extension methods for <see cref="OrderItem"/> objects.
    /// </summary>
    public static class OrderItemResponseExtensions
    {
        /// <summary>
        /// Converts an <see cref="OrderItem"/> object to an <see cref="OrderItemResponse"/> object.
        /// </summary>
        /// <param name="orderItem">The <see cref="OrderItem"/> object to convert.</param>
        /// <returns>An <see cref="OrderItemResponse"/> object.</returns>
        public static OrderItemResponse ToOrderItemResponse(this OrderItem orderItem)
        {
            return new OrderItemResponse
            {
                OrderItemID = orderItem.OrderItemID,
                OrderID = orderItem.OrderID,
                ProductName = orderItem.ProductName,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice
            };
        }


        /// <summary>
        /// Converts a list of <see cref="OrderItem"/> objects to a list of <see cref="OrderItemResponse"/> objects.
        /// </summary>
        /// <param name="orderItems">The list of <see cref="OrderItem"/> objects to convert.</param>
        /// <returns>A list of <see cref="OrderItemResponse"/> objects.</returns>
        public static List<OrderItemResponse> ToOrderItemResponseList(this List<OrderItem> orderItems)
        {
            var orderItemResponses = new List<OrderItemResponse>();
            foreach (var orderItem in orderItems)
            {
                orderItemResponses.Add(orderItem.ToOrderItemResponse());
            }
            return orderItemResponses;
        }
    }
}
