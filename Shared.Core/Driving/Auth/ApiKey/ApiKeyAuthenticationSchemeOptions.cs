using Microsoft.AspNetCore.Authentication;

namespace Shared.Core.Driving.Auth.ApiKey;

public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public string InHeaderName { get; set; } = "x-api-key";
    public string InAppSettingsName { get; set; } = "ApiKey";
}