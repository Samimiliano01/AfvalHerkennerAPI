using Sensoring_API.Data;

namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// A class that validates API keys for administrators by verifying their existence and role in the database.
/// </summary>
/// <remarks>
/// The AdminKeyValidation class implements the IApiKeyValidation interface to provide a method for validating
/// administrative API keys using a LitterDbContext instance to interact with the database.
/// It checks the key's existence and ensures its associated role is "Admin".
/// </remarks>
/// <example>
/// This class is designed to be used in services where API key authentication and role-based validation for
/// administrators are required.
/// </example>
/// <param name="context">
/// Instance of LitterDbContext used to access the database for validating the API key.
/// </param>
/// <seealso cref="IApiKeyValidation" />
public class AdminKeyValidation(LitterDbContext context) : IApiKeyValidation
{
    /// <summary>
    /// Validates whether the provided API key corresponds to an admin role in the database.
    /// </summary>
    /// <param name="userApiKey">The API key provided by the user for validation.</param>
    /// <returns>A task that represents the asynchronous validation operation. The task result contains
    /// a boolean value indicating whether the API key is valid and belongs to an admin user.</returns>
    public async Task<bool> IsValidApiKeyAsync(string userApiKey)
    {
        if (string.IsNullOrEmpty(userApiKey))
        {
            return false;
        }

        var apiKeyFromDb = await context.ApiKey.FindAsync(Util.HashWithSha256(userApiKey));

        if (apiKeyFromDb == null)
        {
            return false;
        }

        return apiKeyFromDb.Role == "Admin";
    }
}