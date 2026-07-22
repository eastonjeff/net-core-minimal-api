using Microsoft.EntityFrameworkCore;
using net_core_minimal_api.Data;
using net_core_minimal_api.Data.Models;
using net_core_minimal_api.Services.Models;
using net_core_minimal_api.Services.Repositories.Models;

namespace net_core_minimal_api.Services.Repositories
{
    public class CustomerRepository(MinimalApiDbContext dbContext) : ICustomerRepository
    {
        public Task<Customer> CreateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCustomersRepositoryResult> GetCustomersAsync(GetCustomersQuery query)
        {
            var dbQuery = dbContext
                .Customers
                .AsNoTracking()
                .AsQueryable();

            if (query.Id != null)
            {
                dbQuery = dbQuery.Where(x => x.Id == query.Id);
            }

            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                dbQuery = dbQuery.Where(x => x.FirstName == query.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                dbQuery = dbQuery.Where(x => x.LastName == query.LastName);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;

            var total = await dbQuery.CountAsync();

            dbQuery = dbQuery
              .OrderBy(x => x.Id)
              .Skip(skip)
              .Take(query.PageSize);

            var customers = await dbQuery.ToListAsync();

            return new GetCustomersRepositoryResult(total, customers.AsReadOnly<Customer>());

            
        }

        public Task<Customer> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
