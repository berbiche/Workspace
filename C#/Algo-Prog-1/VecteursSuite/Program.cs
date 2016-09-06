using System;

namespace VecteursSuite
{
    class Program
    {

        // afficher le vecteur
        static void affvecteur(int[] vect)
        {

            int I;
            for (I = vect.GetLowerBound(0); I <= vect.GetUpperBound(0); I++)
            {
                Console.Write(vect[I] + " ");
            }
            Console.WriteLine();
            Console.ReadLine();
        } // fin affvecteur

        // insérer nombre à la position n
        static void InsererPosN(ref int[] vect, int nb1, int pos1)
        {
            pos1 = pos1 < 0 ? 0 : pos1 > vect.Length ? vect.Length : pos1;
            Array.Resize(ref vect, vect.Length + 1);
            for (int i = vect.Length - 1; i > pos1; i--)
                vect[i] = vect[i - 1];
            vect[pos1] = nb1;
        }// fin insérer nombre

        // retirer
        static void RetirerPosN(ref int[] vect, int pos1)
        {
            if (pos1 > -1 && pos1 < vect.Length)
            {
                int[] vecteur = new int[vect.Length];
                for (int i = 0; i < vect.Length; i++)
                    vecteur[i] = vect[i];
                for (int i = pos1; i < vect.Length - 1; i++)
                {
                    vect[i] = vecteur[i + 1];
                }
                Array.Resize(ref vect, vect.Length - 1);
            }
            else
                Console.WriteLine("Aucun élément retiré");
        }

        static void TrierVecCroissant(ref int[] vect)
        {
            int nbTemp = 0;
            for (int i = 0; i < vect.Length; i++)
                for (int j = i + 1; j < vect.Length; j++) //on compare le nombre i au nombre suivant
                    if (vect[i] > vect[j])
                    {
                        nbTemp = vect[i]; // on place i dans le nombre temporaire puis on donne la valeur de la case supérieure à la case inférieure (donc ordre croissant) qui est plus faible avant de donner le nombre temporaire à la case j qui est plus haut dans l'index (et donc dans l'ordre)
                        vect[i] = vect[j];
                        vect[j] = nbTemp;
                    }
        }

        static void Inserer(ref int[] vect, int nb1)
        {
            Array.Resize(ref vect, vect.Length + 1);
            int index = 0;
            for (int i = 0; i < vect.Length - 1; i++)
                if (nb1 > vect[i])
                    index = i;
            for (int i = vect.GetUpperBound(0); i > index; i--)
                vect[i] = vect[i - 1];
            if (index != 0)
                vect[index + 1] = nb1;
            else
                vect[index] = nb1;
        }

        static void Retirer(ref int[] vect, int nb1)
        {
            bool present = false;
            for (int i = 0; i < vect.Length; i++)
                if (vect[i] == nb1)
                    present = true;
            if (present)
            {
                int index = 0;
                for (int i = 0; i < vect.Length; i++) //cette boucle va retirée le dernier # dans le cas où il apparait plusieurs fois
                    if (vect[i] == nb1)
                        index = i;
                for (int i = index; i < vect.Length - 1; i++)
                    vect[i] = vect[i + 1];
                Array.Resize(ref vect, vect.Length - 1);
            }
            else
                Console.WriteLine("Le nombre n'est pas présent");
        }

