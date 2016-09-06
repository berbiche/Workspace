using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPP_exercice_1
{
    class Personne
    {
        #region Variables
        public enum EtatCivile { marié, célibataire, conjointDeFait, unionCivile }
        public enum Sexe { femme, homme } // sexe != genre
        private string prenom, nom, lieuNaissance; //
        private int age, numAssuranceSociale;
        private DateTime dateNaissance;
        private EtatCivile etatCivile;
        private Sexe sexe;
        #endregion
        #region Constructeur
        public Personne(string prenom, string nom, Sexe sexe) //pour les personnes qui ne veulent pas donner trop d'information
        {
            this.prenom = prenom;
            this.nom = nom;
            this.sexe = sexe;
        }
        public Personne(string prenom, string nom, string numSociale, Sexe sexe, string lieuNaissance, DateTime dateNaissance, EtatCivile etatCivile, string qualificatif = "")
        {
            this.prenom = prenom;
            this.nom = nom;
            this.lieuNaissance = lieuNaissance;
            this.dateNaissance = dateNaissance;
            this.etatCivile = etatCivile;
            this.sexe = sexe;
            this.age = DateTime.Now.Year - dateNaissance.Year;
        }
        #endregion
        #region Accesseur
        public string Conjoint { get { return this.conjoint; } set { this.conjoint = value; } }
        public EtatCivile etatcivile { get { return this.etatCivile; } set { this.etatCivile = value; } }
        public string Nom { get { return this.nom; } }
        public string Prenom { get { return this.prenom; } }
        public int numassurancesociale { get { return this.numAssuranceSociale; } }
        #endregion
    }
}
