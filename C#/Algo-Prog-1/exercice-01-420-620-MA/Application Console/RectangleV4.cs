using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Console
{
    class RectangleV4
    {
        private int hauteur;
        private int largeur;
        public int hauteuR { get { return this.hauteur; } }
        public int largeuR { get { return this.largeur; } }
        public int aire { get { return hauteur * largeur; } }
        public int perimetre { get { return hauteur * 2 + largeur * 2; } }

        public RectangleV4(int hauteur, int largeur)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            RectangleV5.nbInstances++;
            RectangleV5.toString();
        }

        public RectangleV4(int dimension)
        {
            this.hauteur = dimension;
            this.largeur = dimension;
            RectangleV5.nbInstances++;
            RectangleV5.toString();
        }

        public string toString()
        {
            return "Rectangle "+hauteur+" (h) x "+largeur+" (l).";
        }
    }
}
