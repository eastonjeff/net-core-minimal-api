using net_core_minimal_api.Data.Models;

namespace net_core_minimal_api.Services.Models
{
    public class CustomerDTO
    {
        public CustomerDTO() { }

        public CustomerDTO(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
