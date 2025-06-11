namespace Sensoring_API.ApiKeyAuth
{
    public interface IApiKeyValidation
    {
        Task<bool> IsValidApiKeyAsync(string userApiKey);
    }
}
