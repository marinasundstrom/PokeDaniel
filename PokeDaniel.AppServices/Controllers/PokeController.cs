using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PokeDaniel.AppServices.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokeDaniel.AppServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PokeController : ControllerBase
    {
        static ulong pokes = 0;
        private readonly IHubContext<PokeHub> pokeHubContext;

        public PokeController(IHubContext<PokeHub> pokeHubContext)
        {
            this.pokeHubContext = pokeHubContext;
        }

        // GET api/<controller>
        [HttpGet]
        public ActionResult<ulong> Get()
        {
            return pokes;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post()
        {
            pokes++;
            await pokeHubContext.Clients.All.SendAsync("Poked", pokes);
        }
    }
}
