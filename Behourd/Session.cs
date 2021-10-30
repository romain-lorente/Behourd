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
            for (int i = 0; i < equipe.Joueurs.Length; i++)
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

        #endregion
    }
}
