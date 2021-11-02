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
            public IÉquipes Équipes { get; set; }

            public Partie(List<IJoueur> joueurs)
            {
                Équipes = new Équipes(joueurs);
            }
        }

        public void AddPlayer(IJoueur joueurEnPlus)
        {
            _joueur.Add(joueurEnPlus);
           
        }

        public void EquilibrerEquipes(IPartie partie)
        {
            int lon = _joueur.Count;
            int diffCategoriesARetenir = 0;
            List<Équipes> equipesARetenir = new List<Équipes>();

            for (int i = 1; i < lon; i++)
            {
                //On forme les deux équipes
                List<IJoueur> joueursE1 = new List<IJoueur>()
                {
                    _joueur[0],
                    _joueur[i]
                };

                List<IJoueur> joueursE2 = new List<IJoueur>(_joueur);
                joueursE2.RemoveAt(i);
                joueursE2.RemoveAt(0);

                Équipe e1 = new Équipe(joueursE1);
                Équipe e2 = new Équipe(joueursE2);

                //Puis on récupère les catégories de poids de chaque équipe
                CategoriePoids catE1 = ObtenirCategoriePoids(CalculerMoyenne(joueursE1));
                CategoriePoids catE2 = ObtenirCategoriePoids(CalculerMoyenne(joueursE2));

                //On calcule la différence entre les catégories
                int diffCategories = Math.Abs((int)catE1 - (int)catE2);

                if(equipesARetenir.Count > 0)
                {
                    //Si la combinaison en cours est meilleure que la précédente, alors on ne garde que la nouvelle
                    if(diffCategories < diffCategoriesARetenir)
                    {
                        equipesARetenir.Clear();
                        equipesARetenir.Add(new Équipes(e1, e2));
                        diffCategoriesARetenir = diffCategories;
                    }
                    //On ajoute l'équipe à la liste de combinaisons à retenir si la différence de catégorie de poids est la même
                    else if (diffCategories == diffCategoriesARetenir)
                    {
                        equipesARetenir.Add(new Équipes(e1, e2));
                    }
                }
                else
                {
                    //A la première exécution, on ajoute directement l'équipe à la liste des combinaisons à retenir
                    equipesARetenir.Add(new Équipes(e1, e2));
                    diffCategoriesARetenir = diffCategories;
                }
            }

            //A la sortie de la boucle, s'il n'y a qu'une équipe, alors on la retient
            if (equipesARetenir.Count == 1)
            {
                partie.Équipes = equipesARetenir.First();
            }
            //Sinon, on compare les anciennetés
            /*else
            {
                int diffAnciennete = 0;

                foreach(Équipes eqs in equipesARetenir)
                {

                }
            }*/
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
