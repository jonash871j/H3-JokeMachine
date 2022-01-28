using JokeMachine.Models;
using Newtonsoft.Json;

namespace JokeMachine.Services
{
    public interface IJokeService
    {
        Joke? GetJoke(JokeCategorie jokeCategorie, string language, List<Joke> oldJokes);
    }

    public class JokeService : IJokeService
    {
        private static readonly Random random = new();

        private readonly ILogger<JokeService> logger;
        private readonly IConfiguration configuration;

        public JokeService(ILogger<JokeService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public Joke? GetJoke(JokeCategorie jokeCategorie, string language, List<Joke> oldJokes)
        {
            List<Joke> jokes = GetJokes(jokeCategorie, language);
            
            // Removes jokes there is allready seen
            jokes.RemoveAll(j1 => oldJokes.Any(j2 => j1.Question == j2.Question));

            // Returns random joke
            return jokes.Any() ? jokes[random.Next(jokes.Count)] : null;
        }

        private List<Joke> GetJokes(JokeCategorie jokeCategorie, string language)
        {
            // Loads jokes from file
            List<Joke> jokes = LoadJokes();

            // Finds all jokes base on categorie and langauge
            return jokes.FindAll(j => j.Categorie == jokeCategorie && j.Language == language);
        }

        private List<Joke> LoadJokes()
        {
            try
            {
                // Reads jokes from json file
                string jsonData = File.ReadAllText(configuration.GetValue<string>("JokesJsonPath"));

                // Deserializes jokes to list of jokes
                return JsonConvert.DeserializeObject<List<Joke>>(jsonData) ?? new List<Joke>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load jokes");
                return new List<Joke>();
            }
        }
    }
}
