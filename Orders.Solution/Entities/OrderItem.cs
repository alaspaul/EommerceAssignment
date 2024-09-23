using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemID { get; set; }

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

        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }
    }
}
