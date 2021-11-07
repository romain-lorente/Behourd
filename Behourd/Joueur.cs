namespace Behourd
{
    public class Joueur : IJoueur
    {
        public int poids { get; set; }
        public int exp { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        public Joueur() { }

        public Joueur(int poids, int exp)
        { 
            this.poids = poids;
            this.exp = exp;
        }

        public Joueur(int poids, int exp, string nom, string prenom)
        {
            this.poids = poids;
            this.exp = exp;
            this.nom = nom;
            this.prenom = prenom;
        }
    }
}
