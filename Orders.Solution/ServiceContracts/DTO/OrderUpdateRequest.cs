using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{

    public class OrderUpdateRequest
    {

        public Guid OrderID { get; set; }


        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(50, ErrorMessage = "Name should be within 50 characters only")]
        public string? CustomerName { get; set; }



        [Required(ErrorMessage = "The OrderNumber field is required.")]
        public string? OrderNumber { get; set; }


        [Required(ErrorMessage = "Date is Required")]
        public DateTime OrderDate { get; set; }



        [Required(ErrorMessage = "Total amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount should be a Positive number")]
        public decimal TotalAmount { get; set; }


        /// <summary>
        /// Maps the data from the current instance of <see cref="OrderUpdateRequest"/> to an instance of <see cref="Order"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Order"/> with the mapped data.</returns>
        public Order ToOrder()
        {
            return new Order
            {
                OrderID = OrderID,
                CustomerName = CustomerName,
                OrderNumber = OrderNumber,
                OrderDate = OrderDate,
                TotalAmount = TotalAmount,
            };
        }
    }
}
