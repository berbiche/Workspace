using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HouseOfCards
{

    /*
     * Classe qui implémente un paquet de 52 cartes. Dans le constructeur, les 52 cartes sont brassées (shuffled).
     *
     */
    class Deck
    {
        List<Card> stack;

        public int Count  
        {
            get {
                return stack.Count;
            }
        }

        public Deck() 
        {
            stack = new List<Card>();

            foreach (Couleur couleur in Enum.GetValues(typeof(Couleur))) 
            {
                foreach (CardValeur valeur in Enum.GetValues(typeof(CardValeur)))
                {
                    stack.Add(new Card(couleur, valeur));
                }
            }

            stack.Shuffle();
        }

        /*
         *  Prend n cartes du paquet
         */
        public List<Card> Take(int n)
        {
            return stack.Splice(0, n);
        }
    }

    /*
     * Le code ci-dessous utilise des techniques avancées pour pouvoir implémenter la méthode SHUFFLE() sur les listes génériques.
     * Je ne m'attends pas à ce que vous compreniez tout cela. Ce n'est pas très important pour le contexte du cours.
     *
     */

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    static class MyExtensions_
    {
        public static List<T> Splice<T>(this List<T> list, int index, int count)
        {
            List<T> range = list.GetRange(index, count);
            list.RemoveRange(index, count);
            return range;
        }
    }
}
