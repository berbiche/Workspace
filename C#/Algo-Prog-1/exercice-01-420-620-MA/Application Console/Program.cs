using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RectangleV5.nbInstances = 0;
            //Version1();
            //Version2();
            //Version3();
            //Version4();
            Version5();
            Console.ReadLine();
        }

        static void Version1()
        {
            Rectangle v1 = new Rectangle(10, 30), v2 = new Rectangle(15, 28);
            Console.WriteLine("Rectangle v1\nHauteur: {4}, Largeur: {5}\nAire: {0}\nPerimètre: {1}\n\nRectangle v2\nHauteur: {6}, Largeur: {7}\nAire: {2}\nPerimètre: {3}\n\n", v1.Aire(), v1.Perimetre(), v2.Aire(), v2.Perimetre(),v1.hauteur, v1.largeur, v2.hauteur, v2.largeur);
        }

        static void Version2()
        {
            RectangleV2 v1 = new RectangleV2(10, 30), v2 = new RectangleV2(15, 28);
            Console.WriteLine("RectangleV2 v1\nHauteur: {4}, Largeur: {5}\nAire: {0}\nPerimètre: {1}\n\nRectangleV2 v2\nHauteur: {6}, Largeur: {7}\nAire: {2}\nPerimètre: {3}\n\n", v1.airE, v1.perimetrE, v2.airE, v2.perimetrE, v1.hauteuR, v1.largeuR, v2.hauteuR, v2.largeuR);
        }

        static void Version3()
        {
            RectangleV3 v1 = new RectangleV3(10, 30), v2 = new RectangleV3(15, 28), v3 = new RectangleV3(50);
            Console.WriteLine("RectangleV3 v1\nHauteur: {4}, Largeur: {5}\nAire: {0}\nPerimètre: {1}\n\nRectangleV3 v2\nHauteur: {6}, Largeur: {7}\nAire: {2}\nPerimètre: {3}\n\nRectangleV3 v3\nHauteur: {8}, Largeur: {9}\nAire: {10}\nPerimètre: {11}\n\n", v1.aire, v1.perimetre, v2.aire, v2.perimetre, v1.hauteuR, v1.largeuR, v2.hauteuR, v2.largeuR, v3.hauteuR, v3.largeuR, v3.aire, v3.perimetre);
        }

        static void Version4()
        {
            RectangleV4 v1 = new RectangleV4(10, 30), v2 = new RectangleV4(15, 28), v3 = new RectangleV4(50);
            Console.WriteLine("RectangleV4 v1: {12}\nHauteur: {4}, Largeur: {5}\nAire: {0}\nPerimètre: {1}\n\nRectangleV4 v2: {13}\nHauteur: {6}, Largeur: {7}\nAire: {2}\nPerimètre: {3}\n\nRectangleV4 v3: {14}\nHauteur: {8}, Largeur: {9}\nAire: {10}\nPerimètre: {11}\n\n", v1.aire, v1.perimetre, v2.aire, v2.perimetre, v1.hauteuR, v1.largeuR, v2.hauteuR, v2.largeuR, v3.hauteuR, v3.largeuR, v3.aire, v3.perimetre, v1.toString(), v2.toString(), v3.toString());
        }

        static void Version5()
        {
            RectangleV4 v1 = new RectangleV4(10, 30), v2 = new RectangleV4(15, 28), v3 = new RectangleV4(50);
            Console.WriteLine("{12}\nHauteur: {4}, Largeur: {5}\nAire: {0}\nPerimètre: {1}\n\n{13}\nHauteur: {6}, Largeur: {7}\nAire: {2}\nPerimètre: {3}\n\n{14}\nHauteur: {8}, Largeur: {9}\nAire: {10}\nPerimètre: {11}\n\n", v1.aire, v1.perimetre, v2.aire, v2.perimetre, v1.hauteuR, v1.largeuR, v2.hauteuR, v2.largeuR, v3.hauteuR, v3.largeuR, v3.aire, v3.perimetre, v1.toString(), v2.toString(), v3.toString());
        }
    }
}
