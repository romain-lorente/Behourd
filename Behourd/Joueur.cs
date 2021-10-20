namespace Behourd
{
    public class Joueur : IJoueur
    {
        public int poids { get; set; }

        public Joueur() { }

        public Joueur(int poids)
        { 
            this.poids = poids;
        }
    }
}
