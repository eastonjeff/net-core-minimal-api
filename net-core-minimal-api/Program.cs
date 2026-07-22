using FluentValidation;
using Microsoft.EntityFrameworkCore;
using net_core_minimal_api.Data;
using net_core_minimal_api.Services.BuilderExtensions;
using net_core_minimal_api.Services.Endpoints;
using net_core_minimal_api.Services.Models;
using net_core_minimal_api.Services.Repositories;
using net_core_minimal_api.Services.Validators;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register services
// DB
builder.Services.AddDbContext<MinimalApiDbContext>(options =>
    options.UseNpgsql(connectionString));

// Validators
// ValidationFilters are constructed with the IValidator<T> interface
builder.Services.AddScoped<IValidator<GetCustomersQuery>, GetCustomersQueryValidator>();

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddDevCorsPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(DevCorsExtension.PolicyName);
    app.MapOpenApi();
    // Using this over swagger or default endpoint explorer
    // Accessible at /scalar
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();

//See Services/Endpoints for entity based routes & handlers
app.MapUserEndpoints();

app.Run();
