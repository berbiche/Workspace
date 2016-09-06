using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magasin_Guichet
{
    class Magasin
    {
        #region Constructeurs
        public Magasin()
        {
            Guichet un_guichet = new Guichet();
            Guichets.Add(un_guichet);
        }

        public Magasin(int nb_guichet)
        {
            for (int i = 0; i < nb_guichet; i++)
            {
                Guichet un_guichet = new Guichet();
                Guichets.Add(un_guichet);
            }
        }
        #endregion
        #region Variables
        private int compteurClient = 1;
        private List<Guichet> Guichets = new List<Guichet>();
        #endregion
        public string toString()
        {
            int index = 0, temporaire = int.MaxValue;
            for (int i = 0; i < Guichets.Count; i++)
            {
                if (Guichets[i].enATTENTE.Count + ((Guichets[i].clientencours > 0) ? 1 :0) < temporaire && Guichets[i].enpause == false)
                {
                    temporaire = Guichets[i].enATTENTE.Count + ((Guichets[i].clientencours > 0) ? 1 : 0);
                    index = i;
                }
            }
            string s = "";
            for (int i = 0; i < Guichets.Count; i++)
            {
                if (!Guichets[i].enpause)
                {
                    if (i == index)
                        s += Guichets[i].toString() + " <-- le prochain client va ici\n";
                    else
                        s += Guichets[i].toString() + "\n";
                }
                else
                {
                    s += System.Text.RegularExpressions.Regex.Replace(Guichets[i].toString(), "[0-9]", "X") + " <-- Guichet en pause\n";
                }
            }
            return s;
        }

        public void AjouterClient(int n)
        {
            if (n > 0)
            {
                var temporaire = int.MaxValue; ; var numGuichet = -1;
                for (int i = 0; i < Guichets.Count; i++)
                {
                    if (!Guichets[i].enpause)
                    {
                        if (Guichets[i].clientencours > 0)
                        {
                            if (Guichets[i].enATTENTE.Count + 1 < temporaire) //la file la plus courte reçoit la personne
                            {
                                temporaire = Guichets[i].enATTENTE.Count + 1;
                                numGuichet = i;
                            }
                        }
                        else if (Guichets[i].clientencours == 0)
                        {
                            numGuichet = i;
                            break;
                        }
                    }
                } //trouve le guichet avec le moins de personne et y ajoute le client
                if (numGuichet > -1)
                {
                    Guichets[numGuichet].AjouterClient(compteurClient);
                    compteurClient++;
                }
                AjouterClient(n - 1);
            }
        }

        public void Liberer(int index)
        {
            try
            {
                Guichets[index].Liberer();
            }
            catch { }
        }

        public void LibererTout()
        {
            for (int i = 0; i < Guichets.Count; i++)
            {
                try
                {
                    Guichets[i].Liberer();
                }
                catch { }
            }
        }

        public int c_client(int numClient) //retourne le nombre de client avant le clien spécifié
        {
            try
            {
                foreach (Guichet un_clients in Guichets)
                {
                    if (un_clients.client_existe(numClient))
                        if (un_clients.clientencours > 0)
                            return un_clients.enATTENTE.IndexOf(numClient) + 1;
                        else
                            return un_clients.enATTENTE.IndexOf(numClient);
                }
            }
            catch { }
            return 0;
        }

        public int en_pause(int nomGuichet) //int pour retourner un code d'erreur
        {
            if (nomGuichet < Guichets.Count && Guichets.Count > 0 && nomGuichet >= 0)
            {
                if (Guichets[nomGuichet].enpause == true)
                    return 1;
                Guichets[nomGuichet].enpause = true;
                return 0;
            }
            return -1;
        }

        public int au_travail(int nomGuichet) //int pour retourner un code d'erreur
        {
            if (nomGuichet <= Guichets.Count && Guichets.Count > 0 && nomGuichet >= 0)
            {
                if (Guichets[nomGuichet].enpause == false)
                    return 1;
                Guichets[nomGuichet].enpause = false;
                return 0;
            }
            return -1;
        }
    }
}