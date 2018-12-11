using System;
using System.Threading.Tasks;

namespace PokeDaniel.Server.Services
{
    public interface IPokeService : IDisposable
    {
        Task PokeAsync();

        Task<ulong> GetPokesAsync();
    }
}