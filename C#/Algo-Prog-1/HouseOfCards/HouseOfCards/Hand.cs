using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfCards
{
    class Hand
    {
        List<Card> cards;
        Deck deck;

        public Hand(Deck fromDeck, int start)
        {
            cards = fromDeck.Take(start);
            deck = fromDeck;
        }

        public void PickFromDeck(int n)
        {
            cards.AddRange(deck.Take(n));
        }

        public void Print()
        {
            foreach (Card card in cards)
            {
                Console.Write(card.ToString() + " ");
            }
            Console.WriteLine();
        }


        /* Ajout de méthodes ici */


        public int TotalBlackJackPoints()
        {
            return cards.Sum(x => x.BlackJackPoints);
        }

        public bool AllSameSuit()
        {
            return cards.TrueForAll(x => x._couleur == cards[0]._couleur);
        }

        public bool IsPair()
        {
            return cards.TrueForAll(x => x._valeur == cards[0]._valeur);
        }

    }
}
