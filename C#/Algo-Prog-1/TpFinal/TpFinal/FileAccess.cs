using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TpFinal
{
    [Serializable]
    //ca rend l'écriture et la lecture dans le fichier beaucoup plus simple
    class FileAccess
    {
        #region Variables
        //load le dictionnaire des taches et des utilisateurs
        private static List<Tache> listeTache = new List<Tache>(); //je vais accéder aux éléments via des méthodes publiques
        private static Dictionary<int, string> dictionnaireEmploye = new Dictionary<int, string>(); //pareil qu'en-haut
        public static readonly string tacheModeleConsole = string.Format("{0,-6}\t{1,-30}\t{2,-10}\t{3,-20}\t{4,-30}\t{5,-10}\r\n{6}\t{7}\t{8}\t{9}\t{10}\t{11}", "ID", "Description", "Statut", "Assigné à", "Requête", "Fin", "------", "------------------------------", "----------", "--------------------", "------------------------------", "----------");
        #endregion

        #region Accesseurs
        public static int dernierIdentifiantTache { get { return listeTache.Last().idtache; } }
        #endregion

        #region Accès aux fichiers
        /// <summary>
        /// Charge la liste des employés et des tâches dans leur dictionnaire respectif
        /// </summary>
        public static void loadFiles()
        {
            BinaryFormatter loadSave = new BinaryFormatter();
            FileStream fichierEmploye = new FileStream(@"..\..\Data\employes.txt", FileMode.Open), fichierTaches = new FileStream(@"..\..\Data\taches.txt",FileMode.Open);
            dictionnaireEmploye = (Dictionary<int, string>) (loadSave.Deserialize(fichierEmploye));
            listeTache = (List<Tache>) (loadSave.Deserialize(fichierTaches));
            fichierTaches.Close();
            fichierEmploye.Close();
        }

        /// <summary>
        /// Sauvegarde les modifications dans les fichiers à la fin du programme (donc menu quitter)
        /// </summary>
        public static void saveToFiles()
        {
            BinaryFormatter saveFile = new BinaryFormatter();
            saveFile.Serialize(new FileStream(@"..\..\Data\employes.txt", FileMode.Create), dictionnaireEmploye);
            saveFile.Serialize(new FileStream(@"..\..\Data\taches.txt", FileMode.Create), listeTache);
        }
        #endregion

        #region Méthodes de recherche et autres
        /// <summary>
        /// Regarde si la clé spécifiée existe dans la liste d'employées
        /// </summary>
        /// <param name="identifiant">clé unique de l'utilisateur</param>
        /// <returns>Vrai si l'identifiant unique existe sinon faux</returns>
        public static bool cleExistanteEmploye(int identifiant)
        {
            return dictionnaireEmploye.ContainsKey(identifiant);
        }

        /// <summary>
        /// Retourne l'identifiant de la personne possédant l'identifiant unique passé en paramètre
        /// </summary>
        /// <param name="identifiant">Identifiant unique de l'employé</param>
        /// <returns>Nom de la personne pour l'identifiant</returns>
        public static string trouverEmploye(int identifiant)
        {
            return dictionnaireEmploye.ContainsKey(identifiant) ? dictionnaireEmploye[identifiant] : null;
        }

        /// <summary>
        /// Créé une nouvelle tâche
        /// </summary>
        /// <param name="description">Description de la tâche</param>
        /// <param name="idAssigne">Identifiant unique de la personne assignée</param>
        public static void createTache(string description, int idAssigne)
        {
            listeTache.Add(new Tache(description, idAssigne));
        }

        /// <summary>
        /// Recherche si la clé existe dans la liste des tâches
        /// </summary>
        /// <param name="identifiant">L'identifiant unique de la clé</param>
        /// <returns>vrai: identifiant existe, faux: n'existe pas</returns>
        public static bool tacheExistante(int identifiant)
        {
            return listeTache.Exists(x => x.idtache == identifiant);
        }

        /// <summary>
        /// Place en ordre les tâches terminées et retourne les 3 plus récentes
        /// </summary>
        /// <param name="a">2:termine, 3:annule</param>
        /// <returns>string contenant la(les) dernière(s) tâche(s) terminée(s)</returns>
        public static string troisTermineOuAnnule(int a)
        {
            return string.Join("\n", listeTache
                .Where(x => x.dateFinale.HasValue && x.etatCourant == a) //sélectionne les éléments non nuls
                .OrderByDescending(x => x.dateFinale.Value) //mis en ordre par leur date
                .Take(3) //prend les 3 premiers éléments
                .Select(x => x.Verbalisation()));
        }

        /// <summary>
        /// Retourne une liste de tous les éléments selon le type
        /// </summary>
        /// <param name="a">0:enAttente,1:enCours,2:termine,3:annule</param>
        /// <returns>Tous les éléments satisfaisant à la tâche</returns>
        public static string getByEtat(int a)
        {
            return string.Join("\n", listeTache.Where(x => x.etatCourant == a).Select(x => x.Verbalisation()));
        }

        /// <summary>
        /// Retourne la liste de toutes les tâches
        /// </summary>
        /// <returns>string contenant toutes les tâches formatés</returns>
        public static string getAllTache()
        {
            return string.Join("\n", listeTache.Select(x => x.Verbalisation()));
        }
        /// <summary>
        /// Retourne un string formaté de la tâche spécifiée si trouvé
        /// </summary>
        /// <param name="identifiant">Identifiant unique de la tâche</param>
        /// <returns>Verbalisation de la tâche spécifiée sinon null</returns>
        public static string getTache(int identifiant)
        {
            return listeTache.Find(x => x.idtache == identifiant).Verbalisation(); //retourne null si rien trouvé
        }

        /// <summary>
        /// Modifie l'état d'une tâche
        /// </summary>
        /// <param name="identifiant">L'identifiant de la tâche</param>
        /// <param name="choix">0: terminate, 1: Cancel, 2: StartAgain, 3: Wait</param>
        /// <returns>Vrai si l'opération est réussi</returns>
        public static bool modifierTache(int identifiant, int choix)
        {
            if (tacheExistante(identifiant))
            {
                var abc = listeTache.FindIndex(x => x.idtache == identifiant);
                try
                {
                    if (choix == 0)
                        return listeTache[abc].Terminate();
                    else if (choix == 1)
                        return listeTache[abc].Cancel();
                    else if (choix == 2)
                        return listeTache[abc].StartAgain();
                    else if (choix == 3)
                        return listeTache[abc].Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }
        #endregion
    }
}
