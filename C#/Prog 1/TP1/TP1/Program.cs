using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    class Program
    {
        public void coupPatient(int coutConsultation, int coutImage, int coutAnalyse, decimal coutInjection, decimal coutTotal)
        {

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez le nombre de patients reçus aujourd'hui");
            int nbPatients;
            int.TryParse(Console.ReadLine(), out nbPatients);
            for (int essaie = 0; essaie <= nbPatients; essaie++)
            {
                Console.WriteLine("Entrez l'âge du patient");
                int age;
                int.TryParse(Console.ReadLine(), out age);
                Console.WriteLine("Le patient a-t-il prit une consultation, Oui/Non?");
                string consultation = Console.ReadLine().ToLower();
                bool consult = (consultation == "oui");
                Console.WriteLine("Le patient a-t-il prit une image radio");
            }
        }
    }
}
