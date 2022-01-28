using System.Text.Json.Serialization;

namespace JokeMachine.Models
{
    public class Joke
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }

        [JsonIgnore]
        public JokeCategorie Categorie { get; set; }

        [JsonIgnore]
        public string? Language { get; set; }
    }
}
