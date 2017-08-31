using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz
{
    public class Client : IComparable, IComparable<Client>
    {

        private string noClient;
        private string nomFamille;
        private string prenom;

        private double solde;

        public Client()
        {
        }

        public Client(string noClient, string nomFamille, string prenom)
        {
            this.noClient = noClient;
            this.NomFamille = nomFamille;
            this.Prenom = prenom;
        }

        public string NoCLient
        {
            get { return noClient; }
        }

        public string NomFamille
        {
            get { return nomFamille; }
            set { nomFamille = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public double Solde
        {
            get { return solde; }
            set { solde = value; }
        }

        public override bool Equals(Object obj)
        {

            Client c = obj as Client;
            if (c == null)
                return false;
            else
                return NoCLient.Equals(c.NoCLient);
        }

        public override int GetHashCode()
        {
            return NoCLient.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            else
            {

                if (obj is Client)
                {
                    return this.NoCLient.CompareTo(((Client)obj).NoCLient);
                }
                else
                {
                    throw new ArgumentException("L'objet n'est pas un Client");

                }
            }
        }

        public int CompareTo(Client other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.NoCLient.CompareTo(other.NoCLient);
            }
        }

    }


}