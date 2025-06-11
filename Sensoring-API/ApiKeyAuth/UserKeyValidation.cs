using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.ObjectPool;
using Sensoring_API.Data;

namespace Sensoring_API.ApiKeyAuth
{
    public class UserKeyValidation : IApiKeyValidation
    {
        private readonly LitterDbContext _context;

        public UserKeyValidation(LitterDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsValidApiKeyAsync(string userApiKey)
        {
            if (string.IsNullOrEmpty(userApiKey))
            {
                return false;
            }

            //var apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
            var apiKeyFromDb = await _context.ApiKey.FindAsync(userApiKey);

            if (apiKeyFromDb == null)
            {
                return false;
            }

            if (apiKeyFromDb.Role == "Admin" || apiKeyFromDb.Role == "User")
            {
                return true;
            }

            return false;
        }
    }
}
