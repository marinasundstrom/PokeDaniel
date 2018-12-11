using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;
using PokeDaniel.Server.Hubs;

namespace PokeDaniel.Server.Services
{
    public class PokeService : IPokeService
    {
        private readonly IHubContext<PokeHub> pokeHubContext;
        private Subject<ulong> subject;
        private readonly IDisposable observable;
        private ulong pokes;

        public PokeService(IHubContext<PokeHub> pokeHubContext)
        {
            this.pokeHubContext = pokeHubContext;

            subject = new Subject<ulong>();
            observable = subject
                //.Throttle(TimeSpan.FromMilliseconds(200))
                .Subscribe(async o => {
                    await pokeHubContext.Clients.All.SendAsync("Poked", pokes);
                });
        }

        public Task PokeAsync()
        {
            return Task.Run(() =>
            {
                pokes++;
                subject.OnNext(pokes);
            });
        }

        public Task<ulong> GetPokesAsync()
        {
            return Task.FromResult(pokes);
        }

        public void Dispose()
        {
            subject.Dispose();
            observable.Dispose();
        }
    }
}

