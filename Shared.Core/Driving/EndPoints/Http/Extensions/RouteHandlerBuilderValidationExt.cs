using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.Core.Driving.EndPoints.Http.Filters;

namespace Shared.Core.Driving.EndPoints.Http.Extensions;

public static class RouteHandlerBuilderValidationExt
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<RequestValidationFilter<TRequest>>();
    }
}