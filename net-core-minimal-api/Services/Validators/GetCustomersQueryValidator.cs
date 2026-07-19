using FluentValidation;
using net_core_minimal_api.Services.Models;

namespace net_core_minimal_api.Services.Validators
{
    public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
    {
        public GetCustomersQueryValidator()
        {
            // Page index must be 1 or higher
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be at least 1.");

            // Prevent users from requesting 10,000 items at once and crashing the DB
            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");

            // Name search length
            RuleFor(x => x.FirstName)
                .MaximumLength(50)
                .WithMessage("FirstName term cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .WithMessage("LastName term cannot exceed 50 characters.");
        }
    }
}
