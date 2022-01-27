using JokeLib.Models;
using JokeMachine.Services;
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
        public IEnumerable<Joke> Get(JokeCategorie jokeCategorie)
        {
            return jokeService.GetJokes(jokeCategorie);
        }
    }
}
