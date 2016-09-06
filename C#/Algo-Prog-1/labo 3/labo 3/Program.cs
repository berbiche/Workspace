using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labo_3
{
    class Program
    {
        // Affiche la matrice
        static void AffMatrice(int[,] matrice)
        {
            int ligne, colonne;
            for (ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    Console.Write("{0}  ", matrice[ligne, colonne]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadKey();
        } // fin AffMatrice

        // Initialise la matrice avec le numéro de ligne pour chaque colonne
        static void InitMatriceNumLigne(ref int[,] matrice) // puisque 1 seul paramètre est demandé je vais assigner le même nombre de colonne et de ligne
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    matrice[ligne, colonne] = ligne + 1;
                }
            }
        } // fin InitMatriceNumLigne

        // Initialise la matrice avec le numéro de colonne pour la ligne diagonale de façon décroissant, le reste est rempli 0
        static void InitMatriceAntiDiag(ref int[,] matrice)
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if (colonne == matrice.GetLength(0) - 1 - ligne)
                        matrice[ligne, colonne] = matrice.GetLength(0) - ligne;
                    else
                        matrice[ligne, colonne] = 0;
                }
            }
        } // fin InitMatriceAntiDiag

        // Initialise la matrice avec le numéro de colonne pour la ligne diagonale de façon croissant, le reste étant rempli de 0
        static void InitMatriceDiag(ref int[,] matrice)
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if (ligne == colonne)
                        matrice[ligne, colonne] = ligne + 1;
                    else
                        matrice[ligne, colonne] = 0;
                }
            }
        } // fin InitMatriceDiag

        // Initialise une matrice dont les valeurs sous la diagonale sont de zéro, et les autres valeurs sont égales au numéro de la colonne
        static void InitMatriceTriangSup(ref int[,] matrice)
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if (colonne + 1 > ligne)
                        matrice[ligne, colonne] = colonne + 1;
                    else
                        matrice[ligne, colonne] = 0;
                }
            }
        } // fin InitMatriceTriangSup

        // Initialise une matrice opposée à InitMatriceTriangSup, les données au-dessus de la diagonale sont égales à 0

        static void InitMatriceTriangInf(ref int[,] matrice)
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if (colonne < ligne + 1)
                        matrice[ligne, colonne] = Convert.ToInt32(string.Concat((ligne + 1) + "" + (colonne + 1)));
                    else
                        matrice[ligne, colonne] = 0;
                }
            }
        } // fin InitMatriceTriangInf
        static void Main()
        {
            int dimTab;
            Console.WriteLine("Quel sera la taille du tableau?");
            do{
                int.TryParse(Console.ReadLine(), out dimTab);
                if (dimTab < 0)
                    Console.WriteLine("Entrez une valeur supérieure à 0");
            } while (dimTab < 0);
            int[,] Mat = new int[dimTab, dimTab];
            AffMatrice(Mat);

            Console.WriteLine("Exercice 1\n");
            InitMatriceNumLigne(ref Mat);
            AffMatrice(Mat);

            Console.WriteLine("Exercice 2\n");
            InitMatriceAntiDiag(ref Mat);
            AffMatrice(Mat);

            Console.WriteLine("Exercice 3\n");
            InitMatriceDiag(ref Mat);
            AffMatrice(Mat);

            Console.WriteLine("Exercice 4\n");
            InitMatriceTriangSup(ref Mat);
            AffMatrice(Mat);

            Console.WriteLine("Exercice 5\n");
            InitMatriceTriangInf(ref Mat);
            AffMatrice(Mat);
        }
    }
}
