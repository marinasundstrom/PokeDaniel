using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace PokeDaniel.Client
{
    public static class AudioHelpers
    { 
        public static async Task PlayMandel()
        {
            await JSRuntime.Current.InvokeAsync<object>("playMandel");
        }

        public static async Task PlayDuck()
        {
            await JSRuntime.Current.InvokeAsync<object>("playDuck");
        }

        public static async Task PlayPoke()
        {
            await JSRuntime.Current.InvokeAsync<object>("playPoke");
        }
    }
}