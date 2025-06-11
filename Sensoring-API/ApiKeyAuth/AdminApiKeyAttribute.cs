using Microsoft.AspNetCore.Mvc;

namespace Sensoring_API.ApiKeyAuth
{
    public class AdminApiKeyAttribute : ServiceFilterAttribute
    {
        public AdminApiKeyAttribute()
            : base(typeof(AdminKeyAuthFilter))
        {
        }
    }
}

