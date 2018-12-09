using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PokeDaniel.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeDaniel.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPokeClient pokeClient;
        private readonly AudioService audioService;
        private RelayCommand pokeCommand;
        private ulong pokes;
        private ulong totalPokes;
        private string text;
        private string text2;

        public MainViewModel(IPokeClient pokeClient, AudioService audioService)
        {
            this.pokeClient = pokeClient;
            this.audioService = audioService;

            Text = "Poke Daniel!";
        }

        public async Task InitializeAsync()
        {
            if (Application.Current.Properties.TryGetValue("count", out var foo))
            {
                pokes = (ulong)foo;
                UpdateText();
            }

            totalPokes = await pokeClient.GetPokesAsync();

            UpdateText2();

            pokeClient.Poked += PokeClient_Poked;
            await pokeClient.StartAsync();
        }

        private void PokeClient_Poked(object sender, PokeEventArgs e)
        {
            totalPokes = e.Pokes;

            UpdateText2();
        }

        public async Task StopAsync()
        {
            await pokeClient.StopAsync();
        }

        public ICommand PokeCommand => pokeCommand ?? (pokeCommand = new RelayCommand(async () =>
        {
            await pokeClient.PokeAsync();

            pokes++;

            UpdateText();
            PlaySound();

            Application.Current.Properties["count"] = pokes;
        }));

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public string Text2
        {
            get => text2;
            set => Set(ref text2, value);
        }

        private void PlaySound()
        {
            if (pokes % 100 == 0)
            {
                audioService.PlayMandel();
            }
            else if (pokes % 10 == 0)
            {
                audioService.PlayDuck();
            }
            else
            {
                audioService.PlayPoke();
            }
        }

        private void UpdateText()
        {
            if (pokes == 1)
            {
                Text = $"You've poked Daniel!";
            }
            else
            {
                Text = $"You've poked Daniel {pokes} times!";
            }
        }

        private void UpdateText2()
        {
            if (totalPokes == 1)
            {
                Text2 = $"Daniel has been poked";
            }
            else
            {
                Text2 = $"Daniel has been poked {totalPokes} times!";
            }
        }
    }
}
