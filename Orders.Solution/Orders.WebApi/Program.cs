using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;
using Services.OrderItems;
using Services.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<IOrdersAdderService, OrdersAdderService>();
builder.Services.AddScoped<IOrdersDeleterService, OrdersDeleterService>();
builder.Services.AddScoped<IOrdersFilterService, OrdersFilterService>();
builder.Services.AddScoped<IOrdersGetterService, OrdersGetterService>();
builder.Services.AddScoped<IOrdersUpdaterService, OrdersUpdaterService>();
builder.Services.AddScoped<IOrderItemsAdderService, OrderItemsAdderService>();
builder.Services.AddScoped<IOrderItemsDeleterService, OrderItemsDeleterService>();
builder.Services.AddScoped<IOrderItemsGetterService, OrderItemsGetterService>();
builder.Services.AddScoped<IOrderItemsUpdaterService, OrderItemsUpdaterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error"); 
    app.UseHsts(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
