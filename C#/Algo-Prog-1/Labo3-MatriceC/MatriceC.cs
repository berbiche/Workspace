using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo3_MatriceB_C
{
	class MatriceC
	{
		static void Main()
		{
			Console.WriteLine("Quelle taille de tableau?");
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
            int ligne, colonne, compteur = 1, x = 0, y = 0;
			for (ligne = x; ligne < matrice.GetLength(0); ligne++)
			{
				for (colonne = y; colonne < matrice.GetLength(1); colonne++)
				{
                    if ((ligne == x || ligne == matrice.GetLength(0) - 1 * compteur) || (colonne == y || colonne == matrice.GetLength(1) - 1 * compteur))
                        matrice[ligne, colonne] = 1 * compteur; //s'occupe de remplir le contour du carré avec des 1
                    //else
                    //    matrice[ligne, colonne] = 1 * compteur;
                }
                if ((ligne == matrice.GetLength(0) - 1) && (colonne == matrice.GetLength(1) - 1))
                {
                    ligne = x;
                    x++;
                    y++;
                    compteur++;
                }
			}
		}
	}
}