using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfCards
{
    enum Couleur { Pique, Coeur, Carreau, Trèfle };

    enum CardValeur { _2, _3, _4, _5, _6, _7, _8, _9, _10, Valet, Dame, Roi, As };


    class Card
    {
        Couleur couleur;
        CardValeur valeur;
        public bool _isHonnor { get { return (int)valeur > 8; } }
        public int BlackJackPoints { get { return (int) valeur == 12 ? 1 : (int) valeur > 8 && (int)valeur < 12 ? 10 : (int) valeur + 2; } }
        public Couleur _couleur { get { return this.couleur; } }
        public CardValeur _valeur { get { return this.valeur; } }
        public Card(Couleur c, CardValeur v)
        {
            couleur = c;
            valeur = v;
        }

        public override string ToString()
        {
            return valeur.ToString() + couleur.ToString();
        }

        /* Ajout de méthodes ici */
    }
}
