using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace ProjetFinal_BLANQUE_Bertrand
{
    class Program
    {
        /// <summary>
        /// Dans le main je vais initialiser deux joueurs et un jeu. Je vais faire jouer les joueurs chacun leurs tour
        /// tant que le jeu n'est pas terminé.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //mon programme a une forte probabilité d'avoir le mauvais lien d'accés.
            //prenez soin SVP de fixer le lien d'accés a votre debug si ca ne marche pas.
            //Environment.CurrentDirectory = @"\\Mac\Home\Desktop\COURS\informatique\ProjetFinal-BLANQUE-Bertrand\ProjetFinal-BLANQUE-Bertrand\bin\Debug";
            
            Console.WriteLine(Directory.GetCurrentDirectory());
            Random r = new Random();

            Joueur Joueur1 = new Joueur("prof");
            Joueur Joueur2 = new Joueur("bertrand");
            Jeu Jeu1 = new Jeu(2, Joueur1, Joueur2);

            InitialisationJeu(r, Joueur1, Joueur2, Jeu1);
            int i = 0;

            do
            {
                i += 1;
                UnTour(Joueur1, Joueur2, Jeu1, r, i);

            } while (!Jeu1.JeuFini());

        }


        /// <summary>
        /// Dans la méthode UnTour je donne la main a un joueur ou l'autre pour qu'il puisse se déclarer, déclarer
        /// son mots, et dire la position de celui ci. A la fin sa grille est actualisée dans la console.
        /// </summary>
        /// <param name="Joueur1"></param>
        /// <param name="Joueur2"></param>
        /// <param name="Jeu1"></param>
        /// <param name="r"></param>
        private static void UnTour(Joueur Joueur1, Joueur Joueur2, Jeu Jeu1, Random r,int i)
        {

            DateTime Copie = DateTime.Now; //permet de faire le Timer
            Console.WriteLine("Tour " + i);
            Console.WriteLine("Le joueur qui veut jouer rentre son Nom (maj ou non), Vous avez 60 secondes.");
            string Nom = Console.ReadLine();
            Console.WriteLine("Rentrez le mots en MAJUSCULE");
            string mots = Console.ReadLine();
            Console.WriteLine("Saissiez la ligne, la colonne, h: horizontale v: verticale");
            string saisie1 = Console.ReadLine();
            string[] Position = saisie1.Split(',');
            MotsCroises MotsCroises1 = new MotsCroises(Joueur1);//Un objet MotsCroises pour chaque joueur
            MotsCroises MotsCroises2 = new MotsCroises(Joueur2);

            if (Nom.ToUpper() == Joueur1.Nom.ToUpper())
            {
                Console.Clear();
                if (DateTime.Now.Second+DateTime.Now.Minute*60 - Copie.Second-Copie.Minute*60 < 60) //verifie que il ne s'est pas écoulé 60 secondes.
                {
                    Console.WriteLine($"DUREE DU TOUR => {DateTime.Now.Second + DateTime.Now.Minute * 60 - Copie.Second - Copie.Minute * 60} secondes");
                    Joueur1.PoserUnMot(mots, Position, MotsCroises1, Jeu1, r);
                    //méthode qui permet de faire toutes les verifications et de poser un mot. et rajoute deux lettre a ce joueur
                    Copie = DateTime.Now;
                    Joueur2.Add_Lettres(2, Jeu1.LaPioche, r); //on ajoute deux lettres a l'autre joueur.
                }
                else
                {
                    Console.WriteLine($"DUREE DU TOUR => {DateTime.Now.Second + DateTime.Now.Minute * 60 - Copie.Second - Copie.Minute * 60} secondes");
                    Console.WriteLine("TO LATE: temps dépassé");
                    Copie = DateTime.Now;
                    Joueur1.Add_Lettres(2, Jeu1.LaPioche, r);//les deux prennent deux lettre
                    Joueur2.Add_Lettres(2, Jeu1.LaPioche, r);
                }
                Console.WriteLine(Joueur1.toString());
                
                Console.WriteLine("/////////////////////////////////////////////////////////////////");
                Joueur2.AfficherGrille();
                Console.WriteLine(Joueur2.toString());
            }
            if (Nom.ToUpper() == Joueur2.Nom.ToUpper())
            {
                Console.Clear();
                if (DateTime.Now.Second + DateTime.Now.Minute * 60 - Copie.Second - Copie.Minute * 60 < 60)
                {
                    Console.WriteLine($"DUREE DU TOUR => {DateTime.Now.Second + DateTime.Now.Minute * 60 - Copie.Second - Copie.Minute * 60} secondes");
                    Joueur2.PoserUnMot(mots, Position, MotsCroises2, Jeu1, r);
                    Copie = DateTime.Now;
                    Joueur1.Add_Lettres(2, Jeu1.LaPioche, r);
                }
                else
                {
                    Console.WriteLine($"DUREE DU TOUR => {DateTime.Now.Second + DateTime.Now.Minute * 60 - Copie.Second - Copie.Minute * 60} secondes");
                    Console.WriteLine("TO LATE: temps dépassé");
                    Copie = DateTime.Now;
                    Joueur1.Add_Lettres(2, Jeu1.LaPioche, r);
                    Joueur2.Add_Lettres(2, Jeu1.LaPioche, r);
                }
                Console.WriteLine(Joueur2.toString());
                Console.WriteLine("/////////////////////////////////////////////////////////////////");
                Joueur1.AfficherGrille();
                Console.WriteLine(Joueur1.toString());
            }
            else //surement due a une erreur d'entrée de l'utilisateur.
            {
                Console.WriteLine("ERREUR => Veuillez rentrer le Nom de l'utlisateur et ecrire le mot puis" +
                    " la position ex: 1,1,h");
                i -= 1; //on annule le tour et c'est encore a lui de jouer.
            }
        }
        
        /// <summary>
        /// J'initialise le jeu: Les joueurs et differents parametres.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Joueur1"></param>
        /// <param name="Joueur2"></param>
        /// <param name="Jeu1"></param>
        private static void InitialisationJeu(Random r, Joueur Joueur1, Joueur Joueur2, Jeu Jeu1)
        {
            Console.Clear();
            ConsignesEtSpecificitéesDeMonProgramme(); //correspond aux consignes: comment utiliser mes JOKERS par ex
            Console.WriteLine("BIENVENUE DANS LE MIXMO");
            Console.WriteLine("NOUS ALLONS DEBUTER UNE PARTIE");
            Joueur1.AfficherGrille();
            Joueur1.Add_Lettres(6, Jeu1.LaPioche, r);
            Console.WriteLine(Joueur1.toString());

            Joueur2.Add_Lettres(6, Jeu1.LaPioche, r);
            Joueur2.AfficherGrille();
            Console.WriteLine(Joueur2.toString());

        }
        private static void ConsignesEtSpecificitéesDeMonProgramme()
        {
            Console.WriteLine("Bienvenue dans Le mixmo");
            Console.WriteLine("Dans cette version toutes les fonctionnalitées sont comme présentées sur le sujet. Pour utiliser" +
                " les Jokers il faut");
            Console.WriteLine("Mettre des () autour de la lettre remplacée.");
            Console.WriteLine("Exemple: Vous avez un A et Un JOKER mais pas un L. Vous pouvez marquer (L)A pour spécifiée que vous utilisez ce joker pour" +
                " le L.");
        }
       
    }
}
