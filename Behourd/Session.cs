using System.Linq;

namespace Behourd
{
    public class Session
    {
        private readonly IJoueur[] _joueur;

        public Session(params IJoueur[] joueur)
        {
            _joueur = joueur;
        }

        public IPartie DémarrerPartie() => new Partie(_joueur);

        private class Partie : IPartie
        {
            public IÉquipes Équipes { get; }

            public Partie(IJoueur[] joueur)
            {
                Équipes = new ImmutableÉquipes(joueur);
            }
        }

        private class ImmutableÉquipes : IÉquipes
        {
            private readonly IÉquipe _first;
            private readonly IÉquipe _last;

            public ImmutableÉquipes(IJoueur[] joueurs)
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
        }
    }
}
