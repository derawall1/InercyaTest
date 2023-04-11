using Customers.Models;
using Microsoft.EntityFrameworkCore;
using Customers.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(p => p.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<CustomerService>();
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var dbcontext = services.GetRequiredService<DataContext>();
    dbcontext.Database.Migrate();

    var customerService = services.GetRequiredService<CustomerService>();
  var customers = customerService.GetCustomersFromCsv("data//Customers.csv");
    await customerService.SaveCustomers(customers);
    
}
// Configure the HTTP request pipeline.



app.Run();

