using Microsoft.AspNetCore.Routing;

namespace Shared.Core.Driving.EndPoints;

public interface IEndPoint
{
    // NOTE Maybe this should be WebApplication instead of IEndpointRouteBuilder
    static abstract void Map(IEndpointRouteBuilder app);
}