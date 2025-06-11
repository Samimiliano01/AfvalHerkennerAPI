using Sensoring_API.Data;

namespace Sensoring_API.ApiKeyAuth;

public class AdminKeyValidation(LitterDbContext context) : IApiKeyValidation
{
    public async Task<bool> IsValidApiKeyAsync(string userApiKey)
    {
        if (string.IsNullOrEmpty(userApiKey))
        {
            return false;
        }

        //var apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
        var apiKeyFromDb = await context.ApiKey.FindAsync(Util.HashWithSha256(userApiKey));

        if (apiKeyFromDb == null)
        {
            return false;
        }

        return apiKeyFromDb.Role == "Admin";
    }
}