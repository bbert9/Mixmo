using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal_BLANQUE_Bertrand
{
    /// <summary>
    /// Un joueur est caractérisé par son nom, son score, les lettres en sa possesion, les mots qu'il a trouvé, et sa matrice
    /// de lettres.
    /// C'est ma classe la plus importante, elle va faire toutes les verifications concernant les mots rentrés dans la matrice:
    /// es ce que c'est un mot ? Es ce qu'on a les lettres pour le former? 
    /// </summary>
    public class Joueur
    {
        public string Nom { get; set; }
        public int Score { get; set; }
        public List<Lettre> ListeLettres { get; set; }
        public List<string> ListeMotsTrouves { get; set; }
        public Lettre[,] GrilleMatrice { get; set; }
        public Joueur(string Nom) //constructeur
        {
            this.Nom = Nom;
            Score = 0;
            ListeLettres = new List<Lettre>();
            ListeMotsTrouves = new List<string>();
            this.GrilleMatrice = new Lettre[10,10]; //on sait que la matrice est une 10,10
            //on doit instancier la mtrice. Je rempli donc la matrice d'espaces.
            //ses lettres vides seront remplacés quand on posera un mots.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Lettre UneLettre = new Lettre(' ', 0);
                    GrilleMatrice[i, j] = UneLettre;
                }
            }
            
        }
        /// <summary>
        /// Pour ajouter une lettre on prend un index random dans la pioche est on preleve la lettre a cette index
        /// On la rajoute a la liste de lettre du joueur.
        /// </summary>
        /// <param name="nb"></param>
        /// <param name="pioche"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool Add_Lettres(int nb, Lettres pioche, Random r)
            //tire au hasard un nb de letttres dans la pioche
        {
            for(int i=0;i<nb;i++)
            {
                int index = r.Next(pioche.Pioche.Count); //la pioche est une liste
                ListeLettres.Add(pioche.Pioche[index]);
                pioche.Pioche.RemoveAt(index); //on enleve ce quon a pioché a la Pioche.
            }

            return true;
        }
        /// <summary>
        /// Le toString va renvoyer les informations essentielles au joueur pour qu'il puisse reflechir et jouer
        /// concraitement.
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string result = $"Le joueur {Nom} a un score de {Score} et a deja trouvé \n {string.Join(",", ListeMotsTrouves)}";
            result += $"\n Ses lettres sont";
            for (int i = 0; i < ListeLettres.Count; i++)
            {
                if (ListeLettres[i].Symbole=='§') //mon JOKER a '§' comme symbole. Ca me permet de le differencier.
                {
                    result += " JOKER,";
                }
                else
                {
                    result += $" {ListeLettres[i].Symbole},";
                }
            }
            return result;
        }
        public void Add(string mot)
        {
            ListeMotsTrouves.Add(mot);
        }
        /// <summary>
        /// Otelettre est un verificateur qui m'autorise ou non a poser un mots. Cette methode prends en parametre
        /// Le nb de jokers d'un joueur et si il en a. Elle retire les lettres correspondants au mots posé.
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="ContientUnJoker"></param>
        /// <param name="NbJokers"></param>
        /// <returns></returns>
        public bool OteLettre(string mot, bool ContientUnJoker, int NbJokers, string[] Position) //enleve une lettre au joueur.
        {
            bool result = true;
            Lettre var = new Lettre(char.Parse("a"),0);
            int NombreLettresManquantes = 0;
            if(ContientUnJoker)
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    var.Symbole = mot[i];
                    int index = ListeLettres.FindIndex(x => x.Symbole == mot[i]); //un peu de linq qui m'est tres utile.
                    if (index > -1) { ListeLettres.RemoveAt(index); }
                    else
                    {
                        NombreLettresManquantes += 1;
                        int index1 = ListeLettres.FindIndex(x => x.Symbole == '§');
                        ListeLettres.RemoveAt(index1);
                    }
                    if (NombreLettresManquantes>NbJokers)
                    {
                        return false;
                    }
                }
                return true;
            }
            if(!ContientUnJoker)
            {
                List<Lettre> LettresEnlevees = new List<Lettre>(); //on remet les lettres si le mot nest pas valide
                for (int i = 0; i < mot.Length; i++)
                {
                    int index = ListeLettres.FindIndex(x => x.Symbole == mot[i]); //un peu de linq qui m'est tres utile.
                    if (index != -1)
                    {
                        LettresEnlevees.Add(ListeLettres[index]);
                        ListeLettres.RemoveAt(index);
                    }
                    else if (GrilleMatrice[int.Parse(Position[0])-1, int.Parse(Position[1])-1].Symbole==mot[i])
                    { 
                        //La lettre n'est pas dans le plateau du joueur car elle est deja dans la grille.
                    }
                    else
                    {
                        
                            for (int k = 0; k < LettresEnlevees.Count; k++)
                            {
                                ListeLettres.Add(LettresEnlevees[k]);
                            }
                            return false;
                        
                    }
                    

                }
                return result;
            }
            else
            {
                return true;
            }
        }
        public void AfficherGrille()
        {
            string[] GrilleVierge = //c'est le modele de grille que je voulais avoir
            {
            "       1  2  3  4  5  6  7  8  9  10 ",
            "_____________________________________",
            "1     |  |  |  |  |  |  |  |  |  |  |",
            "2     |  |  |  |  |  |  |  |  |  |  |",
            "3     |  |  |  |  |  |  |  |  |  |  |",
            "4     |  |  |  |  |  |  |  |  |  |  |",
            "5     |  |  |  |  |  |  |  |  |  |  |",
            "6     |  |  |  |  |  |  |  |  |  |  |",
            "7     |  |  |  |  |  |  |  |  |  |  |",
            "8     |  |  |  |  |  |  |  |  |  |  |",
            "9     |  |  |  |  |  |  |  |  |  |  |",
            "10    |  |  |  |  |  |  |  |  |  |  |",
            };
            
            for (int i = 1; i < 12; i++)
            {
                if (i==1)
                {
                    Console.WriteLine("       1   2   3   4   5   6   7   8   9   10  ");
                    Console.Write("_______________________________________________");
                }
                else
                {
                    for (int j = 7; j < 18; j++)
                    {
                        if(i==11)
                        {
                            if (j == 7)
                            {
                                Console.Write($"{i-1}    |");
                            }
                            else
                            {
                                Console.Write($"{GrilleMatrice[i - 2, j - 8].Symbole}  |");
                            }
                        }
                        else
                        {
                            if (j == 7)
                            {

                                Console.Write($"{i-1}     |");
                            }
                            else
                            {
                                Console.Write($"{GrilleMatrice[i - 2, j - 8].Symbole}  |");
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(this.Nom);
        }
        /// <summary>
        /// Calcule le score du joueur a chaque tour en fonction des mots qu'il a trouvé
        /// </summary>
        public void CalculScore()
        {
            Score = 0;
            for (int i = 0; i < ListeMotsTrouves.Count; i++)
            {
                for (int y = 0; y < ListeMotsTrouves[i].Length; y++)
                {
                    if (ListeMotsTrouves[i][y] == 'X' || ListeMotsTrouves[i][y] == 'Y' || ListeMotsTrouves[i][y] == 'Z' || ListeMotsTrouves[i][y] == 'W' || ListeMotsTrouves[i][y] == 'K')
                    {
                        Score += 5;
                    }
                    if (ListeMotsTrouves[i].Length>4)
                    {
                        Score += ListeMotsTrouves[i].Length;
                    }
                }
            }
        }
        /// <summary>
        /// Si on veut poser un mots on passe par cette méthode qui verifie si le mots est un mots du dico
        /// si celui ci est un motscroisée valable
        /// si les lettres correpondantes au mots sont des lettre du joueur.
        /// </summary>
        /// <param name="mots"></param>
        /// <param name="Position"></param>
        /// <param name="UnMot"></param>
        /// <param name="Jeu1"></param>
        /// <param name="r"></param>
        public void PoserUnMot(string mots, string[] Position, MotsCroises UnMot, Jeu Jeu1, Random r) //a partir de l'entree mettre une lettre dans la grille.
        {
                Lettre JOKER = new Lettre('§', 0); //C'est mon joker
                bool ContientUnJoker = false;
                string NouveauMots = "";
                int NbJokers = 0;
                for (int o = 0; o < 2; o++) //au cas ou la personne ait 2 jokers On verfie Deux fois
                {
                    for (int l = 0; l < mots.Length; l++)
                    {
                        if (mots[l] == '(') //si j'utilises un joker je mets des () autour de la lettre remplacée
                        {
                            if (mots[l + 2] == ')')
                            {
                                for (int z = 0; z < mots.Length; z++) //il y a un joker
                                {
                                    if (z != l && z != l + 2)//on reforme le mots sans les ().
                                    {
                                        NbJokers += 1;
                                        ContientUnJoker = true;
                                        NouveauMots += mots[z]; 
                                    }
                                }
                                mots = NouveauMots; //pour ne pas avoir les ().
                            }
                        }
                    }
                } // on verfie les Jokers
                if (ContientUnJoker) { mots = NouveauMots; }
                UnMot.MotsGrille.Add(mots);

           

            //une fonction pour verifier que le mot est bon
            bool result =OteLettre(mots, ContientUnJoker, NbJokers, Position); //3 verifications ici.
            if (UnMot.EstUnMotCroisee(mots, Position) && UnMot.EstUnMotDuDictionnaire() && result)
            {
                
                int poid; //correspond au poid de la lettre en question
                int PoidMots = 0; //corresponds poid du mots
                Console.WriteLine("le mots est " + mots);
                for (int i = 0; i < mots.Length; i++)
                {
                    if (mots[i]=='X' || mots[i] == 'Y' || mots[i] == 'Z' || mots[i] == 'W' || mots[i] == 'K')
                    {
                        poid = 5;
                    }
                    else { poid = 0; }
                    Lettre UneLettre = new Lettre(mots[i], poid);
                    PoidMots += poid;
                    if (Position[2].ToUpper() == "H")//on pose le mots sur la grille
                    {
                        GrilleMatrice[int.Parse(Position[0]) - 1, int.Parse(Position[1]) - 1 + i] = UneLettre;
                    }
                    if (Position[2].ToUpper() == "V")
                    {
                        GrilleMatrice[int.Parse(Position[0]) - 1 + i, int.Parse(Position[1]) - 1] = UneLettre;
                    }
                }
                Console.WriteLine($"Le poid du mot est {PoidMots}");
                List<string> Mots = new List<string>();
                Mots.Add(mots);
                ListeMotsTrouves.Add(mots);
                CalculScore();
            }
            else
            {
                Console.WriteLine("ERREUR => NON POSSIBLE"); //une des 3 verifications a renvoyé false
                UnMot.MotsGrille.Remove(mots); //on actualise
                ListeMotsTrouves.Remove(mots);
            }
            Add_Lettres(2, Jeu1.LaPioche, r); //un tour est effectué, on ajoute deux lettre au joueur en cours
            Console.WriteLine($"Nouveau Score = {this.Score}");
                //si le mots est valable
             AfficherGrille();
        }
    }
}
