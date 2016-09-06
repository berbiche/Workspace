using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laboPO01
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GestionDictionnaire ObjMot1 = new GestionDictionnaire();
        GestionDictionnaire ObjMot2 = new GestionDictionnaire();

        public MainWindow()
        {
            InitializeComponent();
        } // fin MainWindow

        // ajouter un mot au dictionnaire dans les 2 langues
        // la deuxième langue aura  la même clé que la première langue, afin de pouvoir trouver 
        // le mot équivalent de la deuxième langue lorsque l'usager inscrit un mot pour la première langue 
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string LaCle = txtMot1.Text;
            ObjMot1.EntrerUnMot(txtMot1.Text, Convert.ToChar(txtGenre1.Text), txtDefinition1.Text, LaCle);
            txtMot1.Clear();
            txtGenre1.Clear();
            txtDefinition1.Clear();

            ObjMot2.EntrerUnMot(txtMot2.Text, Convert.ToChar(txtGenre2.Text), txtDefinition2.Text, LaCle);
            txtMot2.Clear();
            txtGenre2.Clear();
            txtDefinition2.Clear();

        } // fin btbAjouter 

        
        // inscrire les informations générales au moment du load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GestionDictionnaire.TypeDictionnaire = "Français Anglais";
            ObjMot1.langue = "Français"; // vous pouvez modifier cette information
            ObjMot2.langue = "Anglais"; // vous pouvez modifier cette information
        } // fin Window_Loaded

        // afficher les informations sur le mot, et utiliser la clé de la première langue 
        // pour trouver son équivalent dans la deuxième langue 
        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            string LeMot = txtMot1.Text;
            string LaCle = txtMot1.Text; 
            char LeGenre = ' ';
            string LaDefinition =""; 
            ObjMot1.RetournerUnMot (ref LeMot, ref LeGenre, 
                                    ref LaDefinition,  LaCle);
            txtMot1.Text = LeMot;
            txtGenre1.Text = Convert.ToString(LeGenre);
            txtDefinition1.Text = LaDefinition;


            ObjMot2.RetournerUnMot(ref LeMot, ref LeGenre,
                                    ref LaDefinition,  LaCle);
            txtMot2.Text = LeMot;
            txtGenre2.Text = Convert.ToString(LeGenre);
            txtDefinition2.Text = LaDefinition;

        } // fin btnAfficher_Click

        // Afficher les titres lorsque demandé
        private void btnTitres_Click(object sender, RoutedEventArgs e)
        {
            lblTitre.Content = GestionDictionnaire.TypeDictionnaire;
            lblLangue1.Content = ObjMot1.langue;
            lblLangue2.Content = ObjMot2.langue;
        } // fin btnTitres_Click
    } // fin partial class MainWindow : Window
} // namespace laboPO01
