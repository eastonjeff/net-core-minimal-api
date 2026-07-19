using FluentValidation;

namespace net_core_minimal_api.Services.Validators
{
    // A single generic filter that handles validation for an endpoint, given an IValidator type
    public class ValidationFilter<T>(IValidator<T>? validator = null) : IEndpointFilter where T : class
    {
        private readonly IValidator<T>? _validator = validator;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            if (_validator is not null)
            {
                var argument = context.Arguments.OfType<T>().FirstOrDefault();
                if (argument is not null)
                {
                    var validationResult = await _validator.ValidateAsync(argument);
                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }
                }
            }
            return await next(context);
        }
    }
}
