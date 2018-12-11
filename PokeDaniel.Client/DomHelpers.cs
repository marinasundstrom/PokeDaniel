using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace PokeDaniel.Client
{
    public static class DomHelpers
    {
        public static async Task ScrollToBottom(bool smooth, int delay)
        {
            await JSRuntime.Current.InvokeAsync<object>("domHelpers.scrollToBottom", smooth, delay);
        }
    }
}