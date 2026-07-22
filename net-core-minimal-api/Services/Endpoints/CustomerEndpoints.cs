using net_core_minimal_api.Services.Models;
using net_core_minimal_api.Services.Repositories;
using net_core_minimal_api.Services.Validators;

namespace net_core_minimal_api.Services.Endpoints
{
    public static class CustomerEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/customers", GetCustomersAsync)
                .AddEndpointFilter<ValidationFilter<GetCustomersQuery>>()
                .WithName("GetCustomers")
                .Produces<GetCustomersResponse>();

            return app;
        }

        public static async Task<IResult> GetCustomersAsync([AsParameters] GetCustomersQuery query, ICustomerRepository customerRepository)
        {
            var customers = await customerRepository.GetCustomersAsync(query);
            var response = new GetCustomersResponse(customers.Total, customers.Customers);
            return Results.Ok(response);
        }
    }
}
