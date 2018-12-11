using Blazor.Extensions;
using Blazor.Extensions.Logging;
using Blazor.Extensions.Storage;
using BlazorSignalR;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using PokeDaniel.Services;

namespace PokeDaniel.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();
            services.AddNotifications();

            services.AddLogging(builder => builder
                     .AddBrowserConsole()
            );

            services.AddTransient((sp) => 
                new HubConnectionBuilder().WithUrlBlazor("/pokehub",
                            options: opt =>
                            {
                                opt.Transports = HttpTransportType.WebSockets;
                            }).Build());

            services.AddTransient<IPokeClient, PokeClient>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
