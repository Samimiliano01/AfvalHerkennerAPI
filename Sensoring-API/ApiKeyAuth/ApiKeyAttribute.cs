using Microsoft.AspNetCore.Mvc;

namespace Sensoring_API.ApiKeyAuth
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            :base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
