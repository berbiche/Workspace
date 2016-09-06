using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfCards
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Deck myDeck = new Deck();

            Console.WriteLine("Il y a {0} cartes dans le paquet.;", myDeck.Count);

            Hand playerHand = new Hand(myDeck, 2);
            Console.Write("Main du joueur: {0} points : ", playerHand.TotalBlackJackPoints());
            playerHand.Print();



            Hand croupierHand = new Hand(myDeck, 2);
            Console.Write("Main du croupier: {0} points : ", croupierHand.TotalBlackJackPoints());
            croupierHand.Print();
            Hand croupierHand2 = new Hand(myDeck, 5);
            Console.Write("AllSameSuit: {0}\n", croupierHand2.AllSameSuit());
            croupierHand2.Print();

            Hand croupierHand3 = new Hand(myDeck, 2);
            Console.Write("IsPair: {0}\n", croupierHand3.IsPair());
            croupierHand3.Print();

            Console.WriteLine("Il y a {0} cartes dans le paquet.", myDeck.Count);

            Console.ReadLine();
        }
    }
}
