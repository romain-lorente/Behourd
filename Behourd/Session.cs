using System;
using System.Collections.Generic;
using System.Linq;

namespace Behourd
{
    public class Session
    {
        private List<IJoueur> _joueur;

        public Session(List<IJoueur> joueur)
        {
            _joueur = joueur;
        }

        public IPartie DémarrerPartie()
        {
            if (_joueur.Count < 2)
                throw new InvalidOperationException();

            return new Partie(new List<IJoueur>(_joueur));
        }

        private class Partie : IPartie
        {
            public IÉquipes Équipes { get; }

            public Partie(List<IJoueur> joueurs)
            {
                Équipes = new Équipes(joueurs);
            }
        }

        public void AddPlayer(IJoueur joueurEnPlus)
        {
            _joueur.Add(joueurEnPlus);
           
        }

        #region Méthodes de calcul
        
        public int somme_anciennete(IÉquipe equipe)
        {
            int sum = 0;
            for (int i = 0; i < equipe.Joueurs.Count; i++)
            {
                sum += equipe.Joueurs[i].exp;
            }
            return sum ;
        }
        
        public int diff_somme_anciennete(IÉquipe equipe1, IÉquipe equipe2)
        {
            int sum_equipe1 = somme_anciennete(equipe1);
            int sum_equipe2 = somme_anciennete(equipe2);

            return Math.Abs(sum_equipe1- sum_equipe2);
        }

        public int CalculerMoyenne(List<IJoueur> joueurs)
        {
            int moyenne = 0;

            foreach(IJoueur j in joueurs)
            {
                moyenne += j.poids;
            }

            moyenne /= joueurs.Count();

            return moyenne;
        }

        public CategoriePoids ObtenirCategoriePoids(int moyennePoids)
        {
            if(moyennePoids < 52)
            {
                return CategoriePoids.MOUCHE;
            }
            if(moyennePoids >= 52 && moyennePoids <= 56)
            {
                return CategoriePoids.PLUME;
            }
            if (moyennePoids >= 57 && moyennePoids <= 62)
            {
                return CategoriePoids.LEGER;
            }
            if (moyennePoids >= 63 && moyennePoids <= 69)
            {
                return CategoriePoids.WELTER;
            }
            if (moyennePoids >= 70 && moyennePoids <= 75)
            {
                return CategoriePoids.MOYEN;
            }
            if (moyennePoids >= 76 && moyennePoids <= 81)
            {
                return CategoriePoids.MI_LOURD;
            }
            if (moyennePoids >= 82 && moyennePoids <= 90)
            {
                return CategoriePoids.LOURD;
            }

            return CategoriePoids.SUPER_LOURD;
        }

        #endregion
    }
}
