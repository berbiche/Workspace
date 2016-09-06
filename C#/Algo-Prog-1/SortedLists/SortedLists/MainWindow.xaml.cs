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

namespace SortedLists
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
            Voici une liste de voiture pour faciliter la tâche.
            Il suffit de la copier-coller et de mettre "//" devant la déclaration à la fin de ce bloc de commentaire.
            Vous pourrez aussi renommer rapidement les variables de la structure ainsi que son nom avec : clique droit sur la variable et l'option "rename"/"renommer"

            SortedList<string, Auto> ListeAuto = new SortedList<string, Auto> //les clés de la liste sont déjà triées par ordre alphabétique comme le fait la sortedlist à la base
            {
                { "AUDI" , new Auto() { nom = "R8 V10 Coupé 2017", prix = 184000.00 } },
                { "AUDI2" , new Auto() { nom = "Q5 2016", prix = 42600.00 } },
                { "CHEVROLET" , new Auto() { nom = "Spark 2016", prix = 11595.00 } },
                { "CHEVROLET2" , new Auto() { nom = "Corvette Stingray 2016", prix = 66495.00 } },
                { "FORD" , new Auto() { nom = "Escape 2016", prix = 23699.00 } },
                { "FORD2" , new Auto() { nom = "Mustang 2016", prix = 26398.00 } },
                { "FORD3" , new Auto() { nom = "Focus 2016", prix = 17199.00 } },
                { "FORD4" , new Auto() { nom = "Fiesta 2016", prix = 16049.00 } },
                { "HONDA" , new Auto() { nom = "Civic Berline 2015", prix = 17245.00 } },
                { "JEEP" , new Auto() { nom = "Grand Cherokee Laredo 2016", prix = 43395.00 } },
                { "JEEP2" , new Auto() { nom = "Compass Sport 2016", prix = 15745.00 } },
            };
        */

        SortedList<string, Auto> ListeAuto = new SortedList<string, Auto>();

        public int count = 0; //pour savoir où je suis dans la méthode "Afficher un à un"

        struct Auto
        {
            public double prix;
            public string nom;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        //Affiche chaque élément de la collection
        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            ResetMessage(Visibility.Visible);
            if (ListeAuto.Count > 0 && count < ListeAuto.Count)
            {
                txtMessage.Text = "Clé : " + ListeAuto.Keys[count] + " | Nom : " + ListeAuto.Values[count].nom + " | Prix : " + ListeAuto.Values[count].prix + " CAD";
                count++;
                if (count == ListeAuto.Count)
                    ResetField();
            }
            else
            {
                ResetField();
                txtMessage.Text = "Il n'y a aucune auto dans la collection";
            }
        }

        //Ajoute l'élément à la collection
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            ResetMessage(Visibility.Visible);
            if (!ListeAuto.ContainsKey(txtCode.Text))
            {
                Auto uneAuto = new Auto();
                double.TryParse(txtPrix.Text, out uneAuto.prix);
                uneAuto.nom = txtNom.Text;
                ListeAuto.Add(txtCode.Text, uneAuto);
                txtMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                ResetMessage();
                txtMessage.Text = "La clé \"" + txtCode.Text + "\" existe déjà.";
            }
            ResetField();
        }

        //pour remettre les textbox à 0
        private void ResetField(int qqc = 0)
        {
            txtCode.Clear(); //équivalent à txtCode.Text = null; 
            txtNom.Clear();
            txtPrix.Clear();
            count = qqc;
        }

        private void ResetMessage(Visibility qqc = Visibility.Hidden)
        {
            txtMessage.Clear();
            txtMessage.Visibility = qqc;
        }

        //Enlève l'élément de la collection
        private void btnDetruire_Click(object sender, RoutedEventArgs e)
        {
            ResetMessage(Visibility.Visible);
            if (ListeAuto.ContainsKey(txtCode.Text))
            {
                ListeAuto.Remove(txtCode.Text);
                txtMessage.Text = "La clé \"" + txtCode.Text + "\" existante a été retirée avec succès";
            }
            else
                txtMessage.Text = "Erreur, la clé \"" + txtCode.Text + "\" entrée n'existe pas dans la liste!";
            ResetField();
        }

        //Trouve l'élément dans la collection
        private void btnTrouver_Click(object sender, RoutedEventArgs e)
        {
            ResetMessage(Visibility.Visible);
            if (ListeAuto.ContainsKey(txtCode.Text))
            {
                txtMessage.Text = "Clé : " + txtCode.Text + " | Nom : " +  ListeAuto.Values[ListeAuto.IndexOfKey(txtCode.Text)].nom 
                    + "| Prix : " + ListeAuto.Values[ListeAuto.IndexOfKey(txtCode.Text)].prix + " CAD";
            }
            else
            {
                txtMessage.Text = "La clé \"" + txtCode.Text + "\" n'a pas été trouvée.";
            }
            ResetField(count);
        }
    }
}
