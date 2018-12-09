using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PokeDaniel.Services
{
    public class AudioService
    {
        private ISimpleAudioPlayer mandelPlayer;
        private ISimpleAudioPlayer duckPlayer;
        private ISimpleAudioPlayer pokePlayer;

        public AudioService()
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

        public void PlayMandel()
        {
            mandelPlayer.Play();
        }

        public void PlayDuck()
        {
            duckPlayer.Play();
        }

        public void PlayPoke()
        {
            pokePlayer.Play();
        }
    }
}
