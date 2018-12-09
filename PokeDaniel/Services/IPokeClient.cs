using System;
using System.Threading.Tasks;

namespace PokeDaniel.Services
{
    public interface IPokeClient
    {
        event EventHandler<PokeEventArgs> Poked;

        Task PokeAsync();
        Task StartAsync();
        Task StopAsync();
        Task<ulong> GetPokesAsync();
    }
}