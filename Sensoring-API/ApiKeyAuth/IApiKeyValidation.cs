namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// Represents a contract for validating API keys in the application.
/// </summary>
/// <remarks>
/// Implementations of this interface provide mechanisms to validate API keys.
/// This can be used to authenticate requests by verifying the provided API keys against
/// predefined rules or a data store.
/// </remarks>
public interface IApiKeyValidation
{
    /// <summary>
    /// Asynchronously validates the provided API key to ensure it corresponds to a valid user or admin.
    /// </summary>
    /// <param name="userApiKey">The API key provided by the user for authentication.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the API key is valid.</returns>
    Task<bool> IsValidApiKeyAsync(string userApiKey);
}