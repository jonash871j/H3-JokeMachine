using JokeMachine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JokeMachine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecretJokeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public Joke Get()
        {
            return new Joke
            {
                Question = "Hvordan får man en fisk til at grine?",
                Answer = "Man putter den i kildevand"
            };
        }
    }
}
