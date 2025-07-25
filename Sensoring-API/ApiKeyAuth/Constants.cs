﻿namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// A static class containing constants used for API key authentication in the Sensoring API project.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Represents the name of the HTTP request header used to transmit the API key for authentication.
    /// This constant is used in API authentication filters to retrieve the API key value from request headers.
    /// </summary>
    public const string ApiKeyHeaderName = "X-API-Key";
}