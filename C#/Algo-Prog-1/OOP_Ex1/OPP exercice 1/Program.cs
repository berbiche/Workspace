using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPP_exercice_1
{
    class Program
    {
        public static List<Personne> liste_des_personnes = new List<Personne>
            {
                new Personne("Daniel", "Tremblay", "007-007-007",Personne.Sexe.homme, "Montréal", new DateTime(1976,5,21), Personne.EtatCivile.célibataire, "Maître"),
                new Personne("Alice", "Champagne", "123-456-789", Personne.Sexe.femme, "Saint-Lambert", new DateTime(1980, 9, 5), Personne.EtatCivile.célibataire, qualificatif:"Bachelière"),
                new Personne("Paul","Arcand", "987-654-321",Personne.Sexe.homme, "Laval", new DateTime(1977, 1, 28), Personne.EtatCivile.célibataire)
            };
        static void Main()
        {
            foreach (Personne qqc in liste_des_personnes)
            {
                Console.WriteLine(qqc.retournerInfo());
            }
            int personne = int.TryParse(Console.ReadLine(), out personne) ? (personne >= 0 && personne < liste_des_personnes.Count) ? personne : 0 : 0;
            Console.WriteLine(liste_des_personnes[personne].retournerInfo() + "\nCette personne est âgée de " + liste_des_personnes[personne].âge() + " ans.");
            Console.Write("\nMarier une personne:\nEntrez le nom complet (prénom nom) : ");
            string s = Console.ReadLine();
            Console.Write("\nEntrez la seconde personne : ");
            string w = Console.ReadLine();
            if (personneExiste(s) && personneExiste(w))
            {
                liste_des_personnes.ElementAt(liste_des_personnes.FindIndex(x => x.Prenom + " " + x.Nom == s)).Conjoint = w;
                liste_des_personnes.ElementAt(liste_des_personnes.FindIndex(x => x.Prenom + " " + x.Nom == w)).Conjoint = s;
                Console.WriteLine("{0} et {1} sont désormais mariés.", s, w);
            }
            else
                Console.WriteLine("Une ou plusieurs de ces personnes n'existe pas dans le registre");
            Console.WriteLine(liste_des_personnes[personne].retournerInfo() + "\nCette personne est âgée de " + liste_des_personnes[personne].âge() + " ans.");
        }
        
        static bool personneExiste(string s)
        {
            if (liste_des_personnes.Any(x => x.Prenom + " " + x.Nom == s))
                return true;
            else
                return false;
        }
    }
}
