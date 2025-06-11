namespace Sensoring_API.ApiKeyAuth
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
