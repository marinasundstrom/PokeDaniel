using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PokeDaniel.Server.Hubs;
using PokeDaniel.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokeDaniel.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PokeController : ControllerBase
    {
        private readonly IPokeService pokeService;

        public PokeController(IPokeService pokeService)
        {
            this.pokeService = pokeService;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<ActionResult<ulong>> Get()
        {
            return await pokeService.GetPokesAsync();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post()
        {
            await pokeService.PokeAsync();
        }
    }
}
