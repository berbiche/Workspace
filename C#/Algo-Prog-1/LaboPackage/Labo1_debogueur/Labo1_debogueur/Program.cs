using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo1_debogueur
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ce programme comporte plusieurs bogues à corriger
            //La somme des coûts des produits est incorrecte
            //Le décompte des produits est incorrect
            //Les taxes affichées sont incorrectes
            // N.B. : Ce programme présume les deux taxes indépendantes l'une de l'autre.

            int noproduit = 0;
            float somme = 0;
            float produitcourant = 0;

            const float tauxtaxefédérale = 0.05F;
            const float tauxtaxeprovinciale = 0.07F;

            Console.WriteLine("Petite caisse enregistreuse\n");
            Console.WriteLine("Saisissez les prix des produits (-1 pour terminer)");
            float.TryParse(Console.ReadLine(), out produitcourant);
            while (produitcourant != -1)
            {
                noproduit++;
                Console.Write("Prix du produit no. {0} : ", noproduit);
                produitcourant = float.Parse(Console.ReadLine());
                if (produitcourant != -1)
                    somme += produitcourant;
            }
            //int temporaire = noproduit;
            //int x = noproduit++;
            //int y = ++noproduit;
            //Console.WriteLine(x + " " + y + " " + temporaire);
            Console.WriteLine("\nLa somme des coûts des {0} produit(s) est : {1:C}", noproduit > 0 ? --noproduit : noproduit, somme);

            float taxefédérale = somme * tauxtaxefédérale;

            Console.WriteLine("Taxe fédérale : {0:C}", taxefédérale);
            float taxeprovinciale = somme * tauxtaxeprovinciale;

            Console.WriteLine("Taxe provinciale : {0:C}", taxeprovinciale);

            float couttotal = somme + taxefédérale + taxeprovinciale;

            Console.WriteLine("Facture totale : {0:C}", couttotal);
        }
    }
}
