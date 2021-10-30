using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behourd
{
    public class Équipe : IÉquipe
    {
        public Équipe(IList<IJoueur> joueurs)
        {
            Joueurs = joueurs;
        }
        public IList<IJoueur> Joueurs { get; }
    }
}
