using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal_BLANQUE_Bertrand
{
    /// <summary>
    /// La classe Lettre est tres basique et se retrouve partout dans le code. Un joueur aura a une matrice
    /// de lettre pour representer sa grille. Chaque mot dans le probleme sera une liste de Lettre.
    /// Une lettre est composé de son Symbole, son poid et de son constructeur.
    /// Aucune méthode n'a besoin d'etre dans cette classe.
    /// </summary>
    public class Lettre
    {
        public char Symbole { get; set; }
        public int Poid { get; set; }
        public Lettre(char symbole, int poid)
        {
            Symbole = symbole;
            Poid = poid;
        }

    }
}
