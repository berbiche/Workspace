using System;
using System.Linq;
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
        #region Variables
        private int[,] sudoku1, solution;
        DispatcherTimer chrono = new DispatcherTimer(); DateTime tempsDebut = new DateTime(); TimeSpan tempsFinal = new TimeSpan();
        private int nombreBons = 0, nombreResultat = 0;
        private bool hasBeenSaved = false;
        #endregion

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
                    top = 6;
                if (i >= 72 && i <= 80) //pourtour bordure bas
                    bottom = 6;
                if (i == 0 || i % 9 == 0) //pourtour bordure gauche
                    left = 6;
                if (i == 8 || (i - 8) % 9 == 0) //pourtour bordure droite
                    right = 6;
                a.BorderThickness = new Thickness(left: left, right: right, top: top, bottom: bottom);
                a.BorderBrush = Brushes.Black;
                a.PreviewTextInput += txtBoxIsNumber;
                a.PreviewKeyDown += txtBoxNoSpace;
                gridSudoku.Children.Add(a);
            }
        }

        private void Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void chronoKeepTrack(object sender, EventArgs e)
        {
            tempsFinal = DateTime.Now.Subtract(tempsDebut); //hh:mm:ss lance une erreur que je ne parvenais pas à résoudre
            this.CompteRebours.Header = string.Format("{0:00}:{1:00}:{2:00}", tempsFinal.Hours, tempsFinal.Minutes, tempsFinal.Seconds);
        }

        private void txtBoxNoSpace(object sender, KeyEventArgs e)
        {
            //il n'est pas possible de preview l'input dans le cas d'un espace alors previewkeyinput
            //si espace alors le contenu de la textbox ne change pas
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtBoxIsNumber(object sender, TextCompositionEventArgs e)
        {
            int a;
            if (!int.TryParse(e.Text, out a)) //si pas un nombre alors text de textbox change pas
            {
                e.Handled = true;
            }
        }

        private void changeSudoku(object sender, RoutedEventArgs e)
        {
            Sudoku abc = new Sudoku();
            solution = abc._sudokuReponse;
            sudoku1 = abc._sudoku;
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
        private void imposerSudoku(int[,] sudoku)
        {
            Reinitialiser();
            int compteur = 0; //compteur pour transposer dans le uniformgrid
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j] != 0)
                    {
                        (gridSudoku.Children[compteur] as TextBox).Text = "" + sudoku[i, j];
                        (gridSudoku.Children[compteur] as TextBox).IsReadOnly = true;
                        (gridSudoku.Children[compteur] as TextBox).Foreground = Brushes.Red;
                    }
                    compteur++;
                }
            }
        }


        private void showSolution(object sender, RoutedEventArgs e)
        {
            imposerSudoku(solution);
        }

        private void doSomething(int i, int j)
        {
            if ((gridSudoku.Children[i] as TextBox).Text == solution[j, i % 9].ToString())
            {
                (gridSudoku.Children[i] as TextBox).IsReadOnly = true;
                (gridSudoku.Children[i] as TextBox).Foreground = Brushes.Green;
                (gridSudoku.Children[i] as TextBox).Style = this.FindResource("correct") as Style;
                nombreBons++;
            }
        }

        private void Reinitialiser(object sender = null, RoutedEventArgs e = null)
        {
            chrono.Stop();
            nombreBons = 0;
            hasBeenSaved = false;
            for (int i = 0; i < gridSudoku.Children.Count; i++)
            {
                (gridSudoku.Children[i] as TextBox).Text = "";
                (gridSudoku.Children[i] as TextBox).IsReadOnly = false;
                (gridSudoku.Children[i] as TextBox).Style = this.FindResource("reinitialise") as Style;
            }
        }

        private void Sauver(object sender, RoutedEventArgs e)
        {
            if (!hasBeenSaved)
            {
                setTrue(gridSudoku.Children);
                hasBeenSaved = true;
            }
        }

        private void setTrue(UIElementCollection abc)
        {
            foreach (var items in abc)
            {
                if ((items as TextBox).Text != "")
                    (items as TextBox).IsReadOnly = true;
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
            if (nombreBons == 81)
            {
                nombreResultat++;
                MenuItem resultat = new MenuItem();
                resultat.Header = string.Format("Resultat {0}: {1:00}:{2:00}:{3:00}", nombreResultat, tempsFinal.Hours, tempsFinal.Minutes, tempsFinal.Seconds);
                listeResultat.Items.Add(resultat);
                if (listeResultat.Visibility == Visibility.Collapsed || listeResultat.Visibility == Visibility.Hidden)
                    listeResultat.Visibility = Visibility.Visible;
            }
        }
    }
}
