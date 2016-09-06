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

namespace Sommatif_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int Indice = 0;
        static int[] monTab = new int[7];
        public MainWindow()
        {
            InitializeComponent();
        }
        public void changementTab(ref int[] monTab)
        {
            txtCase0.Text = Convert.ToString(monTab[0]);
            txtCase1.Text = Convert.ToString(monTab[1]);
            txtCase2.Text = Convert.ToString(monTab[2]);
            txtCase3.Text = Convert.ToString(monTab[3]);
            txtCase4.Text = Convert.ToString(monTab[4]);
            txtCase5.Text = Convert.ToString(monTab[5]);
            txtCase6.Text = Convert.ToString(monTab[6]);
        }
        public void nombreNul(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < monTab.Length; i++)
                monTab[i] = 0;
            changementTab(ref monTab);

        }
        public void nombreRandom(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bonjour!");
            Random tableau = new Random();
            for (int i = 0; i < monTab.Length; i++)
                monTab[i] = tableau.Next(-999, 999);
            changementTab(ref monTab);
        }

        public void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void btnMoins_Click(object sender, RoutedEventArgs e)
        {
            Indice -= 1;
            txtIndice.Text = Convert.ToString(Indice);
            if (Indice < -1)
            {
                MessageBox.Show("L'indice doit être supérieur ou égal à -1");
                Indice = -1;
                txtIndice.Text = Convert.ToString(Indice);
            }
        }

        public void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            Indice++;
            txtIndice.Text = Convert.ToString(Indice);
            if (Indice > 6)
            {
                MessageBox.Show("L'indice doit être inférieur ou égal à 6");
                Indice = 6;
                txtIndice.Text = Convert.ToString(Indice);
            }
        }

        private void btnIncrement_Click(object sender, RoutedEventArgs e)
        {
            if (Indice == -1)
            {
                for (int i = 0; i < monTab.Length; i++)
                    monTab[i] = monTab[i] + 1;
                changementTab(ref monTab);
            }
            else
            {
                monTab[Indice] = monTab[Indice] + 1;
                changementTab(ref monTab);
            }
        }

        public void btnDecrement_Click(object sender, RoutedEventArgs e)
        {
            if (Indice == -1)
            {
                for (int i = 0; i < monTab.Length; i++)
                    monTab[i] = monTab[i] - 1;
                changementTab(ref monTab);
            }
            else
            {
                monTab[Indice] = monTab[Indice] - 1;
                changementTab(ref monTab);
            }
        }

        public void btnMoyenne_Click(object sender, RoutedEventArgs e)
        {
            double moyenne = 0;
            foreach (int elements in monTab)
                moyenne += elements;
            moyenne = moyenne / 7;
            MessageBox.Show(string.Format("La moyenne du tableau est d'environ {0:0.##}", moyenne));
        }
    }
}
