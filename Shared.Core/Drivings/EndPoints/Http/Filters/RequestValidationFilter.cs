using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Core.Drivings.Models;

namespace Shared.Core.Drivings.EndPoints.Http.Filters;

public class RequestValidationFilter<TRequest>(IValidator<TRequest>? validator = null) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (validator is null)
            return await next(context);

        var request = context.Arguments.OfType<TRequest>().First();
        
        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (!validationResult.IsValid)
            return Results.BadRequest(ErrorResponse.ToErrorResponse(validationResult));

        return await next(context);
    }
}