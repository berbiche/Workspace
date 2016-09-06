using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine().ToLower();
            bool consultation = "oui".Equals(str);
            Console.WriteLine(consultation);
        }
    }
}
