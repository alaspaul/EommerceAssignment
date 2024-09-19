using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using StocksApp.Controllers;
using StocksApp.Models;

namespace StocksApp.Filters
{
    public class SellOrderActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController)
            {
                if (!tradeController.ModelState.IsValid)
                {
                    SellOrderRequest sellOrderRequest = (SellOrderRequest)context.ActionArguments["sellOrderRequest"];
                    tradeController.ViewBag.Errors = 
                        tradeController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    StockTrade stockTrade = new StockTrade() 
                            {   StockName = sellOrderRequest.StockName,
                                Quantity = (int)sellOrderRequest.Quantity, 
                                StockSymbol = sellOrderRequest.StockSymbol };

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
