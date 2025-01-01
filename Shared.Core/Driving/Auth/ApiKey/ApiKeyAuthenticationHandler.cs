using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Shared.Core.Driving.Auth.ApiKey;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IConfiguration configuration)
    : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> _options = options;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKeyHeaderName = _options.CurrentValue.InHeaderName;
        if (string.IsNullOrWhiteSpace(apiKeyHeaderName))
        {
            // TODO add logging
            throw new Exception("apiKeyHeaderName must be provided in appsettings"); // TODO find a better exception
        }

        var actualApiKey = Request.Headers[apiKeyHeaderName];
        if (string.IsNullOrWhiteSpace(actualApiKey))
        {
            // TODO add logging
            throw new Exception("ApiKey must be provided in appsettings"); // TODO find a better exception
        }
        
        var expectedApiKey = configuration[$"Authentication:Schemes:{Scheme.Name}:{_options.CurrentValue.InAppSettingsName}"];
        
        if (actualApiKey != expectedApiKey)
            // TODO consider the response
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
        
        var claims = new[] { new Claim(ClaimTypes.Name, apiKeyHeaderName) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        // TODO consider the response
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}