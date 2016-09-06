using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LaboStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Exercice 1\nQuel est votre nom complet? ");
            string s = GetInitiales(Console.ReadLine()), w;
            Console.WriteLine("Bonjour \"{0}\"!", s);
            Console.Write("\nExercice 2\nQuel est votre nom complet? ");
            s = GetInitialesMaj(Console.ReadLine());
            Console.WriteLine("Les initiales en majuscules de votre nom sont les suivantes \"{0}\"", s);
            Console.Write("\nExercice 3\nEntrez une phrase : ");
            s = Inverser(Console.ReadLine());
            Console.WriteLine("Voici la phrase inverse \"{0}\"", s);
            Console.Write("\nExercice 4\nEntrez des mots : ");
            s = Console.ReadLine();
            Console.Write("Quel lettre ? ");
            w = Console.ReadLine();
            char e = '\0';
            if (w.Length == 1)
                e = Convert.ToChar(w);
            Console.WriteLine("La lettre \"{0}\" revient {1} fois dans \"{2}\"", e, NbCar(s, e), s);
            Console.Write("\nExercice 5\nEntrez un mot : ");
            s = Console.ReadLine();
            Console.WriteLine((EstPalindrome(s) == true) ? "Ce mot est un palindrome" : "Ce mot n'est pas un palindrome");
            Console.WriteLine("\nExercice 6\nEntrez une chaîne de charactère contenant des balises html :");
            s = Console.ReadLine();
            Console.WriteLine("La chaîne \"{0}\" est dans la balise <{1}>", s, HTMLElement(s));
            Console.Write("\nExercice 7\nEntrez une chaîne de charactère html : ");
            s = Console.ReadLine();
            Console.WriteLine("La chaîne de charactère est {0}", (EstValideHTMLElement(s) == true) ? "valide" : "non valide");
            Console.Write("\nExercice 8\nEntrez la chaîne html qui recevra : ");
            s = Console.ReadLine();
            Console.Write("La chaîne qui sera ajouté : ");
            w = Console.ReadLine();
            Console.WriteLine("Voici la nouvelle chaîne \"{0}\"", InsererHtml(s, w));
            Console.Write("\nExercice 9\nEntrez la chaîne pour le blackjack : ");
            s = Console.ReadLine();
            Console.WriteLine("Le nombre de points de la chaîne \"{0}\" est {1}", s, Blackjack(s));
        }

        static string GetInitiales(string s)
        {
            s = Regex.Replace(s, @"\s+", "."); //remplace l'espace par un point
            s = Regex.Replace(s, @"(?<=^.{1})[^.]*|\B[a-zA-Z]*$", ""); //remplace les charactères apres la 1ere lettre ainsi que celle après le point
            return s = s + ".";
        }

        static string GetInitialesMaj(string s)
        {
            s = Regex.Replace(s, @"\s+", ".");
            s = Regex.Replace(s, @"(?<=^.{1})[^.]*|\B[a-zA-Z]*$", "");
            s = s.ToUpper(); //faire un switch est inutile étant donné que cette fonctionne existe déjà
            return s = s + ".";
        }

        static string Inverser(string s)
        {
            //s.Reverse();
            char[] lettreInverse = new char[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                lettreInverse[i] = s.ElementAt(i);
            }
            s = "";
            for (int i = lettreInverse.Length - 1; i >= 0; i--)
            {
                s += lettreInverse[i];
            }
            return s;
        }

        static int NbCar(string s, char w)
        {
            if (w.ToString().Length == 1)
                return s.Count(p => p == w); //j'utilise une expression lambda ainsi que la librairie LINQ
            else
                return 0;
        }

        static bool EstPalindrome(string s)
        {
            string w = s;
            w = Inverser(w);
            if (s == w)
                return true;
            else
                return false;
        }

        static string HTMLElement(string s)
        {
            //je souhaite uniquement conserver le premier mot venant après un "<" et effacer le "<" ainsi que le reste
            //si il n'y a pas de balise au début ("abcdef <abc></abc>") alors ça retourne la balise se trouvant après donc "abc"
            //le site suivant m'a bien aidé : http://regexr.com/
            //le regex ne forme pas un bon décomposeur de html/xml
            return s = Regex.Replace(s, @"^(?:(?!<).)*.|[\s<>](?:(?![.].).*)|>", "");

            /*
            *   ^(?:(?!<).)*.           "^" signifie au début du string, (?:(?!<).)* signifie qu'à partir du début de la balise "<" le reste n'est pas supprimer peu importe le charactère se trouvant après et ce de façon illimité, le "." à la fin signifie la fin de ce qu'il faut protéger 
            *   |                       appelé "pipe", c'est l'équivalent d'un ou (or)
            *   [\s<>]                  recommence à effacer à partir d'un espace, retour de chariot, tab, "<" ou ">"
            *   (?:(?![.].).*)          efface tous les charactères après avoir trouvé la chose qui nous permet de recommencer à effacer "[\s<>]"
            *   |>                      efface toute fermeture de balise restante ">"
            */
        }

        static bool EstValideHTMLElement(string s)
        {
            return Regex.IsMatch(s, @"<([a-zA-Z][a-zA-Z0-9]*)\b.*>.*<\/\1>");
            /*
            *   <([a-zA-Z][0-9a-zA-Z]*)\b   doit commencer avec "<" et peut contenir n'importe quel lettre de l'alphabet, mais ne peut pas débuter par un nombre (h1, h2, h3, h4, h5, h6), les parenthèses () et le \b signifie que ça forme un mot (ex: h1, body, etc.)
            *   .*>.*                       peut contenir n'importe quel charactère illimité mais doit avoir à la fin (de balise) ">" et peut contenir n'importe quel charactère après la balise hormis \n illimité de fois
            *   <\/\1>                      doit commencer avec "</" et "\1" = contenir le mot définit plutot avec les parenthèses et le \b, le \1 = le mot forme (ex: h1, body, etc.)
            */
        }

        static string InsererHtml(string s, string w)
        {
            Regex quelquechose = new Regex(@"(>(?<![\s])<)");
            return s = quelquechose.Replace(s, ">" + w + "<", 1); //remplace la première occurance du "><", s'il y a du texte déjà présent il ne sera pas enlevé
        }

        static int Blackjack(string s)
        {
            int qqC = 0;
            for (int i = 0; i < s.Length; i++)
            {
                switch(s.Substring(i, 1))
                {
                    case "2":
                        qqC += 2;
                        break;
                    case "3":
                        qqC += 3;
                        break;
                    case "4":
                        qqC += 4;
                        break;
                    case "5":
                        qqC += 5;
                        break;
                    case "6":
                        qqC += 6;
                        break;
                    case "7":
                        qqC += 7;
                        break;
                    case "8":
                        qqC += 8;
                        break;
                    case "9":
                        qqC += 9;
                        break;
                    case "10":
                    case "J":
                    case "Q":
                    case "K":
                        qqC += 10;
                        break;
                    case "A":
                        qqC += (qqC + 11 > 21) ? 1 : 11;
                        break;
                }
            }
            return qqC;
        }
    }
}
