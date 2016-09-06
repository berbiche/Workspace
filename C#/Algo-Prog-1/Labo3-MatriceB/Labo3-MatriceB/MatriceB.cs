using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo3_MatriceB
{
    class MatriceB
    {
        // Affiche la matrice
        static void AffMatrice(int[,] matrice)
        {
            int ligne, colonne;
            for (ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    Console.Write("{0:00}  ", matrice[ligne, colonne]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadKey();
        } // fin AffMatrice

        // Affiche le triangle de pascal
        static void InitTrianglePascal(ref int[,] matrice)
        {
            if (matrice.GetLength(0) > 1)
                matrice[1, 1] = 1;
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if (ligne > 1 && colonne != 0)
                        matrice[ligne, colonne] = matrice[ligne - 1, colonne - 1] + matrice[ligne - 1, colonne];
                    else
                    {
                        matrice[ligne, 0] = 0;
                        matrice[0, colonne] = 0;
                    }

                }
            }
        } // fin InitTrianglePascal

        // Initialise la matrice à partir de 1 et fait plus 1;
        static void InitMatriceContinu(ref int[,] matrice)
        {
            int x = 0;
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    x++;
                    matrice[ligne, colonne] = x;
                }
            }
        } // fin InitMatriceContinu

        // Affiche la somme de chaque colonne et chaque ligne
        static void AffMatriceSum(int[,] matrice)
        {
            Console.WriteLine("Affiche la somme des lignes et colonnes");
            int[] qqc = new int[matrice.GetLength(0)];
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                int sommeLig = 0, sommeCol = 0; ;
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    Console.Write("{0:00}  ", matrice[ligne, colonne]);
                    sommeLig += matrice[ligne, colonne];
                    sommeCol += matrice[colonne, ligne];
                }
                qqc[ligne] = sommeCol;
                Console.Write("   {0:000}\n", sommeLig);
            }
            Console.WriteLine();
            for (int i = 0; i < qqc.Length; i++)
                Console.Write("{0:00}  ", qqc[i]);
            Console.WriteLine("\n");
        } // Fin InitMatriceHasard

        // Initialise une matrice avec des nombres au hasard
        static void InitMatriceHasard(ref int[,] matrice)
        {
            Console.WriteLine("Initialise une matrice avec des données au hasard");
            Random qqc = new Random();
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    matrice[ligne, colonne] = qqc.Next(1, 100);
                }
            }
        } // Fin InitMatriceHasard

        // Retourne le minimum d'une matrice
        static int MinMatrice(int[,] matrice)
        {
            int minimum;
            if (matrice.GetLength(0) == 0)
                return 0;
            else
                minimum = 100;
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    if (matrice[ligne, colonne] < minimum)
                        minimum = matrice[ligne, colonne];
                }
            }
            return minimum;
        } // Fin MinMatrice

        // Pivote les valeurs d'une matrice sur sa diagonale
        static void MatriceTransposer(ref int[,] matrice)
        {
			int x = 0, y = -1;
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                y++;
                for (int colonne = y; colonne < matrice.GetLength(1); colonne++)
                {
					x = matrice[ligne, colonne];
					matrice[ligne, colonne] = matrice[colonne, ligne];
					matrice[colonne, ligne] = x;
                }
            }
        } // Fin MatriceTransposer
        static void Main()
        {
            Console.WriteLine("Quelle sera la taille de la matrice?");
            int dimension = int.TryParse(Console.ReadLine(), out dimension) ? dimension > -1 ? dimension : 5 : 5; //pour éviter que le programme plante, si dimension < 0 ou null alors valeur par défaut de 5, sinon dimension = nombre entré
            int[,] matrice = new int[dimension, dimension];
            Console.WriteLine("Voici la matrice de taille {0}:", dimension);
            AffMatrice(matrice);
            //Exerice 6
            Console.WriteLine("Exercice 6");
            InitTrianglePascal(ref matrice);
            AffMatrice(matrice);
            //Exercice 7
            Console.WriteLine("Exercice 7");
            InitMatriceContinu(ref matrice);
            AffMatrice(matrice);
            //Exercice 8
            Console.WriteLine("Exercice 8");
            AffMatriceSum(matrice);
            //Exercice 9
            Console.WriteLine("Exercice 9");
            InitMatriceHasard(ref matrice);
            AffMatrice(matrice);
            Console.WriteLine("Minimum de la matrice avec des données au hasard\nMin : {0}\n", MinMatrice(matrice));
            Console.ReadKey();
            //Exercice 10
            Console.WriteLine("Exercice 10\nTableau avant la transposition");
            AffMatrice(matrice);
            MatriceTransposer(ref matrice);
            Console.WriteLine("Tableau après la transposition");
			AffMatrice(matrice);
        }
    }
}
