using Microsoft.AspNetCore.Mvc;

namespace Sensoring_API.ApiKeyAuth
{
    public class UserApiKeyAttribute : ServiceFilterAttribute
    {
        public UserApiKeyAttribute()
            :base(typeof(UserKeyAuthFilter))
        {
        }
    }
}
