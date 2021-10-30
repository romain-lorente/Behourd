using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behourd.Test.Utilities;
using Xunit;

namespace Behourd.Test
{
    public class SessionTests
    {
        [Fact(DisplayName = "�tant donn� une session ayant deux joueurs, " +
                            "quand une partie d�marre, " +
                            "alors elle compte deux �quipes d'un joueur, chacun diff�rent")]
        public void Two_Players_Make_A_Duel()
        {
            //Arrange = GIVEN = Etant Donn�
            var session = new Session(JoueurGenerator.Generate(2).ToList());

            //Act = WHEN = Quand
            var partie = session.D�marrerPartie();

            //Assert = THEN = ALORS
            var �quipes = partie.�quipes;

            Assert.Equal(2, �quipes.Nombre());
            Assert.Single(�quipes.First().Joueurs);
            Assert.Single(�quipes.Last().Joueurs);

            Assert.NotSame(�quipes.First().Joueurs.Single(), �quipes.Last().Joueurs.Single());
        }

        [Fact(DisplayName = "�tant donn� une session ayant une partie d�but�e " +
                            "quand un joueur rejoint la session, " +
                            "alors la liste des joueurs de la partie n'est pas modifi�")]
        public void Teams_Are_Immutable()
        {
            //Arrange = GIVEN = Etant Donn�
            var joueursInitiaux = JoueurGenerator.Generate(2).ToList();
            var session = new Session(new List<IJoueur>(joueursInitiaux));
            var partie = session.D�marrerPartie();

            //Act = WHEN = Quand
            var joueurEnPlus = JoueurBuilder.Stub;
            session.AddPlayer(joueurEnPlus);

            //Assert = THEN = ALORS
            var joueursPartie = partie.�quipes.First().Joueurs.Concat(partie.�quipes.Last().Joueurs).ToArray();

            Assert.DoesNotContain(joueurEnPlus, joueursPartie);
            Assert.True(joueursInitiaux.SequenceEqual(joueursPartie));
        }

        [Fact(DisplayName = "�tant donn� une session ayant une partie d�but�e " +
                            "quand un joueur rejoint la session, " +
                            "alors il est pr�sent � la partie suivante")]
        public void Player_Is_Included_In_Next_Game()
        {
            //Arrange = GIVEN = Etant Donn�
            var joueursInitiaux = JoueurGenerator.Generate(2).ToList();
            var session = new Session(joueursInitiaux);
            var partie = session.D�marrerPartie();

            //Act = WHEN = Quand

            var joueurEnPlus = JoueurBuilder.Stub;
            session.AddPlayer(joueurEnPlus);
            var partie1 = session.D�marrerPartie();
            var joueursPartie = partie1.�quipes.First().Joueurs.Concat(partie1.�quipes.Last().Joueurs).ToArray();

            //Assert = THEN = ALORS

            Assert.Contains<IJoueur>(joueurEnPlus, joueursPartie);
        }

        [Fact(DisplayName = "�tant donn� une cr�ation de partie, " +
                            "quand on poss�de un nombre de joueurs insuffisant, " +
                            "alors la partie ne peut pas �tre cr��e")]
        public void Invalid_Game_Creation_Not_Enough_Players()
        {
            //Arrange
            List<IJoueur> joueurs = JoueurGenerator.Generate(1).ToList();
            Session session = new Session(joueurs);

            //Act, Assert
            Assert.Throws<System.InvalidOperationException>(() => session.D�marrerPartie());
        }

        [Fact(DisplayName = "�tant donn� une liste de joueurs, " +
                            "quand on souhaite calculer la cat�gorie moyenne de poids de l'�quipe, " +
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

        [Fact(DisplayName = "�tant donn� une liste de joueurs, " +
                            "quand on a calcul� la moyenne des poids des joueurs, " +
                            "alors on obtient la cat�gorie moyenne de poids correspondante")]
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

        [Fact(DisplayName = "�tant donn� une liste de joueurs, " +
                           "quand on calcule la somme des anciennet� des joueurs, " +
                           "alors on obtient la difference d'anciennet� des  deux �quipes")]

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
            var partie1 = session.D�marrerPartie();
            var �quipes = partie1.�quipes;


            Assert.Equal(3, �quipes.First().Joueurs.Length);
            Assert.Equal(2, �quipes.Last().Joueurs.Length);

        }

            [Fact(DisplayName = "�tant donn� une liste de joueurs, " +
                            "quand on calcule la somme des anciennet� des joueurs, " +
                            "alors on obtient la difference d'anciennet� des  deux �quipes")]

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
            var partie1 = session.D�marrerPartie();
            var �quipes = partie1.�quipes;
            I�quipe equipe1 = �quipes.First();
            I�quipe equipe2 = �quipes.Last();

            int resultat_attendu = 4;
            int resultat_reel = session.diff_somme_anciennete(equipe1, equipe2);
            
            Assert.Equal(resultat_attendu, resultat_reel);

        }

    }



        //[Fact]
        //public void Player_Heavy()
        //{
        //    var joueursL�gers = JoueurGenerator.GenerateSpecific(
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


