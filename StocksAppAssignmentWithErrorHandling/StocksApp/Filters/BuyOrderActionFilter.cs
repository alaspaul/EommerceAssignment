using Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO.BuyOrderDTO;
using StocksApp.Controllers;
using StocksApp.Models;

namespace StocksApp.Filters
{
    public class BuyOrderActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController)
            {
                if (!tradeController.ModelState.IsValid)
                {
                    BuyOrderRequest buyOrderRequest = (BuyOrderRequest)context.ActionArguments["buyOrderRequest"];
                    tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    StockTrade stockTrade =
                        new StockTrade()
                        {
                            StockName = buyOrderRequest.StockName,
                            Quantity = (int)buyOrderRequest.Quantity,
                            StockSymbol = buyOrderRequest.StockSymbol
                        };
                    context.Result = tradeController.View("Index", stockTrade);

                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
