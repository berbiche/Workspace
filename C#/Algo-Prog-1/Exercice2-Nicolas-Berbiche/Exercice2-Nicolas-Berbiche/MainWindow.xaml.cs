using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercice2_Nicolas_Berbiche
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtNomVisiteur.Width = 0;
        }

        private void btnGauche_Click(object sender, RoutedEventArgs e)
        {
            //doit ajouter les éléments sélectionnés dans la liste visiteurs
            List<object> thingsToRemove = new List<object>();
            foreach (string items in listExclus.SelectedItems)
            {
                listVisiteur.Items.Add(items);
                thingsToRemove.Add(items);
            }
            foreach (object items in thingsToRemove)
                listExclus.Items.Remove(items);
        }

        private void btnDroite_Click(object sender, RoutedEventArgs e)
        {
            //doit ajouter les éléments sélectionnés dans la liste des exclus
            List<object> choseAEnlever = new List<object>();
            foreach (string items in listVisiteur.SelectedItems)
            {
                listExclus.Items.Add(items);
                choseAEnlever.Add(items);
            }
            foreach (object items in choseAEnlever)
                listVisiteur.Items.Remove(items);
        }

        private void btnVisiteur_Click(object sender, RoutedEventArgs e)
        {
            //vide la liste des visiteurs
            listVisiteur.Items.Clear();
        }

        private void btnExclus_Click(object sender, RoutedEventArgs e)
        {
            //vide la liste des exclus
            listExclus.Items.Clear();
        }

        private void btnAjout_Click(object sender, RoutedEventArgs e)
        {
            //ajoute un visiteur à la liste des visiteurs
            listVisiteur.Items.Add(txtNomVisiteur.Text);
            txtNomVisiteur.Text = string.Empty;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtNomVisiteur.Width = 15 * sliderABC.Value;
        }
    }
}
