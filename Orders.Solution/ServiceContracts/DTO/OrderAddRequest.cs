using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{

    public class OrderAddRequest
    {

        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(50, ErrorMessage = "Name should be within 50 characters only")]
        public string? CustomerName { get; set; }



        [Required(ErrorMessage = "Order Number is required.")]
        public string? OrderNumber { get; set; }


        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a positive number.")]
        public decimal TotalAmount { get; set; }


        public List<OrderItemAddRequest> OrderItems { get; set; } = new List<OrderItemAddRequest>();


        /// <summary>
        /// Converts the <see cref="OrderAddRequest"/> object to an <see cref="Order"/> object.
        /// </summary>
        /// <returns>The converted <see cref="Order"/> object.</returns>
        public Order ToOrder()
        {
            return new Order
            {
                CustomerName = CustomerName,
                OrderNumber = OrderNumber,
                OrderDate = OrderDate,
                TotalAmount = TotalAmount
            };
        }
    }
}
