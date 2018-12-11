namespace PokeDaniel.Services
{
    public class PokeEventArgs
    {
        public PokeEventArgs(ulong pokes)
        {
            Pokes = pokes;
        }

        public ulong Pokes { get; }
    }
}