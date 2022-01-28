using JokeMachine.Models;

namespace JokeMachine.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }

    public class InMemoryGetApiKeyQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> apiKeys;

        public InMemoryGetApiKeyQuery(IConfiguration configuration)
        {
            List<ApiKey> existingApiKeys = new()
            {
                new ApiKey("default", configuration.GetValue<string>("ApiKey")),
            };
            apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
