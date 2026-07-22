using net_core_minimal_api.Data.Models;
using net_core_minimal_api.Services.Models;
using net_core_minimal_api.Services.Repositories.Models;

namespace net_core_minimal_api.Services.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<Customer> GetAsync(int id);
        Task<GetCustomersRepositoryResult> GetCustomersAsync(GetCustomersQuery input);
    }
}
