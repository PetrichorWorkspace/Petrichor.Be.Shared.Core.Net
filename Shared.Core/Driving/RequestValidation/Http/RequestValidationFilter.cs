using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Core.Driving.Models;

namespace Shared.Core.Driving.RequestValidation.Http;

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