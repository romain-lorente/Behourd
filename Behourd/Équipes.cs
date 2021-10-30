using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behourd
{
    public class Équipes : IÉquipes
    {
        private readonly IÉquipe _first;
        private readonly IÉquipe _last;

        public Équipes(List<IJoueur> joueurs)
        {
            _first = new Équipe(joueurs.First());
            _last = new Équipe(joueurs.Last());
        }

        public int Nombre() => 2;
        public IÉquipe First() => _first;
        public IÉquipe Last() => _last;
    }
}
