using Microsoft.AspNetCore.Authentication;

namespace Shared.Core.Driving.Authentication.ApiKey;

public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public string InHeaderName { get; set; } = "x-api-key";
    public string InAppSettingsName { get; set; } = "ApiKey";
}