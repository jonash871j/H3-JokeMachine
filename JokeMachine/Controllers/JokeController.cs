using JokeMachine.Models;
using JokeMachine.Services;
using JokeMachine.Utility;
using Microsoft.AspNetCore.Authorization;
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
            JokeCategorie jokeCategorie = HttpContext.Session.GetObjectFromJson<JokeCategorie?>("JokeCategorie")
                ?? JokeCategorie.Normal;
            string language = Request.HttpContext.Features.Get<IRequestCultureFeature>()
                ?.RequestCulture?.Culture?.TwoLetterISOLanguageName ?? "en";
            List<Joke> oldJokes = HttpContext.Session.GetObjectFromJson<List<Joke>>("OldJokes")
                ?? new List<Joke>();

            Joke? joke = jokeService.GetJoke(jokeCategorie, language, oldJokes);
            if (joke == null)
            {
                return null;
            }
            oldJokes.Add(joke);
            HttpContext.Session.SetObjectAsJson("OldJokes", oldJokes);
            return joke;
        }
    }
}
