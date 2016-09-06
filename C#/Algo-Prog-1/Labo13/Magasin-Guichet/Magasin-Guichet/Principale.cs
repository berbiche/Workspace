using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Magasin_Guichet
{
    class Principale
    {
        public static void Main()
        {
            Magasin un_magasin = new Magasin(3);
            Action show = () => Console.WriteLine(un_magasin.toString());
            show();
            string s, pattern = "[^0-9]";
            Func<string,int> input = (w) => Convert.ToInt32(Regex.Replace(w, pattern, ""));
            Console.WriteLine("Entrez \"aide\" pour la liste des commandes supportées");
            
            do
            {
                Console.WriteLine("Entrez votre commande");
                s = Console.ReadLine().ToLower();
                switch (Regex.Replace(s, "^(?!all$)|[^a-zA-Z]", "")) //enlève les charactères ne faisant pas partie de a-Z (majuscule et minuscule)
                {
                    case "n":
                        un_magasin.AjouterClient(input(s));
                        show();
                        break;
                    case "l":
                        un_magasin.Liberer(input(s));
                        show();
                        break;
                    case "lall":
                        un_magasin.LibererTout();
                        show();
                        break;
                    case "c":
                        Console.WriteLine("Il y a {0} client(s) avant le client spécifié.",un_magasin.c_client(input(s)));
                        break;
                    case "r":
                        if (un_magasin.au_travail(input(s)) == 0)
                        {
                            Console.WriteLine("Le guichet est rouvert, la pause café est terminée!");
                            un_magasin.au_travail(input(s));
                            show();
                        }
                        else if (un_magasin.au_travail(input(s)) == 1)
                            Console.WriteLine("Ce guichet déjà ouvert!");
                        else
                            Console.WriteLine("Ce guichet n'existe pas!");
                        break;
                    case "p":
                        if (un_magasin.en_pause(input(s)) == 0)
                        {

                            Console.WriteLine("Le personnel de ce guichet vient de partir en pause!");
                            un_magasin.en_pause(input(s));
                            show();
                        }
                        else if(un_magasin.en_pause(input(s)) == 1)
                            Console.WriteLine("Le personnel de ce guichet est déjà en pause!");
                        else
                            Console.WriteLine("Ce guichet n'existe pas!");
                            break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    case "aide":
                        Console.WriteLine(commandes());
                        break;
                    case "show":
                        show();
                        break;
                    default:
                        Console.WriteLine("Commande non reconnu!");
                        break;
                }

            } while (true);
        }

        static string commandes()
        {
            return "-------------------------------\n" +
                   "Liste des commandes supportés :\n" +
                   "n [nombre], ajoute le nombre de clients\n" +
                   "l [nombre], enlève le client au guichet indiqué (le premier guichet ayant un index de 0)\n" +
                   "l all, enlève les clients de tous les guichets\n" +
                   "p [nombre], pause le guichet indiqué\n" +
                   "r [nombre], résume le travail du guichet en pause indiqué\n" +
                   "c [nombre], indique le nombre de client avant le client au guichet\n" +
                   "show, affiche les guichets\n" +
                   "aide, affiche la liste des commandes\n" +
                   "-------------------------------";
        }
    }
}
