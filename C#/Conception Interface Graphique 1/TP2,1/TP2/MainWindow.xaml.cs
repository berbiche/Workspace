using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace TP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int?[,] sudoku1 = { { null, null, null, 4, null, null, 7, null, 6 }, { null, null, null, null, null, null, null, 3, null }, { null, null, 5, 8, 6, null, null, 2, null }, { null, null, null, 1, 4, null, null, null, 9 }, { 7, 8, null, null, null, null, null, 1, 5 }, { 5, null, null, null, 8, 7, null, null, null }, { null, 3, null, null, 1, 2, 5, null, null }, { null, 1, null, null, null, null, null, null, null }, { 4, null, 6, null, null, 8, null, null, null } };
        private readonly int?[,] solution = { { 8, 9, 3, 4, 2, 1, 7, 5, 6 }, { 6, 2, 4, 7, 5, 9, 1, 3, 8 }, { 1, 7, 5, 8, 6, 3, 9, 2, 4 }, { 3, 6, 2, 1, 4, 5, 8, 7, 9 }, { 7, 8, 9, 2, 3, 6, 4, 1, 5 }, { 5, 4, 1, 9, 8, 7, 3, 6, 2 }, { 9, 3, 8, 6, 1, 2, 5, 4, 7 }, { 2, 1, 7, 5, 9, 4, 6, 8, 3 }, { 4, 5, 6, 3, 7, 8, 2, 9, 1 } };
        DispatcherTimer chrono = new DispatcherTimer(); DateTime tempsDebut = new DateTime(); TimeSpan tempsFinal = new TimeSpan();
        private int nombreBons = 0, nombreResultat = 0;
        private bool outputLog = false;

        public MainWindow()
        {
            InitializeComponent();
            chargerPage();
            chrono.Tick += chronoKeepTrack;
            chrono.Interval = new TimeSpan(0, 0, 1);
        }

        /// <summary>
        /// Charge la page avec les Textbox du Sudoku
        /// </summary>
        private void chargerPage()
        {
            for (int i = 0; i < 81; i++)
            {
                int left = 1, right = 1, bottom = 1, top = 1;
                TextBox a = new TextBox();
                //fait la bordure
                if (i == 2 || i == 5 || (i - 5) % 9 == 0 || (i - 2) % 9 == 0) //bordure droite colonne 3, 6
                    right = 2;
                if (i == 3 || i == 6 || (i - 6) % 9 == 0 || (i - 3) % 9 == 0) //bordure gauche colonne 4, 7
                    left = 2;
                if ((i >= 18 && i <= 26) || (i >= 45 && i <= 53)) //bordure bas ligne 3, 6
                    bottom = 2;
                if ((i >= 27 && i <= 35) || (i >= 54 && i <= 62)) //bordure haut ligne 4, 7
                    top = 2;
                if (i >= 0 && i <= 8) //pourtour bordure haut
                    top = 4;
                if (i >= 72 && i <= 80) //pourtour bordure bas
                    bottom = 4;
                if (i == 0 || i % 9 == 0) //pourtour bordure gauche
                    left = 4;
                if (i == 8 || (i - 8) % 9 == 0) //pourtour bordure droite
                    right = 4;
                a.BorderThickness = new Thickness(left: left, right: right, top: top, bottom: bottom);
                a.PreviewTextInput += txtBoxIsNumber;
                a.PreviewKeyDown += txtBoxNoSpace;
                gridSudoku.Children.Add(a);
            }
            AddLog("Création des textbox complétés");
        }

        private void Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void outPutLog(object sender, RoutedEventArgs e)
        {
            outputLog = !outputLog;
            tableauBord.Visibility = Visibility.Visible;
        }

        private void chronoKeepTrack(object sender, EventArgs e)
        {
            tempsFinal = DateTime.Now.Subtract(tempsDebut); //hh:mm:ss lance une erreur que je ne parvenais pas à résoudre
            this.CompteRebours.Header = string.Format("{0:00}:{1:00}:{2:00}", tempsFinal.Hours, tempsFinal.Minutes, tempsFinal.Seconds);
        }

        private void AddLog(string log)
        {
            if (tableauBord.Items.Count < int.MaxValue)
            {
                tableauBord.Items.Add(log);
                tableauBord.ScrollIntoView(tableauBord.Items[tableauBord.Items.Count - 1]);
            }
        }

        private void txtBoxNoSpace(object sender, KeyEventArgs e)
        {
            //il n'est pas possible de preview l'input dans le cas d'un espace alors previewkeyinput
            //si espace alors le contenu de la textbox ne change pas
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                if (outputLog)
                    AddLog("Erreur: espace refusé");
            }
        }

        private void txtBoxIsNumber(object sender, TextCompositionEventArgs e)
        {
            int a;
            if (!int.TryParse(e.Text, out a)) //si pas un nombre alors text de textbox change pas
            {
                e.Handled = true;
                if (outputLog)
                    AddLog(e.Timestamp + ": L'élément \"" + e.Text + "\" n'est pas un nombre valide");
            }
        }

        private void changeSudoku(object sender, RoutedEventArgs e)
        {
            imposerSudoku(sudoku1);
            if (!chrono.IsEnabled)
            {
                tempsDebut = DateTime.Now;
                chrono.Start();
            }
        }

        /// <summary>
        /// Transpose la matrice bidimensionnelle dans le uniformgrid
        /// </summary>
        /// <param name="sudoku"></param>
        private void imposerSudoku(int?[,] sudoku)
        {
            Reinitialiser();
            int compteur = 0; //compteur pour transposer dans le uniformgrid
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    (gridSudoku.Children[compteur] as TextBox).Text = "" + sudoku[i, j];
                    if (sudoku[i, j] != null)
                    {
                        (gridSudoku.Children[compteur] as TextBox).IsReadOnly = true;
                        (gridSudoku.Children[compteur] as TextBox).Background = Brushes.LightSkyBlue;
                    }
                    compteur++;
                }
            }
        }


        private void showSolution(object sender, RoutedEventArgs e)
        {
            imposerSudoku(solution);
        }

        private void Reinitialiser(object sender = null, RoutedEventArgs e = null)
        {
            chrono.Stop();
            nombreBons = 0;
            for (int i = 0; i < gridSudoku.Children.Count; i++)
            {
                (gridSudoku.Children[i] as TextBox).Text = "";
                (gridSudoku.Children[i] as TextBox).IsReadOnly = false;
                (gridSudoku.Children[i] as TextBox).Background = Brushes.LightGray;
            }
        }

        /// <summary>
        /// Vérifie le contenu des textbox
        /// </summary>
        private void Verifier(object sender = null, RoutedEventArgs e = null)
        {
            for (int i = 0; i < gridSudoku.Children.Count; i++)
            {
                if (i < 9)
                    doSomething(i, 0);
                else if (i > 8 && i < 18)
                    doSomething(i, 1);
                else if (i > 17 && i < 27)
                    doSomething(i, 2);
                else if (i > 26 && i < 36)
                    doSomething(i, 3);
                else if (i > 35 && i < 45)
                    doSomething(i, 4);
                else if (i > 44 && i < 54)
                    doSomething(i, 5);
                else if (i > 53 && i < 63)
                    doSomething(i, 6);
                else if (i > 62 && i < 72)
                    doSomething(i, 7);
                else
                    doSomething(i, 8);
            }
            if (nombreBons == 81 && nombreResultat < 5)
            {
                nombreResultat++;
                MenuItem resultat = new MenuItem();
                resultat.Header = string.Format("Resultat {0}: {1:00}:{2:00}:{3:00}", nombreResultat, tempsFinal.Hours, tempsFinal.Minutes, tempsFinal.Seconds);
                listeResultat.Items.Add(resultat);
            }
        }

        private void doSomething(int i, int j)
        {
            string a = (gridSudoku.Children[i] as TextBox).Text;
            if ((gridSudoku.Children[i] as TextBox).Text == solution[j, i % 9].ToString())
            {
                a += " à la case " + i + " correspond";
                (gridSudoku.Children[i] as TextBox).IsReadOnly = true;
                (gridSudoku.Children[i] as TextBox).Background = Brushes.LightSkyBlue;
                nombreBons++;
            }
            else
                a += "Aucune correspondance";
            if (outputLog)
                AddLog(a);
        }

    }
}
