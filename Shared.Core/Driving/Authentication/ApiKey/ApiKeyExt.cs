using Microsoft.AspNetCore.Authentication;

namespace Shared.Core.Driving.Authentication.ApiKey;

public static class ApiKeyExt
{
    public static AuthenticationBuilder AddApiKey(
        this AuthenticationBuilder builder, 
        string schemeName,
        Action<ApiKeyAuthenticationSchemeOptions>? action = null)
    {
        return builder.AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(schemeName, action);
    }
}