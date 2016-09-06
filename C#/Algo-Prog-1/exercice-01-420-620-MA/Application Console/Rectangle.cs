using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Console
{
    class Rectangle
    {
        public int hauteur;
        public int largeur;

        public Rectangle(int hauteur, int largeur)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            RectangleV5.nbInstances++;
            RectangleV5.toString();
        }
        public int Perimetre()
        {
            return this.hauteur * 2 + this.largeur * 2;
        }

        public int Aire()
        {
            return this.hauteur * this.largeur;
        }
    }
}
