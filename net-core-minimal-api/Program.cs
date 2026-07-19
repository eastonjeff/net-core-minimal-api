using FluentValidation;
using Microsoft.EntityFrameworkCore;
using net_core_minimal_api.Data;
using net_core_minimal_api.Services;
using net_core_minimal_api.Services.Models;
using net_core_minimal_api.Services.Validators;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register services for DI

// DB
builder.Services.AddDbContext<MinimalApiDbContext>(options =>
    options.UseNpgsql(connectionString));

// Validators
// ValidationFilters are constructed with the IValidator<T> interface
builder.Services.AddScoped<IValidator<GetCustomersQuery>, GetCustomersQueryValidator>();

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //using this over Swagger
    // Accessible at /scalar
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();


app.MapGet("/customers", async ([AsParameters] GetCustomersQuery query, ICustomerRepository customerRepository) =>
{
    var customers = await customerRepository.GetCustomersAsync(query);
    var dtoCustomers = customers.Select(x => new CustomerDto()
    {
        Id = x.Id,
        FirstName = x.FirstName,
        LastName = x.LastName
    });
    return Results.Ok(dtoCustomers);
})
.AddEndpointFilter<ValidationFilter<GetCustomersQuery>>()
.WithName("GetCustomers");

app.Run();
