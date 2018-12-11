using GalaSoft.MvvmLight.Ioc;
using Microsoft.AspNetCore.SignalR.Client;
using PokeDaniel.Services;
using PokeDaniel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace PokeDaniel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();

            var baseUrl = new Uri("https://pokedaniel0181209015714.azurewebsites.net/");
            //baseUrl = new Uri("https://localhost:53248/");

            SimpleIoc.Default.Register<HttpClient>(() => new HttpClient()
            {
                BaseAddress = baseUrl
            });

            SimpleIoc.Default.Register<HubConnection>(() => new HubConnectionBuilder()
                .WithUrl(new Uri(baseUrl, "/pokehub").ToString())
                .Build());

            SimpleIoc.Default.Register<IPokeClient, PokeClient>();
            SimpleIoc.Default.Register<AudioService>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
        }
    }
}