        static int PremierSupN(int[] vect, int val1)
        {
            bool nombreSuperieur = false;
            int rep = 0, m = 0;
            while (nombreSuperieur != true && m < vect.Length - 1)
            {
                if (vect[m] > val1)
                {
                    nombreSuperieur = true;
                    rep = vect[m];
                }
                m++;
            }
            if (nombreSuperieur)
                return rep;
            else
                return -1;
        }
        static void Main()
        {

            int N;  //Intrant
            int PosN;  //Position de N : indice de N

            int[] V = { 23, 42, 6, 88, 12, 23, 31, 67, 101, 14, 25, 29, 12, 17, 50 };

            Console.WriteLine();
            Console.WriteLine("Tableau d'entiers : éléments du vecteur V");
            affvecteur(V);

            Console.WriteLine();
            Console.WriteLine("RetirerPosN Test");
            RetirerPosN(ref V, 0);
            affvecteur(V);

            //Console.WriteLine();
            //N = 35;
            //PosN = 5;
            //Console.WriteLine("Problème 8 - Insérer " + N + " à la position " + PosN);
            //InsererPosN(ref V, N, PosN);
            //affvecteur(V);

            //Console.WriteLine();
            //N = 77;
            //PosN = 19;
            //Console.WriteLine("Problème 8 - Insérer " + N + " à la position " + PosN);
            //InsererPosN(ref V, N, PosN);
            //affvecteur(V);

            //Console.WriteLine();
            //N = 88;
            //PosN = -1;
            //Console.WriteLine("Problème 8 - Insérer " + N + " à la position " + PosN);
            //InsererPosN(ref V, N, PosN);
            //affvecteur(V);

            //Console.WriteLine();
            //PosN = 3;
            //Console.WriteLine("Problème 9 - Retirer l'élément à la position " + PosN);
            //RetirerPosN(ref V, PosN); //Retirer l'élément à la position PosN
            //affvecteur(V);

            //Console.WriteLine();
            //PosN = -1;
            //Console.WriteLine("Problème 9 - Retirer l'élément à la position " + PosN);
            //RetirerPosN(ref V, PosN); //Retirer l'élément à la position PosN
            //affvecteur(V);

            //Console.WriteLine();
            //PosN = 29;
            //Console.WriteLine("Problème 9 - Retirer l'élément à la position " + PosN);
            //RetirerPosN(ref V, PosN); //Retirer l'élément à la position PosN
            //affvecteur(V);


            //Console.WriteLine();
            //Console.WriteLine("Problème 10 - Trier  et afficher le vecteur V");
            //Console.WriteLine("Vecteur après le tri");
            //TrierVecCroissant(ref V);
            //affvecteur(V);

            //Console.WriteLine();
            //N = 26; //Insérer 26 à sa position
            //Console.WriteLine("Problème 11 - Insérer " + N + " à sa position");
            //Inserer(ref V, N);
            //Console.WriteLine("Vecteur après insertion de : " + N);
            //affvecteur(V);

            //Console.WriteLine();
            //N = 222; //Insérer 222 à sa position, à la fin
            //Console.WriteLine("Problème 11 - Insérer " + N + " à sa position");
            //Inserer(ref V, N);
            //Console.WriteLine("Vecteur après insertion de : " + N);
            //affvecteur(V);

            //Console.WriteLine();
            //N = 3; //Insérer 3 à sa position, au début
            //Console.WriteLine("Problème 11 - Insérer " + N + " à sa position");
            //Inserer(ref V, N);
            //Console.WriteLine("Vecteur après insertion de : " + N);
            //affvecteur(V);

            //N = 29; //Retirer l'élément 29
            //Console.WriteLine();
            //Console.WriteLine("Problème 12 - Retirer l'élément " + N);
            //Retirer(ref V, N); //Retirer l'élément N
            //affvecteur(V);

            //N = 68; //Retirer l'élément 68 non présent
            //Console.WriteLine();
            //Console.WriteLine("Problème 12 - Retirer l'élément " + N);
            //Retirer(ref V, N); //Retirer l'élément N
            //affvecteur(V);

            //N = 77; //retourner le premier élément supérieur à 77
            //Console.WriteLine();
            //Console.WriteLine("Problème 13 Premier élément supérieur à " + N);
            //int PremierSup;
            //PremierSup = PremierSupN(V, N); //Si aucune trouvé retourne -1
            //if (PremierSup < 0)
            //    Console.WriteLine("Aucun trouvé.");
            //else
            //    Console.WriteLine("Premier supérieur à " + N + " est " + PremierSup);


            //N = 333; //retourner le premier élément supérieur à 333 (non présent)
            //Console.WriteLine();
            //Console.WriteLine("Problème 13 Premier élément supérieur à " + N);
            //PremierSup = PremierSupN(V, N); //Si aucune trouvé retourne -1
            //if (PremierSup < 0)
            //    Console.WriteLine("Aucun trouvé.");
            //else
            //    Console.WriteLine("Premier supérieur à " + N + " est " + PremierSup);


            Console.ReadLine();
        }
    }
}

