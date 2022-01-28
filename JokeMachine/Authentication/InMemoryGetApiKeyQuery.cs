using JokeMachine.Models;

namespace JokeMachine.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }

    public class InMemoryGetApiKeyQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;
        private readonly IConfiguration configuration;

        public InMemoryGetApiKeyQuery(IConfiguration configuration)
        {
            List<ApiKey> existingApiKeys = new()
            {
                new ApiKey("default", configuration.GetValue<string>("ApiKey")),
            };
            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
            this.configuration = configuration;
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
