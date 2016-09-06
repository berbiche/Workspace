using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpFinal
{
    [Serializable]
    class Tache
    {
        private Exception annule = new Exception("Impossible de changer l'état de cette tâche. La tâche est annulée."), termine = new Exception("Impossible de changer l'état de cette tâche. La tâche est terminée."), autorisation = new Exception("Vous ne possédez pas les autorisations requises.");
        private string description;
        private DateTime dateCreation;
        private DateTime? dateFin; // "?" permet de nullifier le type de variable
        private etat etatcourant;
        private int idTache, idCreateur, idAssigne;
        private enum etat { enAttente = 0, enCours = 1, termine = 2, annule = 3 }

        #region Accesseurs
        public int etatCourant { get { return (int) this.etatcourant; } }
        public DateTime? dateFinale { get { return this.dateFin; } }
        public int idtache { get { return this.idTache; } }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur utilisé pour créer des nouvelles tâches
        /// </summary>
        /// <param name="description">Description de la tâche</param>
        /// <param name="idUser">identifiant de l'utilisateur qui la crée</param>
        /// <param name="assigne">identifiant de l'utilisateur a qui la tâche est assignée</param>
        public Tache(string description, int assigne)
        {
            this.dateCreation = DateTime.Now;
            this.dateFin = null;
            this.etatcourant = etat.enAttente;
            this.description = description;
            this.idCreateur = Program.currentuser;
            this.idAssigne = assigne;
            this.idTache = FileAccess.dernierIdentifiantTache + 1;
        }
        /// <summary>
        /// Constructeur utilisé lors du chargement des tâches dans le dictionnaire
        /// </summary>
        /// <param name="idTache">Identifiant unique de la tâche</param>
        /// <param name="description">Description de la tâche</param>
        /// <param name="etat">État courant de la tâche</param>
        /// <param name="idUser">Identifiant unique du créateur de la tâche</param>
        /// <param name="assigne">Identifiant unique de la personne à qui la tâche est assignée</param>
        /// <param name="dateCreation">Date de création de la tâche</param>
        /// <param name="fin">Date à laquelle la tâche s'est terminée</param>
        public Tache(int idTache, string description, int etat, int idUser, int assigne, DateTime dateCreation, DateTime? fin = null)
        {
            this.idTache = idTache;
            this.description = description;
            this.dateCreation = dateCreation;
            this.dateFin = fin;
            this.etatcourant = (Tache.etat) etat; //enlève l'ambiguité
            this.idCreateur = idUser;
            this.idAssigne = assigne;
        }
        #endregion

        #region Terminate(), Cancel(), Wait(), StartAgain(), Verbalisation()
        /// <summary>
        /// Marque la tâche comme étant terminé
        /// </summary>
        /// <returns>Vrai si l'opération est réussi</returns>
        public bool Terminate()
        {
            if (etatcourant != etat.annule && etatcourant != etat.termine)
                if (Program.currentuser == idAssigne)
                {
                    etatcourant = etat.termine;
                    dateFin = DateTime.Now;
                    return true;
                }
                else
                    throw autorisation;
            else if (etatcourant == etat.termine)
                throw termine;
            else
                throw annule;
        }
        /// <summary>
        /// Marque la tâche comme étant annulé
        /// </summary>
        /// <returns>Vrai si l'opération est réussi</returns>
        public bool Cancel()
        {
            if (etatcourant != etat.annule && etatcourant != etat.termine)
                if (Program.currentuser == idCreateur)
                {
                    etatcourant = etat.annule;
                    dateFin = DateTime.Now;
                    return true;
                }
                else
                    throw autorisation;
            else if (etatcourant == etat.termine)
                throw termine;
            else
                throw annule;
        }
        /// <summary>
        /// Marque la tâche comme étant en attente
        /// </summary>
        /// <returns>Vrai si l'opération est réussi</returns>
        public bool Wait()
        {
            if (etatcourant != etat.annule && etatcourant != etat.termine)
                if (Program.currentuser == idCreateur)
                {
                    etatcourant = etat.enAttente;
                    return true;
                }
                else
                    throw autorisation;
            else if (etatcourant == etat.termine)
                throw termine;
            else
                throw annule;
        }
        /// <summary>
        /// Marque la tâche comme étant en cours
        /// </summary>
        /// <returns>Vrai si l'opération est réussi</returns>
        public bool StartAgain()
        {
            if (etatcourant != etat.annule && etatcourant != etat.termine)
                if (Program.currentuser == idAssigne)
                {
                    etatcourant = etat.enCours;
                    return true;
                }
                else
                    throw autorisation;
            else if (etatcourant == etat.termine)
                throw termine;
            else
                throw annule;
        }
        /// <summary>
        /// Retourne les éléments privés de l'instance de la classe dans un string formaté
        /// </summary>
        /// <returns>string formaté contenant les propriétés de l'instance de la classe</returns>
        public string Verbalisation()
        {
            string etatTache = "", dateDebut = "", dateFin = "";
            dateDebut = string.Format("({0:0000}-{1}-{2:00})", dateCreation.Year, dateCreation.Month, dateCreation.Day);
            if (this.dateFin.HasValue)
                dateFin = string.Format("{0:0000}-{1}-{2:00}", this.dateFin.Value.Year, this.dateFin.Value.Month, this.dateFin.Value.Day);
            switch (etatcourant)
            {
                case etat.enAttente:
                    etatTache = "En attente";
                    break;
                case etat.enCours:
                    etatTache = "En exéc.";
                    break;
                case etat.annule:
                    etatTache = "Annulé";
                    break;
                case etat.termine:
                    etatTache = "Terminé";
                    break;
            }
            return string.Format("{0,-6}\t{1,-30}\t{2,-10}\t{3,-20}\t{4,-20}\t{5,-10}", idTache, description, etatTache, FileAccess.trouverEmploye(idAssigne), string.Format("{0,-18}{1,12}", FileAccess.trouverEmploye(idCreateur), dateDebut), dateFin);
        }
        #endregion
    }
}
