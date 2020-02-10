using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal_BLANQUE_Bertrand
{
    /// <summary>
    /// La classe Jeu va initier une partie avec un certains de Joueur regroupé dans une liste de JOueur
    /// Un jeu a sa pioche et se refere un dictionnaire pour verfier les mots.
    /// </summary>
    public class Jeu
    {
        public Dictionnaire DictionnaireJEU { get; set; }
        public List<Joueur> ListeJoueur { get; set; }
        public Lettres LaPioche { get; set; }
        public Jeu(int NombreJoueur, Joueur Joueur1, Joueur Joueur2) //constructeur du jeu
        {
            //ici on rentre 2 joueurs.
            Dictionnaire UnDic = new Dictionnaire();
            UnDic.DictionnaireMots = UnDic.PeuplerDictionnaire();
            this.DictionnaireJEU=UnDic;
            this.ListeJoueur = new List<Joueur>();
            this.ListeJoueur.Add(Joueur1);
            ListeJoueur.Add(Joueur2);
            Lettres LaPioche = new Lettres();
            this.LaPioche = LaPioche;
        }
        /// <summary>
        /// On a une boucle dans le Main que l'on veut arreter si le jeu est terminé.
        /// Deux cas sont possibles => plus de lettre dans la pioche ou => Un joueur n'a plus de lettre.
        /// </summary>
        /// <returns></returns>
        public bool JeuFini() //verifie que le jeu n'est pas fini
        {
            bool result = false;
            for (int i = 0; i < ListeJoueur.Count; i++) //regarde pour chaque joueur
            {
                if(ListeJoueur[i].ListeLettres.Count==0)
                {
                    Console.WriteLine($"{ListeJoueur[i].Nom} a gagné! il a posé toutes ses lettres!");
                    result = true;
                }//es ce que le joueur a encore des lettres?
            }
            if (LaPioche.Pioche.Count == 0) {
                result = true;
                Joueur Gagnant = ListeJoueur[0];
                for (int i = 0; i < ListeJoueur.Count; i++)
                {
                    if (ListeJoueur[i].Score>Gagnant.Score)
                    {
                        Gagnant = ListeJoueur[i];
                    }
                }
                Console.WriteLine($"{Gagnant.Nom} a gagné avec un score de {Gagnant.Score}");
            }//es ce que la pioche a encore des lettres?
            return result;
        }
    }
}
