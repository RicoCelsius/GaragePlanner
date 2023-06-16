using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core;
using DAL;
using Domain;
using Domain.interfaces;
using GaragePlanner.Controllers;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped(sp => new DbConnection(connectionString));

builder.Services.AddScoped<AppointmentCollection>();
builder.Services.AddScoped<AppointmentDal>();
builder.Services.AddScoped<IAppointmentDal, AppointmentDal>();

builder.Services.AddScoped<CarCollection>();
builder.Services.AddScoped<CarDal>();
builder.Services.AddScoped<ICarDal, CarDal>();

builder.Services.AddScoped<CustomerCollection>();
builder.Services.AddScoped<CustomerDal>();
builder.Services.AddScoped<ICustomerDal, CustomerDal>();







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
