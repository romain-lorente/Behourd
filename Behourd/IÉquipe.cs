using System.Collections.Generic;

namespace Behourd
{
    public interface IÉquipe
    {
        IList<IJoueur> Joueurs { get; }
    }
}
