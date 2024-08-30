using Microsoft.AspNetCore.Mvc;
using BankPractice.Models;
namespace BankPractice.Controllers
{
    public class BankController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to Bank");
        }

        [Route("/account-details")]

        public IActionResult Details()
        {
            AccountDetails account = new AccountDetails()
            {
                AccountNumber = 1001,
                AccountHolderName = "Example Name",
                balance = 5000,
            };

            return Json(account);
        }

        [Route("/account-statement")]

        public IActionResult Statement()
        {
            return File("sample.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{Id}")]
        public IActionResult GetBalance()
        {
    
            string? IdString = Convert.ToString(HttpContext.Request.RouteValues["Id"]);

            if (string.IsNullOrEmpty(IdString))
            {
                return NotFound("Account Number should be supplied");
            }

            if (int.TryParse(IdString, out int Id))
            {

                AccountDetails account = new AccountDetails()
                {
                    AccountNumber = 1001,
                    AccountHolderName = "ExampleName",
                    balance = 5000,
                };

                if (Id == 1001)
                {
                    return Content($"balance for account {Id} is {account.balance}");
                }
                else
                {
                    return BadRequest("Account Number should be 1001");
                }

            }
            else
            {
                return BadRequest("Invalid Account Number format");
            }
        }
            
    }
}
