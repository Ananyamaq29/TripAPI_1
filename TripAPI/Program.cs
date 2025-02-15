using Microsoft.EntityFrameworkCore;
using TripAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TripContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use SQL Server and the connection string from appsettings.json

builder.Services.AddControllers(); // Add controllers to the services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed error pages in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HTTPS for production
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

app.Run(); // Start the application