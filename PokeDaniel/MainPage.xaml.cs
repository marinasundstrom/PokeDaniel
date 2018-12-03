using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

namespace PokeDaniel
{
	public partial class MainPage : ContentPage
	{
		private ulong count = 0;

		private ISimpleAudioPlayer mandelPlayer;
		private ISimpleAudioPlayer duckPlayer;
		private ISimpleAudioPlayer pokePlayer;

		public MainPage()
		{
			InitializeComponent();

			if (Application.Current.Properties.TryGetValue("count", out var foo))
			{
				count = (ulong)foo;
				UpdateText();
			}

			LoadSound();
		}

		private void LoadSound()
		{
			mandelPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			Stream audioStream = LoadAudioStream("mandel.mp3");
			mandelPlayer.Load(audioStream);

			duckPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			audioStream = LoadAudioStream("duck.mp3");
			duckPlayer.Load(audioStream);

			pokePlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			audioStream = LoadAudioStream("poke.mp3");
			pokePlayer.Load(audioStream);
		}

		private static Stream LoadAudioStream(string fileName)
		{
			var assembly = typeof(PokeDaniel.Common.EmptyClass).GetTypeInfo().Assembly;
			return assembly.GetManifestResourceStream("PokeDaniel.Common." + fileName);
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			count++;

			UpdateText();

			PlaySound();

			Application.Current.Properties["count"] = count;
		}

		private void PlaySound()
		{
			if(count % 100 == 0)
			{
				mandelPlayer.Play();
			}
			else if (count % 10 == 0)
			{
				duckPlayer.Play();
			}
			else
			{
				pokePlayer.Play();
			}
		}

		private void UpdateText()
		{
			if (count == 1)
			{
				TextLabel.Text = $"You've poked Daniel!";
			}
			else
			{
				TextLabel.Text = $"You've poked Daniel {count} times!";
			}
		}
	}
}
