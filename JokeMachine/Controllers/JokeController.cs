using JokeMachine.Models;
using JokeMachine.Services;
using JokeMachine.Utility;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace JokeMachine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JokeController : ControllerBase
    {
        private readonly IJokeService jokeService;

        public JokeController(IJokeService jokeService)
        {
            this.jokeService = jokeService;
        }

        [HttpGet]
        public Joke? Get()
        {
            // Gets jokes categorie from session
            JokeCategorie jokeCategorie = HttpContext.Session.GetObjectFromJson<JokeCategorie?>("JokeCategorie")
                ?? JokeCategorie.Normal;

            // Gets language from request
            string language = Request.HttpContext.Features.Get<IRequestCultureFeature>()
                ?.RequestCulture?.Culture?.TwoLetterISOLanguageName ?? "en";

            // Gets old jokes from session
            List<Joke> oldJokes = HttpContext.Session.GetObjectFromJson<List<Joke>>("OldJokes")
                ?? new List<Joke>();

            // Get new unique joke based on filters 
            Joke? joke = jokeService.GetJoke(jokeCategorie, language, oldJokes);
            if (joke == null)
            {
                return null;
            }

            // Adds this new joke to old joke list and updates the session
            oldJokes.Add(joke);
            HttpContext.Session.SetObjectAsJson("OldJokes", oldJokes);
            
            return joke;
        }
    }
}
