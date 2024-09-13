using Entities;
using Microsoft.EntityFrameworkCore;
using Service;
using ServiceContracts;
using Services;
using Repositories;
using RepositoryContracts;
using StocksApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<FinnhubService>();

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddScoped<IStocksService,StocksService>();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IBuyOrderRepository, BuyOrderRepository>();
builder.Services.AddScoped<ISellOrderRepository, SellOrderRepository>();
builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();

builder.Services.AddDbContext<TradeDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();



if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.Run();

public partial class Program { }