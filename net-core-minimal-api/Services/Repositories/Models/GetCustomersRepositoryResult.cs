using net_core_minimal_api.Data.Models;

namespace net_core_minimal_api.Services.Repositories.Models
{
    public class GetCustomersRepositoryResult(int total, IReadOnlyCollection<Customer> customers)
    {
        public IReadOnlyCollection<Customer> Customers { get; set; } = customers;
        public int Total { get; set; } = total;
    }
}
