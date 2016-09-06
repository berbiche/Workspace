using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_structures
{
    class Program
    {
        struct heure
	        {
	        public int heures ;
	        public int minutes ;
	        public int secondes ;
	        } 

        struct date
	        {
	        public int annee;
	        public string mois;
	        public int jour;
            public heure heureprecise;
	        }


        static void affichestruct(date ladate)
        {
            Console.WriteLine("Année    : {0}",ladate.annee);
            Console.WriteLine("Mois     : {0}", ladate.mois);
            Console.WriteLine("Jour     : {0}", ladate.jour);
            Console.WriteLine("Heure    : {0}", ladate.heureprecise.heures);
            Console.WriteLine("Minute   : {0}", ladate.heureprecise.minutes);
            Console.WriteLine("Seconde  : {0}", ladate.heureprecise.secondes);
            Console.WriteLine("\n\n");
        }


        static void Main(string[] args)
        {
            //déclaration de deux "date"

            date datenaissancePaul, datenaissanceJulie;

            //Exemples d'accès aux champs

            datenaissancePaul.annee = 1992;
            datenaissancePaul.mois = "Septembre";
            datenaissancePaul.jour = 17;
            datenaissancePaul.heureprecise.heures = 6;
            datenaissancePaul.heureprecise.minutes = 23;
            datenaissancePaul.heureprecise.secondes = 11;

            datenaissanceJulie.annee = 1988;
            datenaissanceJulie.mois = "Novembre";
            datenaissanceJulie.jour = 12;
            datenaissanceJulie.heureprecise.heures = 9;
            datenaissanceJulie.heureprecise.minutes = 35;
            datenaissanceJulie.heureprecise.secondes = 42;

            //affichage
            Console.WriteLine("Voici les informations de Paul :");
            Console.WriteLine("================================");
            affichestruct(datenaissancePaul);

            Console.WriteLine("Voici les informations de Julie :");
            Console.WriteLine("================================");
            affichestruct(datenaissanceJulie);

            Console.WriteLine("\n\nTapez une touche pour continuer");
            Console.WriteLine("Attention : on va écraser les informations de Paul avec celles de Julie\n\n");

            Console.ReadKey();

            //écrasement des informations de Paul
            datenaissancePaul = datenaissanceJulie;

            Console.WriteLine("Voici les informations de Paul :");
            Console.WriteLine("================================");
            affichestruct(datenaissancePaul);

            Console.WriteLine("Voici les informations de Julie :");
            Console.WriteLine("================================");
            affichestruct(datenaissanceJulie);
        }
    }
}
