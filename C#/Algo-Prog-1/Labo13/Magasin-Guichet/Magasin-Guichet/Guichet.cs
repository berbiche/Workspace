using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magasin_Guichet
{
    class Guichet
    {
        #region Variables
        private int clientEnCours = 0;
        private List<int> enAttente = new List<int>();
        private bool enPause = false;
        #endregion
        #region Accessors
        public int clientencours { get { return this.clientEnCours; } set { this.clientEnCours = value; } }
        public List<int> enATTENTE { get { return this.enAttente; } set { this.enAttente = value; } }
        public bool enpause { get { return this.enPause; } set { this.enPause = value; } }
        #endregion

        public string toString()
        {
            return "[" + clientencours + "]" + " => " + "{" + string.Join(", ", enAttente) + "}";
        }

        public void AjouterClient(int client)
        {
            if (clientEnCours == 0)
                clientEnCours = client;
            else
                enAttente.Add(client);
        }

        public void Liberer()
        {
            if (enAttente.Count > 0 && clientencours > 0)
            {
                clientEnCours = enAttente[0];
                enAttente.RemoveAt(0);
            }
            else
                clientEnCours = 0;
        }

        public bool client_existe(int num_client)
        {
            foreach (var clients in enAttente)
            {
                if (clients == num_client)
                    return true;
            }
            return false; //sinon retourne faux
        }
    }
}
