using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// Authorization filter for validating administrative API keys.
/// </summary>
/// <remarks>
/// This filter checks for the presence and validity of an API key in the request headers.
/// If no API key is provided or the provided API key is invalid, the filter returns an UnauthorizedResult.
/// </remarks>
/// <example>
/// To use this filter, decorate a controller or action with the <see cref="AdminApiKeyAttribute"/>.
/// The filter depends on an implementation of the <see cref="IApiKeyValidation"/> interface to validate the provided API key.
/// </example>
/// <seealso cref="IApiKeyValidation"/>
/// <seealso cref="AdminApiKeyAttribute"/>
public class AdminKeyAuthFilter(AdminKeyValidation apiKeyValidation) : IAuthorizationFilter
{
    /// <summary>
    /// Invoked to perform authorization logic for incoming requests.
    /// </summary>
    /// <param name="context">
    /// The filter context which provides access to HTTP-specific information for authorization, such as request headers.
    /// </param>
    /// <remarks>
    /// This method validates the presence and correctness of an API key in the request headers.
    /// If the API key is absent or invalid, an unauthorized response is returned.
    /// </remarks>
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
