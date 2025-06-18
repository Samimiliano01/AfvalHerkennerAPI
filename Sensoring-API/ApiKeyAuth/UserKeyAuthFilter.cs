using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// Implements an API key-based authorization filter for user-level validation.
/// </summary>
/// <remarks>
/// This class checks whether a valid API key is provided in the request header specified by the
/// constant <c>ApiKeyHeaderName</c>. If the API key is missing or invalid, the request is rejected
/// with an HTTP 401 Unauthorized response. The validation of the API key is delegated to an
/// <see cref="IApiKeyValidation" /> implementation.
/// </remarks>
/// <example>
/// This filter should be registered in the service container and used within controllers
/// to ensure user-specific API key authentication.
/// </example>
public class UserKeyAuthFilter(IApiKeyValidation apiKeyValidation) : IAuthorizationFilter
{
    /// <summary>
    /// Executes the authorization logic by validating the API key from the request headers.
    /// </summary>
    /// <param name="context">
    /// The <see cref="AuthorizationFilterContext"/> containing the HTTP context and filter metadata,
    /// used to perform the authorization logic. If the API key is missing or invalid, the context result
    /// is set to <see cref="UnauthorizedResult"/>.
    /// </param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];
        if (string.IsNullOrEmpty(userApiKey))
        {
            context.Result = new UnauthorizedResult();

            return;
        }

        if (!apiKeyValidation.IsValidApiKeyAsync(userApiKey).Result)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}