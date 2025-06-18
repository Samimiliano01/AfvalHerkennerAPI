using Microsoft.AspNetCore.Mvc;

namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// Attribute used to enforce API key-based authentication for user-level access in ASP.NET Core.
/// </summary>
/// <remarks>
/// This attribute applies the <see cref="UserKeyAuthFilter"/> to the decorated controller
/// actions or controllers. It ensures that requests are authorized based on an API key provided
/// in the request header. The validation of the API key is handled by the logic defined within
/// the <see cref="UserKeyAuthFilter"/> implementation. If the API key is missing or invalid,
/// the request will be denied with an HTTP 401 Unauthorized response.
/// </remarks>
/// <example>
/// Apply this attribute to ASP.NET Core controller actions to enforce user-specific API key
/// authentication. The attribute utilizes dependency injection to resolve the required filter
/// and validation mechanisms.
/// </example>
public class UserApiKeyAttribute() : ServiceFilterAttribute(typeof(UserKeyAuthFilter));