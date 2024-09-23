using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{

    public class OrderItemUpdateRequest
    {

        public Guid OrderItemID { get; set; }


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
        /// Converts the <see cref="OrderItemUpdateRequest"/> object to an <see cref="OrderItem"/> object.
        /// </summary>
        /// <returns>The converted <see cref="OrderItem"/> object.</returns>
        public OrderItem ToOrderItem()
        {
            return new OrderItem
            {
                OrderItemID = OrderItemID,
                OrderID = OrderID,
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice
            };
        }
    }
}