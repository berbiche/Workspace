using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decimal_octal
{
    class Program
    {
        static void Main(string[] args)
        {
            int nombreBidon;
            int.TryParse(Console.ReadLine(), out nombreBidon);
            Console.WriteLine(Division(nombreBidon));
        }
        static public string Division(int nombreDansTextBox)
        {
            int nombre = 0, reste = 0;
            string nombreEnOctal = "";
            nombre = nombreDansTextBox;
            do
            {
                reste = nombre % 8;
                nombre = nombre / 8;
                nombreEnOctal = string.Format(reste + nombreEnOctal);
                /*
                i = i + 1
                ActiveSheet.Cells(i, 1) = Nombre
                ActiveSheet.Cells(i, 2) = Reste
                */
            } while (nombre > 0);
            return "Le nombre " + nombreDansTextBox + " en octal est " + nombreEnOctal;
        }
    }
}
