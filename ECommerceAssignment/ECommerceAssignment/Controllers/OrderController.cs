using ECommerceAssignment.CustomValidators;
using Microsoft.AspNetCore.Mvc;
using ECommerceAssignment.Models;

namespace ECommerceAssignment.Controllers
{
    public class OrderController : Controller
    {
        [Route("/Order")]
        public IActionResult Index([Bind(nameof(Order.InvoicePrice), nameof(Order.Products), nameof(Order.OrderDate))] Order order)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                errorList = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();

                string errors = string.Join("\n", errorList);
                return BadRequest(errors);
            }

            Random random = new Random();
            int randomNumber = random.Next(1, 99999);

            return Json(new { OrderNo = randomNumber });

        }
    }
}
