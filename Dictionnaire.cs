using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal_BLANQUE_Bertrand
{
    /// <summary>
    /// Dictionnaire ne concerne que le dictionnaire de mots dans mon cas.
    /// Un Jeu poccedera un dictionnaire de mots.
    /// </summary>
    public class Dictionnaire
    {
        /// <summary>
        /// Je ne met qu'en attribut le dictionnaire de mots
        /// </summary>
        public Dictionary<string, int> DictionnaireMots { get; set; }
        public List<string> ListeMots { get; set; }
        /// <summary>
        /// Le constructeur rempli le dictionnaire de tous les mots situés dans le fichier MotsPossibles
        /// Quand un jeu se crée son dictionnaire aussi
        /// </summary>
        public Dictionnaire()
        {
            this.DictionnaireMots = PeuplerDictionnaire(); //on rempli le dictionnaire
            List<string> listeMots = new List<string>();
            listeMots = DictionnaireMots.Keys.ToList();
            ListeMots = listeMots;
            ListeMots.Sort(); //liste des mots en ordre alphabetique
        }
        /// <summary>
        /// Méthode pour Remplir un dictionnaire<string ,int > de tous les mots dans le fichier
        /// Je prends comme clée le mots, et en valeur sa taille.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> PeuplerDictionnaire()
        {
            StreamReader texte = new StreamReader(@"MotsPossibles.txt");
            string UneLigne = texte.ReadLine();
            Dictionary<string, int> Dic = new Dictionary<string, int>(); //je crée le dictionnaire
            string[] Donnees;
            try
            {
                while(UneLigne!=null) //tant que le fichier n'est pas fini.
                {
                    UneLigne = texte.ReadLine();
                    if (UneLigne.Length!=1) //je ne veux pas recuperer les lignes correspondantes au longueur des mots
                    {
                        Donnees = UneLigne.Split(); //je recupere tous les mots d'une ligne dans un tableau de string
                        
                        for (int i = 0; i < Donnees.Length; i++)
                        {
                            Dic.Add(Donnees[i], Donnees[i].Length); //key=mot et value=taille du mots
                        }
                    }
                }
            }catch (NullReferenceException) { }
            texte.Close();
            return Dic;
        }
        /// <summary>
        /// ToString est ovveride par rapport au ToString de base.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < 10; i++)
            {
                int Nbre = DictionnaireMots.Where(x => x.Value == i+2).Count();
                result += $"Il y a {Nbre} mots differents de taille {i + 2} \n";
            }
            result+=$"Le dictionnaire est composé de {DictionnaireMots.Count} mots"; //retourne le nombre de mots dans le Dic
            return result;
        }
        /// <summary>
        /// DichoRecursif compare sucessivement le mots et celui a l'index moyenne dans le dico.
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool DichoRecursif(List<string> liste, string mot)
        //cette fonction permet de verifier qu'un element est dans un dictionnaire
        {
            
            int moyenne = liste.Count / 2;
            if (mot == liste[moyenne])
            {
                return true;
            } //les mots sont dans l'ordre alphabetique.
            if (liste.Count == 1)
            {
                return false;
            }
            if (string.Compare(mot, liste[moyenne]) < 0) //je regarde si le mots est devant ou derriere le mots a l'index moyenne
            {
                List<string> J = new List<string>();
                for (int i = 0; i < moyenne; i++) //je reforme une liste de la moitie haute
                {

                    J.Add(liste[i]);
                }
                return DichoRecursif(J, mot);
            }
            if (string.Compare(mot, liste[moyenne]) > 0)
            {
                List<string> J = new List<string>();
                for (int i = moyenne; i < liste.Count; i++)// je reforme une liste de la moitie basse
                {

                    J.Add(liste[i]);
                }
                return DichoRecursif(J, mot);
            }

            else
            {
                return false;
            }
        }
    }
}
