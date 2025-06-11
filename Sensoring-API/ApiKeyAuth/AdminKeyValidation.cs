using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.ObjectPool;
using Sensoring_API.Data;
using Sensoring_API.Util; 

namespace Sensoring_API.ApiKeyAuth
{
    public class AdminKeyValidation : IApiKeyValidation
    {
        private readonly LitterDbContext _context;

        public AdminKeyValidation(LitterDbContext context)
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
            var apiKeyFromDb = await _context.ApiKey.FindAsync(Util.HashWithSHA256(userApiKey));

            if (apiKeyFromDb == null)
            {
                return false;
            }

            if(apiKeyFromDb.Role == "Admin")
            {
                return true;
            }

            return false;
        }
    }
}
