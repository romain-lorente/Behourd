using System;
using System.Collections.Generic;

namespace Behourd.IHM
{
    class Program
    {
        static void Main(string[] args)
        {
            Session session = CreerSession();

            BouclePrincipale(session);

            //Fin du programme
            Console.ReadLine();
        }

        static Session CreerSession()
        {
            Console.WriteLine("BEHOURD - GESTIONNAIRE DE SESSIONS\n\n");
            Console.WriteLine("Création d'une session...");

            Session session = new Session(ChargerJoueurs());

            Console.WriteLine("Session créée.\n");

            return session;
        }

        static List<IJoueur> ChargerJoueurs()
        {
            Console.WriteLine("Chargement des données des joueurs...\n");

            //TODO : données temporaires, utiliser le fichier
            List<IJoueur> joueurs = new List<IJoueur>
            {
                new Joueur(134, 15, "GROS", "Paul"),
                new Joueur(47, 1, "BLANC", "Louis"),
                new Joueur(79, 34, "GIRAUD", "Jean-Michel"),
                new Joueur(102, 18, "PÄRIS", "Théophile")
            };

            foreach(Joueur j in joueurs)
            {
                Console.WriteLine($"Joueur {j.nom} {j.prenom} ({j.poids}kg, {j.exp} années d'expérience) ajouté.");
            }

            Console.WriteLine("Joueurs chargés.\n");

            return joueurs;
        }

        static void BouclePrincipale(Session s)
        {
            
        }
    }
}
