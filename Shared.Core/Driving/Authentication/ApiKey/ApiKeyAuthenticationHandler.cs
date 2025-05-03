using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Shared.Core.Driving.Authentication.ApiKey;

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
            Logger.LogError("");
            throw new ArgumentNullException($"{nameof(apiKeyHeaderName)} is missing, please provide it in the configuration");
        }
    
        var actualApiKey = Request.Headers[apiKeyHeaderName];
        var expectedApiKey = configuration[$"Authentication:Schemes:{Scheme.Name}:{_options.CurrentValue.InAppSettingsName}"];
        if (string.IsNullOrWhiteSpace(expectedApiKey))
        {
            // TODO add logging
            throw new ArgumentNullException($"{nameof(expectedApiKey)} is missing, please provide it in the configuration");
        }
        
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