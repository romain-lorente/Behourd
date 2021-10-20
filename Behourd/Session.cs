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

            public Partie(List<IJoueur> joueur)
            {
                Équipes = new ImmutableÉquipes(joueur);
            }
        }
        private class ImmutableÉquipes : IÉquipes
        {
            private readonly IÉquipe _first;
            private readonly IÉquipe _last;

            public ImmutableÉquipes(List<IJoueur> joueurs)
            {
                _first = new Équipe(joueurs.First());
                _last = new Équipe(joueurs.Last());
            }

            public int Nombre => 2;
            public IÉquipe First() => _first;
            public IÉquipe Last() => _last;
        }

        private class Équipe : IÉquipe
        {
            private readonly IJoueur _joueur;

            public Équipe(IJoueur joueur)
            {
                _joueur = joueur;
            }

            public int NombreJoueurs => 1;
            public IJoueur[] Joueurs => new [] { _joueur };
        }

        public void AddPlayer(IJoueur joueurEnPlus)
        {
            _joueur.Add(joueurEnPlus);
           
        }

        #region Méthodes de calcul

        #endregion
    }
}
