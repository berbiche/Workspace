using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] monTab = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
            int i = 0, j = 0;
            bool Trouve = false;
            while ((i < monTab.Length) && (Trouve == false))
            {
                j = i;
                while ((j < monTab.Length - 1) && (Trouve == false))
                {
                    j = j + 1;
                    if(Trouve = (monTab[i] == monTab[j]))
                        Console.WriteLine("La valeur de j = {0} et i = {1}",j,i);
                    Console.WriteLine("i = " + i + "\nj = " + j);
                }
                i++;
            }
            
        }
    }
}
