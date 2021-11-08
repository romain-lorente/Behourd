using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Behourd.IHM
{
    class Program
    {
        static void Main(string[] args)
        {
            Session session = CreerSession();

            BouclePrincipale(session);
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

            List<IJoueur> joueurs = ChargerJoueursFichier();

            Console.WriteLine("Joueurs chargés.\n");

            return joueurs;
        }

        static void BouclePrincipale(Session s)
        {
            bool exec = true;

            while(exec)
            {
                Console.WriteLine("1 : Lancer une partie");
                Console.WriteLine("2 : Ajouter un joueur");
                Console.WriteLine("3 : Quitter\n");

                string cmd = Console.ReadLine();

                switch(cmd)
                {
                    case "1":
                        Console.WriteLine($"Lancement d'une partie comportant {s.NombreJoueurs()} joueurs.\n");
                        DeroulementPartie(s);

                        break;

                    case "2":
                        Console.WriteLine("Fonctionnalité non implémentée.\n");
                        
                        break;

                    case "3":
                        exec = false;

                        break;

                    default:
                        Console.WriteLine("Commande non reconnue.\n");

                        break;
                }
            }
        }

        static void DeroulementPartie(Session s)
        {
            Console.WriteLine("Équilibrage des équipes...");

            IPartie partie = s.DémarrerPartie();
            s.EquilibrerEquipes(partie);

            Console.WriteLine("Les équipes ont été formées.\n");

            PresentationEquipes(partie);

            Console.WriteLine("\n\nEntrez n'importe quelle commande pour quitter cette partie.");
            Console.ReadLine();
        }

        static void PresentationEquipes(IPartie partie)
        {
            Console.WriteLine("Équipe 1 : \n");
            PresentationEquipe(partie.Équipes.First());
            Console.WriteLine("\nÉquipe 2 : \n");
            PresentationEquipe(partie.Équipes.Last());
        }

        static void PresentationEquipe(IÉquipe e)
        {
            foreach(IJoueur j in e.Joueurs)
            {
                Console.WriteLine($"{j.nom} {j.prenom}");
            }
        }

        static List<IJoueur> ChargerJoueursFichier()
        {
            XSSFWorkbook wb;
            List<IJoueur> joueurs = new List<IJoueur>();
            string chemin = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\Behourd\res\donnees.xlsx";

            using (FileStream fichier = new FileStream(chemin, FileMode.Open, FileAccess.Read))
            {
                wb = new XSSFWorkbook(chemin);
            }

            ISheet feuille = wb.GetSheet("Feuil1");
            for (int l = 1; l <= feuille.LastRowNum; l++)
            {
                IRow ligne = feuille.GetRow(l);

                if (ligne != null)
                {
                    Joueur j = CreerEntiteJoueur(ligne);
                    joueurs.Add(j);

                    Console.WriteLine($"Joueur {j.nom} {j.prenom} ({j.poids}kg, {j.exp} années d'expérience) ajouté.");
                }
            }

            return joueurs;
        }

        static Joueur CreerEntiteJoueur(IRow ligne)
        {
            string nom = ligne.GetCell(0).StringCellValue;
            string prenom = ligne.GetCell(1).StringCellValue;
            string strPoids = ligne.GetCell(2).StringCellValue;
            double dAnnee = ligne.GetCell(3).NumericCellValue;

            DateTime dateAdhesion = new DateTime().AddYears((int)dAnnee - 1);

            int poids = int.Parse(strPoids.Replace("kg", ""));
            int exp = DateTime.Today.Year - dateAdhesion.Year;

            return new Joueur(poids, exp, nom, prenom);
        }
    }
}
