using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behourd.Test.Utilities;
using Xunit;

namespace Behourd.Test
{
    public class SessionTests
    {
        [Fact(DisplayName = "Étant donné une session ayant deux joueurs, " +
                            "quand une partie démarre, " +
                            "alors elle compte deux équipes d'un joueur, chacun différent")]
        public void Two_Players_Make_A_Duel()
        {
            //Arrange = GIVEN = Etant Donné
            var session = new Session(JoueurGenerator.Generate(2).ToList());

            //Act = WHEN = Quand
            var partie = session.DémarrerPartie();

            //Assert = THEN = ALORS
            var équipes = partie.Équipes;

            Assert.Equal(2, équipes.Nombre());
            Assert.Single(équipes.First().Joueurs);
            Assert.Single(équipes.Last().Joueurs);

            Assert.NotSame(équipes.First().Joueurs.Single(), équipes.Last().Joueurs.Single());
        }

        [Fact(DisplayName = "Étant donné une session ayant une partie débutée " +
                            "quand un joueur rejoint la session, " +
                            "alors la liste des joueurs de la partie n'est pas modifié")]
        public void Teams_Are_Immutable()
        {
            //Arrange = GIVEN = Etant Donné
            var joueursInitiaux = JoueurGenerator.Generate(2).ToList();
            var session = new Session(new List<IJoueur>(joueursInitiaux));
            var partie = session.DémarrerPartie();

            //Act = WHEN = Quand
            var joueurEnPlus = JoueurBuilder.Stub;
            session.AddPlayer(joueurEnPlus);

            //Assert = THEN = ALORS
            var joueursPartie = partie.Équipes.First().Joueurs.Concat(partie.Équipes.Last().Joueurs).ToArray();

            Assert.DoesNotContain(joueurEnPlus, joueursPartie);
            Assert.True(joueursInitiaux.SequenceEqual(joueursPartie));
        }

        [Fact(DisplayName = "Étant donné une session ayant une partie débutée " +
                            "quand un joueur rejoint la session, " +
                            "alors il est présent à la partie suivante")]
        public void Player_Is_Included_In_Next_Game()
        {
            //Arrange = GIVEN = Etant Donné
            var joueursInitiaux = JoueurGenerator.Generate(2).ToList();
            var session = new Session(joueursInitiaux);
            var partie = session.DémarrerPartie();

            //Act = WHEN = Quand

            var joueurEnPlus = JoueurBuilder.Stub;
            session.AddPlayer(joueurEnPlus);
            var partie1 = session.DémarrerPartie();
            var joueursPartie = partie1.Équipes.First().Joueurs.Concat(partie1.Équipes.Last().Joueurs).ToArray();

            //Assert = THEN = ALORS

            Assert.Contains<IJoueur>(joueurEnPlus, joueursPartie);
        }

        [Fact(DisplayName = "Étant donné une création de partie, " +
                            "quand on possède un nombre de joueurs insuffisant, " +
                            "alors la partie ne peut pas être créée")]
        public void Invalid_Game_Creation_Not_Enough_Players()
        {
            //Arrange
            List<IJoueur> joueurs = JoueurGenerator.Generate(1).ToList();
            Session session = new Session(joueurs);

            //Act, Assert
            Assert.Throws<System.InvalidOperationException>(() => session.DémarrerPartie());
        }

        [Fact(DisplayName = "Étant donné une liste de joueurs, " +
                            "quand on souhaite calculer la catégorie moyenne de poids de l'équipe, " +
                            "alors on obtient, dans un premier temps, une moyenne de poids")]
        public void Get_Average_Weight_Of_Team()
        {
            List<IJoueur> joueurs = new List<IJoueur>()
            {
                new Joueur(80, 1),
                new Joueur(60, 1),
                new Joueur(40, 1)
            };
            Session session = new Session(joueurs);

            //TODO : enum CategoriePoids
            int moyennePoids = session.CalculerMoyenne(joueurs);

            Assert.Equal(60, moyennePoids);
        }

        [Fact(DisplayName = "Étant donné une liste de joueurs, " +
                            "quand on a calculé la moyenne des poids des joueurs, " +
                            "alors on obtient la catégorie moyenne de poids correspondante")]
        public void Get_Average_Weight_Class_Of_Team()
        {
            List<IJoueur> joueurs = new List<IJoueur>()
            {
                new Joueur(75, 1),
                new Joueur(40, 1),
                new Joueur(60, 1)
            };
            Session session = new Session(joueurs);

            int moyennePoids = session.CalculerMoyenne(joueurs);
            CategoriePoids categorie = session.ObtenirCategoriePoids(moyennePoids);

            Assert.Equal(CategoriePoids.LEGER, categorie);
        }

        [Fact(DisplayName = "Étant donné une liste de joueurs, " +
                           "quand on calcule la somme des ancienneté des joueurs, " +
                           "alors on obtient la difference d'ancienneté des  deux équipes")]

        public void verify_team_affectation()
        {
            List<IJoueur> joueurs = new List<IJoueur>()
            {

            new Joueur(75,0),
            new Joueur(70,2),
            new Joueur(60,5),
            new Joueur(90,3),
            new Joueur(110,3),

            };


            Session session = new Session(joueurs);
            var partie1 = session.DémarrerPartie();
            var équipes = partie1.Équipes;


            Assert.Equal(3, équipes.First().Joueurs.Length);
            Assert.Equal(2, équipes.Last().Joueurs.Length);

        }

            [Fact(DisplayName = "Étant donné une liste de joueurs, " +
                            "quand on calcule la somme des ancienneté des joueurs, " +
                            "alors on obtient la difference d'ancienneté des  deux équipes")]

        public void Get_diffrence_bettween_siniority_Of_Teams()
        {
            List<IJoueur> joueurs = new List<IJoueur>()
            {

            new Joueur(75,0),
            new Joueur(70,2),
            new Joueur(60,5),
            new Joueur(90,3)

            };

            Session session = new Session(joueurs);
            var partie1 = session.DémarrerPartie();
            var équipes = partie1.Équipes;
            IÉquipe equipe1 = équipes.First();
            IÉquipe equipe2 = équipes.Last();

            int resultat_attendu = 4;
            int resultat_reel = session.diff_somme_anciennete(equipe1, equipe2);
            
            Assert.Equal(resultat_attendu, resultat_reel);

        }

    }



        //[Fact]
        //public void Player_Heavy()
        //{
        //    var joueursLégers = JoueurGenerator.GenerateSpecific(
        //        configuration => configuration
        //            .WithWeight(45)
        //            .WithArme(arme => arme.WithWeight(15)), 
        //        15);

        //    var weights = new Stack<int>(new []{ 90, 75, 120 });
        //    JoueurBuilder ConfigureNextPlayer(JoueurBuilder joueurBuilder)
        //    {
        //        return joueurBuilder.WithWeight(weights.Pop());
        //    } 

        //    var joueurLourds = JoueurGenerator.GenerateSpecific(ConfigureNextPlayer, 3);
        //}

    }


