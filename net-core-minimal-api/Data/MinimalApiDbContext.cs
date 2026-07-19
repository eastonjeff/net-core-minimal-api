using Microsoft.EntityFrameworkCore;

namespace net_core_minimal_api.Data
{
    public class MinimalApiDbContext(DbContextOptions<MinimalApiDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers => Set<Customer>();
    }
}
