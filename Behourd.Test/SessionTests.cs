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

            Assert.Equal(2, �quipes.Nombre);
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
}
