namespace net_core_minimal_api.Services.Models
{
    public class GetCustomersQuery
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
