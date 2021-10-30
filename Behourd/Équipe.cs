using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behourd
{
    public class Équipe : IÉquipe
    {
        private readonly IJoueur _joueur;

        public Équipe(IJoueur joueur)
        {
            _joueur = joueur;
        }
        public IJoueur[] Joueurs => new[] { _joueur };
    }
}
