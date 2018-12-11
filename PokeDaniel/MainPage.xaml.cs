using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;
using PokeDaniel.ViewModels;
using Xamarin.Forms;

namespace PokeDaniel
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((MainViewModel)BindingContext).InitializeAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await ((MainViewModel)BindingContext).StopAsync();
        }
    }
}
