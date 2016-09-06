using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Console
{
    class RectangleV2
    {
        private int hauteur;
        private int largeur;
        private int aire;
        private int perimetre;
        public int hauteuR { get { return this.hauteur; } set { this.hauteur = value; } }
        public int largeuR { get { return this.largeur; } set { this.largeur = value; } }

        public int airE { get { return this.aire = hauteur * largeur; } }
        public int perimetrE { get { return this.perimetre = hauteur * 2 + largeur * 2; } }

        public RectangleV2(int hauteur, int largeur)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            RectangleV5.nbInstances++;
            RectangleV5.toString();
        }

    }
}
