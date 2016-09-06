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
using System.Windows.Shapes;

namespace laboPO01v2
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public string titrecomplet;
        public Options()
        {
            InitializeComponent();
        }

        private void btnAccepter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt2eDico.Text))
                MessageBox.Show("Le deuxième dictionnaire doit obligatoirement être remplit");
            else
                Close();
        }
    }
}
