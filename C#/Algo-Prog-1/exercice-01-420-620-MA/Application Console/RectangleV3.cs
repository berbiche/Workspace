using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Console
{
    class RectangleV3
    {
        private int hauteur;
        private int largeur;
        public int hauteuR { get { return this.hauteur; } }
        public int largeuR { get { return this.largeur; } }
        public int aire { get { return hauteur * largeur; } } // Func<int, int, int> aire = (x, y) => x * y; une fonction anonyme/ lambda
        public int perimetre { get { return hauteur * 2 + largeur * 2; } } // Func<int, int, int> perimetre = (x, y) => x * 2 + y * 2;

        public RectangleV3(int hauteur, int largeur)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            nouveau();
            RectangleV5.nbInstances++;
            RectangleV5.toString();
        }

        public RectangleV3(int dimension)
        {
            this.hauteur = dimension;
            this.largeur = dimension;
            nouveau();
            RectangleV5.nbInstances++;
            RectangleV5.toString();
        }

        private void nouveau()
        {
            Console.WriteLine("Un nouveau rectangle vient d'être créé");
        }
    }
}
