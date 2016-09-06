using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TpFinal
{
    class Program
    {
        #region Variables
        private static int currentUser;
        private const int largeurFenetre = 100, hauteurFenetre = 30;
        public static int currentuser { get { return Program.currentUser; } }
        static readonly string[] commandes = { "Afficher toutes les tâches en cours d'exécution", "Afficher toutes les tâches en attente", "Afficher les 3 dernières tâches terminées", "Afficher les 3 dernières tâches annulées", "Afficher toutes les tâches sans discrémination", "Rechercher une tâche", "Terminer une tâche", "Annuler une tâche", "Placer une tâche à \"en cours\"", "Placer une tâche en attente", "Créer une nouvelle tâche", "Quitter" };
        static readonly int nombreCommande = commandes.Length + 3, firstCommande = 4; //pour l'affichage du menu
        #endregion
        static void Main(string[] args)
        {
            FileAccess.loadFiles(); bool getout = false;
            do
            {
                Console.Write("Identifiant unique : ");
                int cleIdentifiant = int.TryParse(Console.ReadLine(), out cleIdentifiant) ? cleIdentifiant : 0;
                Console.Write("\nMot de passe : ");
                string motdepasse = Console.ReadLine();
                Console.Clear();
                if (FileAccess.cleExistanteEmploye(cleIdentifiant))
                {
                    currentUser = cleIdentifiant;
                    getout = true;
                    Console.WriteLine("Bienvenue {0} ayant pour identifiant unique {1}", FileAccess.trouverEmploye(currentUser), currentUser);
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Identifiant invalide!");
                }
            } while (!getout);
            Menu();
            FileAccess.saveToFiles();
        }
        /// <summary>
        /// Affiche le menu et permet de se déplacer avec les flèches du haut et du bas et la touche entrée
        /// </summary>
        static void Menu()
        {
            Console.SetWindowSize(width: largeurFenetre, height: hauteurFenetre);
            listeCommande(currentXpos: commandes[0].Length + 13);
            int currentYpos = firstCommande; bool getout = false;
            ConsoleKeyInfo a;
            do
            {
                Console.CursorVisible = false;
                a = Console.ReadKey(true);
                switch (a.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentYpos > firstCommande) //début de la première commande
                            currentYpos--;
                        listeCommande(currentYpos, commandes[currentYpos - 4].Length + 13);
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentYpos < nombreCommande) //limite dernière commande
                            currentYpos++;
                        listeCommande(currentYpos, commandes[currentYpos - 4].Length + 13);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (currentYpos != nombreCommande) //si quitter alors sortir
                        {
                            Console.WriteLine("Commande {0:00}: {1}", currentYpos - 4, commandes[currentYpos - 4]);
                            commande(currentYpos);
                            Console.ReadKey();
                            listeCommande(currentYpos, commandes[currentYpos - 4].Length + 13);
                        }
                        else
                            getout = true;
                        break;
                }
            } while (a.Key != ConsoleKey.Q && !getout);
        }
        /// <summary>
        /// Affiche la liste des commandes
        /// </summary>
        /// <param name="currentYpos">Position de la ligne (axis Y)</param>
        /// <param name="currentXpos">Position du curseur sur la ligne (axis X)</param>
        static void listeCommande(int currentYpos = 4, int currentXpos = 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("===========================================================\n");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("               Liste des commandes             \n(se déplacer avec les flèches du haut et du bas)\n(appuyer sur entrée pour accepter la commande)");
            for (int i = 0; i < commandes.Length; i++)
            {
                if (i == currentYpos - 4)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}{1}", (i == commandes.Length - 1) ? "" : string.Format("Commande {0:00} : ", i + 1), commandes[i]);
            }
            Console.SetCursorPosition(currentXpos, currentYpos);
        }
        /// <summary>
        /// Exécute les commandes et accomplit les tâches associées
        /// </summary>
        /// <param name="commande">la commande à effectuer</param>
        static void commande(int commande)
        {
            commande -= 4;
            Console.CursorVisible = true;
            switch (commande)
            {
                case 0:
                    Console.WriteLine("{0}\n{1}", FileAccess.tacheModeleConsole, FileAccess.getByEtat(1));
                    break;
                case 1:
                    Console.WriteLine("{0}\n{1}", FileAccess.tacheModeleConsole, FileAccess.getByEtat(0));
                    break;
                case 2:
                    Console.WriteLine("{0}\n{1}", FileAccess.tacheModeleConsole, FileAccess.troisTermineOuAnnule(2));
                    break;
                case 3:
                    Console.WriteLine("{0}\n{1}", FileAccess.tacheModeleConsole, FileAccess.troisTermineOuAnnule(3));
                    break;
                case 4:
                    Console.WriteLine("{0}\n{1}", FileAccess.tacheModeleConsole, FileAccess.getAllTache());
                    break;
                case 5:
                    Console.WriteLine("{0}\n{1}", FileAccess.tacheModeleConsole, FileAccess.getTache(tacheFaireDeQuoiCommande("rechercher")));
                    break;
                case 6:
                    bool a = FileAccess.modifierTache(tacheFaireDeQuoiCommande("terminer"), 0);
                    if (a)
                        Console.WriteLine("Tâche terminé avec succès");
                    else
                        Console.WriteLine("L'action n'a pas réussie");
                    break;
                case 7:
                    bool b = FileAccess.modifierTache(tacheFaireDeQuoiCommande("annuler"), 1);
                    if (b)
                        Console.WriteLine("Tâche annulé avec succès");
                    else
                        Console.WriteLine("L'action n'a pas réussie");
                    break;
                case 8:
                    bool c = FileAccess.modifierTache(tacheFaireDeQuoiCommande("repartir"), 2);
                    if (c)
                        Console.WriteLine("Tâche repartie avec succès");
                    else
                        Console.WriteLine("L'action n'a pas réussie");
                    break;
                case 9:
                    bool d = FileAccess.modifierTache(tacheFaireDeQuoiCommande("mettre en pause"), 3);
                    if (d)
                        Console.WriteLine("Tâche mise en pause avec succès");
                    else
                        Console.WriteLine("L'action n'a pas réussie");
                    break;
                case 10:
                    string description, e; int idAssigne;
                    do
                    {
                        Console.Write("Description de la tâche : ");
                        description = Console.ReadLine();
                        Console.Write("Identifiant de la personne assignée : ");
                        e = Console.ReadLine();
                        int.TryParse(e, out idAssigne);
                    } while (!FileAccess.cleExistanteEmploye(idAssigne));
                    FileAccess.createTache(description, idAssigne);
                    break;
                default:
                    Console.WriteLine("Commande inexistante"); //au cas où?
                    break;
            }
        }
        /// <summary>
        /// Methode forçant une certaine action en offrant de la vérification
        /// </summary>
        /// <param name="abc">le type de tâche à accomplir</param>
        /// <returns>Un identifiant unique vérifié</returns>
        static int tacheFaireDeQuoiCommande(string abc)
        {
            int temp1 = int.MinValue, bidule = 0; string a = ""; bool sortir = false, temp2 = false;
            do
            {
                do
                {
                    if (bidule == 1)
                    {
                        bidule = -1;
                        Console.Clear();
                    }
                    bidule++;
                    Console.Write("\nTâche à " + abc + " (\"q\" pour sortir): ");
                    a = Console.ReadLine();
                } while (a != "q" && !int.TryParse(a, out temp1));
                if (a == "q")
                    sortir = true;
                temp2 = FileAccess.tacheExistante(temp1);
                if (!temp2)
                {
                    Console.WriteLine("Tâche inexistante");
                }
                else
                    sortir = true;
            } while (!sortir);
            return temp1;
        }
    }
}
