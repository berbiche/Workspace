using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboStructure
{
    class Program
    {
        // *******declaration des structures****** 
        struct SUneHeureMin
        {
            public int HEU;
        }


        struct SUnHoraire
        {
            public SUneHeureMin HeureDeb;
            public SUneHeureMin HeureFin;
        }


        struct SUnEmploye
        {
            public int codeEmp;
            public string NomEmp;
            public decimal Salaire; //salaire = heuretotal * 15.6
            public int Heuretotal; //heuretotal travaillée par la personne en 5 jours
            public SUnHoraire[] HeuresTrav;
        }

        private const int NbJours = 5;
        private const decimal salaireHoraire = 15.16m;

        // ***** les méthodes *****

        static string AffMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Menu:");
            Console.WriteLine("1) Entrer les heures d'un employé");
            Console.WriteLine("2) Résumé des gains salariaux");
            Console.WriteLine("3) Afficher heures d'un employé");
            Console.WriteLine("q) Quitter");

            Console.Write("Votre choix : ");

            string choix = Console.ReadLine();
            return choix;
        }

        // ***** main *****
        static void Main()
        {
            List<SUnEmploye> baseDeDonne = new List<SUnEmploye>();
            while (true)
            {
                string choix = AffMenu();

                if (choix == "1")
                {
                    // Mettre ici le code pour agrandir le tableau et appeller la fonction de saisie!
                    SUnEmploye unEmploye = saisirEmploye();
                    baseDeDonne.Add(unEmploye);
                }
                else if (choix == "2")
                {
                    resumeSalarial(baseDeDonne);
                }
                else if (choix == "3")
                {
                    //heuresEmplcode(Employes);
                    Console.Write("Entrez le code de l'employé: ");
                    int codeEmp = Convert.ToInt32(Console.ReadLine());
                    heuresEmplcode(baseDeDonne, codeEmp);

                }
                else if (choix == "q")
                {
                    //Environment.Exit(0);
                    break;
                }
            }
        }

        static SUnEmploye saisirEmploye()
        {
            SUnEmploye unEmploye = new SUnEmploye();
            Console.Write("Entrez le nom de l'employé: ");
            unEmploye.NomEmp = Console.ReadLine();
            Console.Write("Entrez le numéro de l'employé: ");
            unEmploye.codeEmp = Convert.ToInt32(Console.ReadLine());
            unEmploye.HeuresTrav = new SUnHoraire[5];
            int i = 0;
            while (i < 5)
            {
                Console.Write("Entrez l'heure du début du travail du jour {0}: ", i);
                unEmploye.HeuresTrav[i].HeureDeb.HEU = Convert.ToInt32(Console.ReadLine());
                Console.Write("Entrez l'heure de fin de travail du jour {0}: ", i);
                unEmploye.HeuresTrav[i].HeureFin.HEU = Convert.ToInt32(Console.ReadLine());
                unEmploye.Heuretotal += unEmploye.HeuresTrav[i].HeureFin.HEU - unEmploye.HeuresTrav[i].HeureDeb.HEU;
                i++;
            }
            unEmploye.Salaire = unEmploye.Heuretotal * salaireHoraire;
            return unEmploye;
        }

        static void resumeSalarial(List<SUnEmploye> baseDeDonne)
        {
            //code employée,  nom employée,  nombre heure,  gain salarial(h*15.16)
            for (int i = 0; i < baseDeDonne.Count; i++)
            {
                Console.WriteLine("{0, -9} {1, -" + baseDeDonne[i].NomEmp.Length + "} {2, -6} {3, -10}", "Code", "Nom", "Heure", "Salaire");
                Console.WriteLine("{0, -9} {1, -" + baseDeDonne[i].NomEmp.Length + "} {2, -6} {3, -10}", baseDeDonne[i].codeEmp, baseDeDonne[i].NomEmp, baseDeDonne[i].Heuretotal, baseDeDonne[i].Salaire);
            }
            Console.ReadKey();
        }

        static void heuresEmplcode(List<SUnEmploye> baseDeDonne, int codeEmp)
        {
            foreach (var elements in baseDeDonne)
            {
                if (elements.codeEmp == codeEmp)
                {
                    Console.WriteLine("{0, -9} {1, -" + elements.NomEmp.Length + "} {2, -6} {3, -10}", "Code", "Nom", "Heure", "Salaire");
                    Console.WriteLine("{0, -9} {1, -" + elements.NomEmp.Length + "} {2, -6} {3, -10}", elements.codeEmp, elements.NomEmp, elements.Heuretotal, elements.Salaire); //le -10 est pour le formatage, négatif = à gauche, "10" = espace de 10 incluant le texte qui y prend place
                    break; //pas la peine de continuer la boucle puisque le code devrait être unique pour chaque employer
                }
            }
        }
    }
}
