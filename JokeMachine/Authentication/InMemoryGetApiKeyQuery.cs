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

        public InMemoryGetApiKeyQuery()
        {
            List<ApiKey> existingApiKeys = new()
            {
                new ApiKey(1, "default", "C5BFF7F0-B4DF-475E-A331-F737424F013C", new DateTime(2022, 01, 27), new List<string>()),
            };
            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
