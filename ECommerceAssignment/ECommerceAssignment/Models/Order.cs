using System.ComponentModel.DataAnnotations;
using ECommerceAssignment.CustomValidators;

namespace ECommerceAssignment.Models
{
    public class Order
    {
        public int? OrderNo { get; set; }

        [Required]
        [MinimumDateValidator("2000-01-01")]
        public DateTime OrderDate { get; set; }

        [Required]
        [InvoicePriceValidator]
        public double InvoicePrice { get; set; }

        [Required]
        [ProductCountValidator]
        public List<Product> Products { get; set; }    
    }
}
