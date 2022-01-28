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
        private readonly ILogger<JokeService> logger;
        private readonly Random random = new();
        private List<Joke> jokes = new ();

        public JokeService(ILogger<JokeService> logger)
        {
            this.logger = logger;

            LoadJokes();
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
            return jokes.FindAll(j => j.Categorie == jokeCategorie && j.Language == language);
        }

        private void LoadJokes()
        {
            try
            {
                string jsonData = File.ReadAllText("Jokes.json");
                jokes = JsonConvert.DeserializeObject<List<Joke>>(jsonData) ?? new List<Joke>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load jokes");
            }
        }
    }
}
