namespace Behourd
{
    public interface IÉquipes
    {
        int Nombre { get; }

        IÉquipe First();
        IÉquipe Last();
    }
}
