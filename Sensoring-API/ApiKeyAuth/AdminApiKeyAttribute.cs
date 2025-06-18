using Microsoft.AspNetCore.Mvc;

namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// Attribute to enforce the usage of administrative API key authorization for a controller action or class.
/// </summary>
/// <remarks>
/// This attribute applies an <see cref="AdminKeyAuthFilter"/> to the decorated target.
/// The filter ensures that requests are authenticated using a valid administrative API key.
/// If the API key is missing or invalid, the request is denied with an Unauthorized response.
/// </remarks>
/// <seealso cref="AdminKeyAuthFilter"/>
/// <seealso cref="IApiKeyValidation"/>
public class AdminApiKeyAttribute() : ServiceFilterAttribute(typeof(AdminKeyAuthFilter));