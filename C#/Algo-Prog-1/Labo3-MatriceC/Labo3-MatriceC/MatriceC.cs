using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo3_MatriceC
{
    class MatriceC
    {
        static void Main()
        {
            Console.WriteLine("Quelle est la taille du tableau?");
            int dimension = int.TryParse(Console.ReadLine(), out dimension) ? dimension : 5;
            int[,] matrice = new int[dimension, dimension];
            AffMatrice(matrice);
            //Exercice 11
            Console.WriteLine("Exercice 11");
            InitMatricePourtour(ref matrice);
            AffMatrice(matrice);
            //Exercice 12
            Console.WriteLine("Exercice 12");
            InitMatriceCible(ref matrice);
            AffMatrice(matrice);
            //Exercice 13
            Console.WriteLine("Exercice 13");
            InitMatriceCoins(ref matrice);
            AffMatrice(matrice);
            //Exercice 14
            Console.WriteLine("Exercice 14\nNombre pour X?");
            int X = int.TryParse(Console.ReadLine(), out X) ? X : 5;
            InitMatriceX(ref matrice, X);
            AffMatrice(matrice);
            //Exercice 15
            Console.WriteLine("Exercice 15");
            AffVecteur(MatriceVecteurReduction(matrice)); //transpose la matrice dans une matrice unidimensionnelle
            Console.ReadLine();
        }

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
        }

        static void InitMatricePourtour(ref int[,] matrice)
        {
            int x = 1;
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    if ((ligne == 0 || ligne == matrice.GetLength(0) - 1) || (colonne == 0 || colonne == matrice.GetLength(1) - 1))
                    {
                        matrice[ligne, colonne] = x;
                        x++;
                    }
                    else
                        matrice[ligne, colonne] = 0;
                    //valeur = (ligne = 0) ? x : 0 : (ligne = (matrice.GetLength(0) - 1)) ? x : 0 : (colonne = 0) ? (ligne > 0) ? x : 0 : 0;
                    //matrice[ligne, colonne] = valeur;
                }
            }
        }

        static void InitMatriceCible(ref int[,] matrice)
        {
            int z, compteur = 1, x = 0, y = matrice.GetLength(0) - 1;
            do
            {
                for (z = x; z <= y; z++)
                {
                    matrice[z, y] = compteur;
                    //Console.WriteLine("z:{0} x:{1} y:{2}, matrice[{0},{2}]:{3}", z, x, y, matrice[z,y]);
                    matrice[y, z] = compteur;
                    //Console.WriteLine("z:{0} x:{1} y:{2}, matrice[{2},{0}]:{3}", z, x, y, matrice[y, z]);
                    matrice[z, x] = compteur;
                    //Console.WriteLine("z:{0} x:{1} y:{2}, matrice[{0},{1}]:{3}", z, x, y, matrice[z, x]);
                    matrice[x, z] = compteur;
                    //Console.WriteLine("z:{0} x:{1} y:{2}, matrice[{1},{0}]:{3}", z, x, y, matrice[x, z]);
                }
                compteur++;
                x++;
                y = y - 1;
            } while (x <= y);
        }
        /// <summary>
        /// J'ai essayé de simplifier cet exercice, mais il n'y avait aucun "pattern" qui revenait
        /// </summary>
        /// <param name="matrice"></param>
        static void InitMatriceCoins(ref int[,] matrice)
        {
            //malheureusement, lorsque mon tableau possède une taille de 2 il n'y a aucun nombre dans les coins parcequ'il n'y a pas d'espace entre les coins.
            if (matrice.GetLength(0) == 2)
            {
                int compteur = 0;
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < matrice.GetLength(0); j++)
                    {
                        matrice[i, j] = compteur;
                        compteur++;
                    }
                }
            }
            else
            {
                for (int l = 0; l < matrice.GetLength(0); l++)
                {
                    for (int m = 0; m < matrice.GetLength(1); m++)
                    {
                        matrice[l, m] = 0;
                    }
                }
                double nombre = matrice.GetLength(0);
                int w = 0;
                if (nombre % 2 != 0)
                    nombre = Math.Floor(nombre / 2);
                else
                {
                    nombre = nombre / 2;
                    w = 1;
                }
                for (int i = 0; i < nombre - w; i++)
                {
                    for (int j = 0; j < nombre - w; j++)
                    {
                        matrice[i, j] = 1;
                    }
                }
                for (int i = (int)nombre + 1; i < matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < nombre - w; j++)
                    {
                        matrice[i, j] = 2;
                    }
                }
                for (int i = 0; i < nombre - w; i++)
                {
                    for (int j = matrice.GetLength(0) - 1; j > nombre; j = j - 1)
                    {
                        matrice[i, j] = 3;
                    }
                }
                for (int i = matrice.GetLength(0) - 1; i > nombre; i = i - 1)
                {
                    for (int j = matrice.GetLength(0) - 1; j > nombre; j = j - 1)
                    {
                        matrice[i, j] = 4;
                    }
                }
            }

        }
        /// <summary>
        /// 1.Prend en entrée une matrice dont l'affichage sera d'abord remplacé par des zéros (le code est plus rapide qu'avec une condition)
        /// 2.Change la croix du centre pour la valeur <i>n</i>
        /// 3.Si aucune valeur <i>n</i> est reçu alors la valeur sera de 99 par défaut
        /// </summary>
        /// <param name="matrice"></param>
        /// <param name="n"></param>
        static void InitMatriceX(ref int[,] matrice, int n = 99)
        {
            for(int k = 0; k < matrice.GetLength(0); k++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    matrice[k, j] = 0;
                }
            }
            int x = 0, i = 0, y = matrice.GetLength(1) - 1;
            while (x <= y) {
                for (x = i; x < i + 1; x++)
                {
                    matrice[x, x] = n;
                    //Console.WriteLine("x:{0} y:{1}, matrice[{0},{0}]:{2}", x, y, matrice[x, x]);
                    matrice[x, y] = n;
                    //Console.WriteLine("x:{0} y:{1}, matrice[{0},{1}]:{2}", x, y, matrice[x, y]);
                    matrice[y, y] = n;
                    //Console.WriteLine("x:{0} y:{1}, matrice[{1},{1}]:{2}", x, y, matrice[y, y]);
                    matrice[y, x] = n;
                    //Console.WriteLine("x:{0} y:{1}, matrice[{1},{0}]:{2}", x, y, matrice[y, x]);
                }
                y = y - 1;
                i++;
            }
        }

        static int[] MatriceVecteurReduction(int[,] matrice)
        {
            var x = matrice.GetLength(0);
            int[] tableau = new int[x*x/*(int)Math.Pow(matrice.GetLength(0), 2)*/];
            int ligne, colonne, i = 0;
            for (ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    tableau[i] = matrice[ligne, colonne];
                    i++;
                }
            }
            return tableau;
        }

        static void AffVecteur (int[] matrice) //affiche la matrice unidimensionnelle
        {
            for( int i = 0; i < matrice.Length; i++)
            {
                Console.Write("{0}  ", matrice[i]);
            }
            Console.WriteLine();
        }
    }
}