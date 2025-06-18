
using Sensoring_API.Data;

namespace Sensoring_API.ApiKeyAuth;

/// <summary>
/// Validates user API keys by checking their existence and role in the database.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IApiKeyValidation"/> interface to provide
/// functionality for validating API keys specific to users. It leverages the database
/// context to retrieve and verify the API key and its associated role.
/// </remarks>
/// <param name="context">The database context instance used to access API key data.</param>
public class UserKeyValidation(LitterDbContext context) : IApiKeyValidation
{
    /// <summary>
    /// Validates the provided API key by checking its existence and associated role in the database.
    /// </summary>
    /// <param name="userApiKey">The API key provided by the user for validation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the API key is valid and belongs
    /// to a user with a role of "Admin" or "User"; otherwise, false.
    /// </returns>
    public async Task<bool> IsValidApiKeyAsync(string userApiKey)
    {
        if (string.IsNullOrEmpty(userApiKey))
        {
            return false;
        }

        var apiKeyFromDb = await context.ApiKey.FindAsync(Util.HashWithSha256(userApiKey));

        return apiKeyFromDb?.Role is "Admin" or "User";
    }
}