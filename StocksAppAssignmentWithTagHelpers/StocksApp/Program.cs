using Service;
using ServiceContracts;
using StocksApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<FinnhubService>();

builder.Services.AddSingleton<IStocksService,StocksService>();
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.Run();
