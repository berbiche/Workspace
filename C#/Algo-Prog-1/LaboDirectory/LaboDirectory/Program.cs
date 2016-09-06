using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LaboDirectory
{
    class Program
    {
        private static SortedDictionary<int, Compte> livreCompte = new SortedDictionary<int, Compte>();
        private static int nombreFichier = 0, nombreErreur = 0;
        static void Main(string[] args)
        {
            loaded();
            Console.WriteLine("RAPPORT COMPTABLE");
            Console.WriteLine();
            Console.WriteLine("Nb fichiers traités : {0}",nombreFichier);
            Console.WriteLine("Nb erreurs rencontrées : {0}",nombreErreur);
            foreach (var compte in livreCompte)
            {
                Console.WriteLine();
                Console.WriteLine("Compte #{0}",compte.Key);
                Console.WriteLine("{0,-7} {1,16}\n{2,-7} {3,16}", "Mois", "Montant transigé", new string('=', 7), new string('=', 16));
                Console.WriteLine(compte.Value.ToString());
                Console.WriteLine("{0,24}",new string('-',16));
                Console.WriteLine("{0,-7} {1,16:##.00} $","Total",compte.Value._transactionTotal);
            }
        }

        static void loaded()
        {
            var lesFichiers = Directory.EnumerateFiles(@"..\..\data\", "*.csv");
            for (int i = 0; i < lesFichiers.Count(); i++)
            {
                foreach (var lines in File.ReadAllLines(lesFichiers.ElementAt(i)))
                {
                    int numCompte = 0; double montantTransaction = 0; DateTime date = new DateTime();
                    var matches = System.Text.RegularExpressions.Regex.Matches(lines, "[0-9-,.]+");
                    if (matches.Count != 3)
                        nombreErreur++;
                    try
                    {
                        var dateParse = matches[0].Value.Split('-');
                        date = new DateTime(int.Parse(dateParse[0]), int.Parse(dateParse[1]), int.Parse(dateParse[2])); //parse la date
                    }
                    catch { nombreErreur++; }
                    if (!int.TryParse(matches[1].Value, out numCompte))
                        nombreErreur++;
                    if (!double.TryParse(matches[2].Value.Replace(',','.'), out montantTransaction))
                        nombreErreur++;
                    if (livreCompte.ContainsKey(numCompte))
                    {
                        livreCompte[numCompte]._livreTransactions.Add(new Compte.transaction(date, montantTransaction));
                    }
                    else
                    {
                        livreCompte.Add(numCompte, new Compte(date, montantTransaction));
                    }
                }
                nombreFichier++;
            }
        }
    }
}
