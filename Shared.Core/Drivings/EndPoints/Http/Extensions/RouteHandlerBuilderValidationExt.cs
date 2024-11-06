using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.Core.Drivings.EndPoints.Http.Filters;

namespace Shared.Core.Drivings.EndPoints.Http.Extensions;

public static class RouteHandlerBuilderValidationExt
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<RequestValidationFilter<TRequest>>();
    }
}