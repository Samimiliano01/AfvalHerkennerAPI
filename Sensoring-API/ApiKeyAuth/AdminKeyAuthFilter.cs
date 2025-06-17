using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sensoring_API.Data;

namespace Sensoring_API.ApiKeyAuth
{
    public class AdminKeyAuthFilter : IAuthorizationFilter
    {
        private readonly AdminKeyValidation _adminKeyValidation;

        public AdminKeyAuthFilter(AdminKeyValidation apiKeyValidation)
        {
            _adminKeyValidation = apiKeyValidation;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];
            if (string.IsNullOrEmpty(userApiKey))
            {
                context.Result = new UnauthorizedResult();

                return;
            }

            if (!_adminKeyValidation.IsValidApiKeyAsync(userApiKey).Result)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

