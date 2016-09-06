using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo2_debogueur
{
    class Program
    {

        //Ce programme comporte plusieurs bogues 
        //Les conversions effectuées sont incorrectes
        //Les valeurs affichées sont incorrectes
        //Le programme plante aussi sur une entrée usager invalide : Pouvez-vous corriger?
        public static Func<float, float> degre_fahrenheit = (x) => x = x * 9 / 5 + 32; //fonction anonyme qui retourne la température de celcius à fahrenheit
        public static Func<float, float> degre_celcius = (x) => x = (x - 32) * 5 / 9; //fonction anonyme qui retourne la température de fahrenheit à celcius

        static void Main(string[] args)
        {
            while (true)
                {
                Console.Write("Entrez un degré (q = quitter): ");
                string strdegre = Console.ReadLine();
                if (strdegre == "q")
                    break;
                float degre;
                float.TryParse(strdegre, out degre);
                Console.WriteLine("{0} degré celsius, c'est {1:##.##} degré fahrenheit", degre, degre_fahrenheit(degre));
                Console.WriteLine("{0} degré fahrenheit, c'est {1:##.##} degré celsius", degre, degre_celcius(degre));
                }
        }
    }
}
