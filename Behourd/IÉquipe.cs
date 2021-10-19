namespace Behourd
{
    public interface IÉquipe
    {
        int NombreJoueurs { get; }
        IJoueur[] Joueurs { get; }
    }
}
