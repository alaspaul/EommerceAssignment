using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }

        [Required(ErrorMessage = "The OrderNumber field is required.")]
        public string? OrderNumber { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(50, ErrorMessage = "Name should be within 50 characters only")]
        public string? CustomerName { get; set; }


        [Required(ErrorMessage = "Date is Required")]
        public DateTime OrderDate { get; set; }


        [Required(ErrorMessage = "Total amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount should be a Positive number")]
        public decimal TotalAmount { get; set; } 
    }
}
