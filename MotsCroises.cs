using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal_BLANQUE_Bertrand
{
    /// <summary>
    /// Surement la classe la plus compliquée, MotsCroises doit me permettre de savoir si un mots peut etre posé sur
    /// la grille à partir des mots deja posés sur la grille.
    /// Les mots deja posés sont dans MotsGrille.
    /// Un objet motscroises est toujours associé a un joueur. Normalement on aura donc que objet MotsCroises initiés
    /// quand on lancera le jeu.
    /// </summary>
    public class MotsCroises
    {
        public List<string> MotsGrille { get; set; }
        Joueur Joueur { get; set; }
        public MotsCroises(Joueur Unjoueur)
        {
            MotsGrille = new List<string>();
            this.Joueur = Unjoueur;
        }
        /// <summary>
        /// EstUnMotDuDictionnaire permet de savoir si le mot est dans le dictionnaire.
        /// </summary>
        /// <returns></returns>
        public bool EstUnMotDuDictionnaire() //verifie que les mots sont existants
        {
            Dictionnaire LeDictionnaireDeMots = new Dictionnaire();
            LeDictionnaireDeMots.PeuplerDictionnaire();
            bool result = true;
            for (int i = 0; i < MotsGrille.Count; i++)
            {
                //LeDictionnaireDeMots.DichoRecursif(LeDictionnaireDeMots.ListeMots,MotsGrille[i])

                if (!LeDictionnaireDeMots.DichoRecursif(LeDictionnaireDeMots.ListeMots,MotsGrille[i]))
                {
                    Console.WriteLine("ERREUR => Le mot n'est pas dans le dictionnaire");
                    result = false;
                }
            }
            if (result == false)
            {
                MotsGrille.RemoveAt(MotsGrille.Count - 1);
            }
            return result;
        }
        /// <summary>
        /// EstUnMotCroisee permet de savoir si on peut rajouter le mot.
        /// A partir des mots contenu dans la liste des mots trouvés EstUnmotCroisee verfie que le nouveau soit lié par au moins
        /// une lettre avec le nouveau mot.
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool EstUnMotCroisee(string mot, string[] position)
        {
            if (Joueur.ListeMotsTrouves.Count == 0) //premier mot posé
            {
                return true;
            }
            int MotsCroisés = 0;
            for (int i = 0; i < mot.Length; i++)
                //on verifie a au moins une lettre en commun avec la matrice de lettre deja en place
            {
                if (position[2].ToUpper()=="V")
                {
                    if (Joueur.GrilleMatrice[int.Parse(position[0]) - 1+i, int.Parse(position[1]) - 1].Symbole==mot[i])
                    {
                        MotsCroisés += 1; //on a croisé un mot
                    }
                }
                if(position[2].ToUpper()=="H")
                {
                    if (Joueur.GrilleMatrice[int.Parse(position[0]) - 1 , int.Parse(position[1]) - 1+i].Symbole == mot[i])
                    {
                        MotsCroisés += 1;
                    }
                }
            }
            if(MotsCroisés>0)
            {
                return true; //on a croisé au moins un mot.
            }
            else
            {
                Console.WriteLine("ERREUR => Ce n'est pas un mot croisé");
                return false;
            }
        }
    }
}
