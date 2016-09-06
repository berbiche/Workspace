using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercice_01_420_D12
{
    class Program
    {
        static void modifvar(ref int var)
        {
            var = 4;
        }

        static void modifvar2(ref int var)
        {
            var = 5;
            modifvar(ref var);
        }
        static void modifvar3(int var)
        {
            var = 5;
            modifvar(ref var);
        }

        static void modifvar4(ref int var)
        {
            var = 5;
            modifvar(ref (var));
        }

        static void ModifVec1(int[] V)
        {
            int k;

            int[] VV = { 1111, 2222, 3333, 4444 };

            for (k = 0; k <= V.GetUpperBound(0); k++)
            {
                V[k] = V[k] * 2;
            }

            V = VV;
        }

        static void ModifVec2(ref int[] V)
        {
            int k;

            int[] VV = { 111, 222 };

            for (k = 0; k <= V.GetUpperBound(0); k++)
            {
                V[k] = V[k] * 3;
            }

            V = VV;

        }

        static void AffVec(int[] V)
        {
            int k;
            for (k = 0; k <= V.GetUpperBound(0); k++)
            {
                Console.Write(" {0},", V[k]);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int x = 3;
            int[] VecA = { 1, 2, 3, 4 };
            int[] VecB = { 10, 20, 30 };

            modifvar(ref x);
            Console.WriteLine();
            Console.Write("Problème 1: x = {0}", x);
            Console.WriteLine();

            x = 3;
            modifvar(ref (x));
            Console.Write("Problème 2: x = {0}", x);
            Console.WriteLine();

            x = 3;
            modifvar2(ref x);
            Console.Write("Problème 3: x = {0}", x);
            Console.WriteLine();

            x = 3;
            modifvar3(x);
            Console.Write("Problème 4: x = {0}", x);
            Console.WriteLine();

            x = 3;
            modifvar4(ref x);
            Console.Write("Problème 5: x = {0}", x);
            Console.WriteLine();

            Console.Write("VecA : ");
            ModifVec1(VecA);
            AffVec(VecA);

            Console.Write("VecB : ");
            ModifVec2(ref VecB);
            AffVec(VecB);
            Console.ReadKey();
        }
    }
}
