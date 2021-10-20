namespace Behourd
{
    public class Joueur : IJoueur
    {
        public int poids { get; set; }
        public int exp { get; set; }

        public Joueur() { }

        public Joueur(int poids, int exp)
        { 
            this.poids = poids;
            this.exp = exp;
        }
    }
}
