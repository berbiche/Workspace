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

namespace TP1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> Credit = new List<int>(), Debit = new List<int>(), lastItemAdded = new List<int>(); //il faut faire une liste pour tenir compte de l'ordre dans lequel les éléments sont ajoutés
        public long totalCredit = 0, totalDebit = 0; // pour éviter que l'addition de plusieurs int.max change le bit signe
        public MainWindow()
        {
            InitializeComponent();
            lblTemps.Content = lblTemps.Content + string.Format("{0:dd/MM/yyy}",DateTime.Now);
        }

        private void WriteInJournal(object sender, RoutedEventArgs e)
        {
            int temporaire = int.TryParse(txtMontant.Text, out temporaire) == true ? temporaire : -1; //opération ternaire, int.tryparse est une bool qui indique si la conversion est réussie ou non
            if (temporaire < 0)
                MessageBox.Show("Le nombre n'est pas valide", "Nombre Invalide", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (rdiCredit.IsChecked == true) //si le bouton radio "Crédit" est coché
            {
                totalCredit = 0;
                Credit.Add(temporaire); //ajouté à la liste
                lbCredit.Items.Clear();
                foreach (int trucs in Credit)
                {
                    lbCredit.Items.Add(trucs);
                    totalCredit += trucs;
                }
                lbCredit.Items.Add("Total: " + totalCredit);
                lastItemAdded.Add(0);
            }
            else
            {
                totalDebit = 0;
                Debit.Add(temporaire);
                lbDebit.Items.Clear();
                foreach (int choses in Debit)
                {
                    lbDebit.Items.Add(choses);
                    totalDebit += choses;
                }
                lbDebit.Items.Add("Total: " + totalDebit);
                lastItemAdded.Add(1);
            }
            txtMontant.Clear();
            Balance(totalCredit, totalDebit);
        }

        private void UndoLastWrite(object sender, RoutedEventArgs e)
        {
            if (lastItemAdded.Count > 0) //sinon il y a crash lors de la vérification de la valeur du champ du tableau dans la condition suivante
            {
                if (lastItemAdded[lastItemAdded.Count - 1] == 0 && Credit.Count > 0) //s'il y a moins d'un élément dans la liste alors il y aura un problème
                {
                    totalCredit = 0;
                    Credit.RemoveAt(Credit.Count - 1);
                    lbCredit.Items.Clear();
                    foreach (int trucs in Credit)
                    {
                        lbCredit.Items.Add(trucs);
                        totalCredit += trucs;
                    }
                    lbCredit.Items.Add("Total: " + totalCredit);
                    lastItemAdded.RemoveAt(lastItemAdded.Count - 1);
                }
                else if (Debit.Count > 0)
                {
                    totalDebit = 0;
                    Debit.RemoveAt(Debit.Count - 1);
                    lbDebit.Items.Clear();
                    foreach (int choses in Debit)
                    {
                        lbDebit.Items.Add(choses);
                        totalDebit += choses;
                    }
                    lbDebit.Items.Add("Total: " + totalDebit);
                    lastItemAdded.RemoveAt(lastItemAdded.Count - 1);
                }
                Balance(totalCredit, totalDebit);
            }
            else
                MessageBox.Show("Il n'y a aucune écriture à annuler!", "Aucun élément à retirer", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Balance(long totalCredit, long totalDebit)
        {
            long Balance;
            Balance = totalCredit - totalDebit;
            txtBalance.Text = "" + Balance;
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
