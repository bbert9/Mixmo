using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetFinal_BLANQUE_Bertrand
{
    /// <summary>
    /// La classe Lettres correponds a une liste de Lettre. J'ai intentionellement réduit l'utilité de cette qu'a former
    /// une pioche. Quand j'ai besoin d'une liste de Lettre je la crée directement plutot que d'utiliser cette classe.
    /// </summary>
    public class Lettres
    {
       
        public List<Lettre> Pioche { get; set; }
        //public List<Lettre> Plateau { get; set; }
        public Lettres()
        {
            Pioche = LaPioche();
        }
        /// <summary>
        /// Cette méthode permet de former la Pioche. Normalement cette méthode est appelé en début de compilation
        /// pour crée la pioche du jeu.
        /// </summary>
        /// <returns></returns>
        public List<Lettre> LaPioche() //peupler la pioche
        {
            string file1 = @"Lettre.txt";//n'est pas dans le debug
            StreamReader Lettres = new StreamReader(file1);
            List<Lettre> LaNouvellePioche = new List<Lettre>();
            string[] Donnees;
            string UneLigne;
            while ((UneLigne = Lettres.ReadLine()) != null)//condition d'arret.
            {
                Donnees = UneLigne.Split(','); //données séparés par des virgules
                Lettre UneLettre = new Lettre(char.Parse("a"), 2);//c'est une variable
                UneLettre.Symbole = char.Parse(Donnees[0]);//on l'actualise a chaque passage dans la boucle
                UneLettre.Poid = int.Parse(Donnees[2]);
                for (int j = 0; j < int.Parse(Donnees[1]); j++)//et on l'ajoute pour le nombre de fois ou elle est censé etre dans la pioche
                {
                    LaNouvellePioche.Add(UneLettre);
                }

            }
            Lettres.Close();
            //on ajoute deux jokers
            Lettre JOKER = new Lettre('§', 0);//mes JOKER sont un peu spéciaux. Je les remarques facilement avec '§'.
            LaNouvellePioche.Add(JOKER);
            LaNouvellePioche.Add(JOKER);
            return LaNouvellePioche;
        }
        public override string ToString()
        {
            string result = $"Il y a {Pioche.Count()} elements dans la Pioche";
            return result;
        }
    }
}
