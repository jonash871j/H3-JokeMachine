using JokeLib.Models;
using Newtonsoft.Json;

namespace JokeMachine.Services
{
    public interface IJokeService
    {
        List<Joke> GetJokes(JokeCategorie jokeCategorie);
    }

    public class JokeService : IJokeService
    {
        private readonly ILogger<JokeService> logger;
        private List<Joke> jokes = new ();

        public JokeService(ILogger<JokeService> logger)
        {
            this.logger = logger;

            LoadJokes();
        }

        public List<Joke> GetJokes(JokeCategorie jokeCategorie)
        {
            return jokes.FindAll(j => j.Categorie == jokeCategorie);
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
