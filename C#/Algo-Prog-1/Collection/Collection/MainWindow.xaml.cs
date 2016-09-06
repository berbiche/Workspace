using System;
using System.Collections;
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

namespace Collection
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*Je me suis dit que c'était une bonne occasion pour utiliser les structures.
         *Je ne peux pas initialiser une ArrayList de struct alors j'ai fait une List puisque c'est aussi une collection et que l'exercice porte sur les List
         *Comme vous l'avez mentionné, les List sont plus évolués que les ArrayList.
        */
        #region Variables et structure
        struct AutoPrix
        {
            public int prix;
            public string auto;
        }
        static List<AutoPrix> collAutoPrix = new List<AutoPrix>();
        static AutoPrix[] tabAutoPrix;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Détruit Élément dans Collection
        private void btnNoDetruire_Click(object sender, RoutedEventArgs e)
        {
            int ElemX;
            int.TryParse(txtElemX.Text, out ElemX);
            try
            {
                collAutoPrix.RemoveAt(ElemX);
            }
            catch (Exception F)
            {
                MessageBox.Show("" + F);
            }
            txtElemX.Clear();
        }
        #endregion
        #region Affiche Collection
        /// <summary>
        /// Affiche les éléments de la collection dans la listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAffCol_Click(object sender, RoutedEventArgs e)
        {
            listCollection.Items.Clear();
            foreach(var elements in collAutoPrix)
            {
                //listCollection.Items.Add(string.Format("Auto: {0} Prix: ${1}",elements.auto, elements.prix));
                listCollection.Items.Add(string.Format("Auto: {0}", elements.auto));
                listCollection.Items.Add(string.Format("Prix: ${0}", elements.prix));
            }
        }
        #endregion
        #region Affiche Tableau
        /// <summary>
        /// Affiche les éléments du tableau dans la listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAffTab_Click(object sender, RoutedEventArgs e)
        {
            listTableau.Items.Clear();
            if (tabAutoPrix != null)
            {
                foreach (var elements in tabAutoPrix)
                {
                    //listTableau.Items.Add(string.Format("Auto: {0} Prix: {1}", elements.auto, elements.prix));
                    listTableau.Items.Add(string.Format("Auto: {0}", elements.auto));
                    listTableau.Items.Add(string.Format("Prix: ${0}", elements.prix));
                }
            }
        }
        #endregion
        #region Capacité Collection
        /// <summary>
        /// Donne la capacité actuelle de la collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCapCol_Click(object sender, RoutedEventArgs e)
        {
            txtCap.Text = "" + collAutoPrix.Capacity;
        }
        #endregion
        #region Ajout collection
        /// <summary>
        /// Ajoute le nom et le prix de l'auto rentrés par l'utilisateur dans les textbox dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAjoutCol_Click(object sender, RoutedEventArgs e)
        {
            AutoPrix uneAuto = new AutoPrix();
            int.TryParse(txtPrixAuto.Text, out uneAuto.prix);
            uneAuto.auto = txtNomAuto.Text;
            collAutoPrix.Add(uneAuto);
            txtNomAuto.Clear();
            txtPrixAuto.Clear();
        }
        #endregion
        #region Count collection
        /// <summary>
        /// Donne le nombre d'élément dans la collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnNbrCol_Click(object sender, RoutedEventArgs e)
        {
            txtNbrElem.Text = "" + collAutoPrix.Count;
        }
        #endregion
        #region Vider Collection
        /// <summary>
        /// Vide la collection de son contenu en la réinitialisant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViderCol_Click(object sender, RoutedEventArgs e)
        {
            collAutoPrix = new List<AutoPrix>();
            //semblerait que c'est plus rapide que de changer 
        }
        #endregion
        #region Inverser Collection
        /// <summary>
        /// Renverse l'ordre des éléments de la collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInversercol_Click(object sender, RoutedEventArgs e)
        {
            collAutoPrix.Reverse();
        }
        #endregion
        #region Ajoute Tableau dans Collection
        /// <summary>
        /// Ajoute les éléments du tableau dans la collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutTabCol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var elements in tabAutoPrix)
                {
                    collAutoPrix.Add(elements);
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("" + f);
            }
        }
        #endregion
        #region Remplace Tableau par Collection
        /// <summary>
        /// Remplace les éléments du tableau par ceux contenu dans la collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplaceTabCol_Click(object sender, RoutedEventArgs e)
        {
            Array.Resize(ref tabAutoPrix, collAutoPrix.Count);
            for (int i = 0; i < collAutoPrix.Count; i++)
            {
                tabAutoPrix[i] = collAutoPrix[i];
            }
        }
        #endregion
    }
}
