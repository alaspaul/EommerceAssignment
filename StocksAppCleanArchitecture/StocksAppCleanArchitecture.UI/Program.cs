using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using StocksApp;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using StocksApp.Middleware;
using ServiceContracts.FinnhubServiceContracts;
using ServiceContracts.StocksServiceContracts;
using ServiceContracts;
using StocksAppCleanArchitecture.Core.Services.StocksService;
using StocksAppCleanArchitecture.Core.Services.FinnhubService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Host.UseSerilog((HostBuilderContext context,
                        IServiceProvider services,
                        LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            //read config settings from built in Iconfiguration
            .ReadFrom.Services(services);
    //read out current apps services
});
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddScoped<IBuyOrderService,BuyOrderService>();
builder.Services.AddScoped<ISellOrderService, SellOrderService>();

builder.Services.AddScoped<IFinnhubServiceCompanyProfileService, FinnhubServiceCompanyProfileService>();
builder.Services.AddScoped<IFinnhubServiceSearchStocksService, FinnhubServiceSearchStocksService>();
builder.Services.AddScoped<IFinnhubServiceStockPriceQouteService, FinnhubServiceStockPriceQouteService>();
builder.Services.AddScoped<IFinnhubServiceStockService, FinnhubServiceStockService>();

builder.Services.AddScoped<IBuyOrderRepository, BuyOrderRepository>();
builder.Services.AddScoped<ISellOrderRepository, SellOrderRepository>();
builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();



builder.Services.AddDbContext<TradeDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseCustomMiddleware();
}


app.UseSerilogRequestLogging();
if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.Run();

public partial class Program { }