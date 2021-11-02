using System;
using System.Collections.Generic;

namespace Behourd
{
    public class Équipes : IÉquipes
    {
        private readonly IÉquipe _first;
        private readonly IÉquipe _last;

        public Équipes(List<IJoueur> joueurs)
        {
            _first = new Équipe(new List<IJoueur>());
            _last = new Équipe(new List<IJoueur>());

            bool estMembreEquipeUne = true;

            foreach(IJoueur j in joueurs)
            {
                if(estMembreEquipeUne)
                {
                    _first.Joueurs.Add(j);
                }
                else
                {
                    _last.Joueurs.Add(j);
                }

                estMembreEquipeUne = !estMembreEquipeUne;
            }
        }

        public Équipes(Équipe e1, Équipe e2)
        {
            _first = e1;
            _last = e2;
        }

        public int Nombre() => 2;
        public IÉquipe First() => _first;
        public IÉquipe Last() => _last;
    }
}
