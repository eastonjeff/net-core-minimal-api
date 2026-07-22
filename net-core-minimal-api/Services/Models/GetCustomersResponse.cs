using net_core_minimal_api.Data.Models;
using System.Text.Json.Serialization;

namespace net_core_minimal_api.Services.Models
{
    public class GetCustomersResponse(int total, IReadOnlyCollection<Customer> customers)
    {
        [JsonPropertyName("customers")]
        public IReadOnlyCollection<CustomerDTO> Customers { get; set; } = [.. customers.Select(x => new CustomerDTO(x))];

        [JsonPropertyName("total")]
        public int Total { get; set; } = total;
    }
}
