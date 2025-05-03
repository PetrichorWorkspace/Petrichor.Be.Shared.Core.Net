using System.Security.Claims;

namespace Shared.Core.Driven.Security;

public static class ClaimsPrincipalExt
{
    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }
}