namespace JokeMachine.Models
{
    public class Joke
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public JokeCategorie Categorie { get; set; }
    }
}
