using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sensoring_API.Data;

namespace Sensoring_API.ApiKeyAuth
{
    public class UserKeyAuthFilter : IAuthorizationFilter
    {
        private readonly UserKeyValidation _apiKeyValidation;

        public UserKeyAuthFilter(UserKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];
            if (string.IsNullOrEmpty(userApiKey))
            {
                context.Result = new UnauthorizedResult();

                return;
            }

            if (!_apiKeyValidation.IsValidApiKeyAsync(userApiKey).Result)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
