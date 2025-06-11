using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sensoring_API.ApiKeyAuth
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidation _apiKeyValidation;

        public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];
            if (string.IsNullOrEmpty(userApiKey))
            {
                context.Result = new BadRequestResult();

                return;
            }

            if (!_apiKeyValidation.IsValidApiKey(userApiKey)) 
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
