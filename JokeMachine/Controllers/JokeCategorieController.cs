﻿using JokeMachine.Models;
using JokeMachine.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JokeMachine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JokeCategorieController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enum.GetNames(typeof(JokeCategorie));
        }

        [HttpPost]
        public void Post(string jokeCategorie)
        {
            if (Enum.TryParse(jokeCategorie, ignoreCase: true, out JokeCategorie _))
            {
                HttpContext.Session.SetObjectAsJson("JokeCategorie", jokeCategorie);
            }
        }
    }
}
