using System;
using System.Collections.Generic;
using System.IO;
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

namespace LaboFichiers1
{
    /// <summary>
    /// Interaction logic for FenetrePrincipale.xaml
    /// </summary>
    public partial class FenetrePrincipale : Window
    {
        private FileStream TexteLire = new FileStream(@"..\..\data\Texte.bin", FileMode.OpenOrCreate, FileAccess.Read), NombreLire = new FileStream(@"..\..\data\Nombre.bin", FileMode.OpenOrCreate, FileAccess.Read);
        public FenetrePrincipale()
        {
            InitializeComponent();
            BinaryReader lireTexte = new BinaryReader(TexteLire), lireNombres = new BinaryReader(NombreLire);
            while (lireTexte.PeekChar() > 0)
            {
                txtTexte.Text = lireTexte.ReadString();
            }
            while (lireNombres.PeekChar() > 0)
            {
                listNombres.Items.Add(lireNombres.ReadString());
            }
            TexteLire.Dispose();
            NombreLire.Dispose();
        }

        private void btnSaveListe_Click(object sender, RoutedEventArgs e)
        {
            FileStream NombreEcrire = new FileStream(@"..\..\data\Nombre.bin", FileMode.Create, FileAccess.Write);
            BinaryWriter ecrireNombre = new BinaryWriter(NombreEcrire);
            foreach (var chose in listNombres.Items)
            {
                ecrireNombre.Write(chose.ToString());
            }
        }

        private void btnViderListe_Click(object sender, RoutedEventArgs e)
        {
            listNombres.Items.Clear();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            int nombre;
            int.TryParse(txtNombreAjouter.Text, out nombre);
            listNombres.Items.Add(nombre);
            txtNombreAjouter.Text = "";
        }

        private void btnSaveTexte_Click(object sender, RoutedEventArgs e)
        {
            FileStream TexteEcrire = new FileStream(@"..\..\data\Texte.bin", FileMode.Create, FileAccess.Write);
            BinaryWriter ecrireTexte = new BinaryWriter(TexteEcrire);
            ecrireTexte.Write(txtTexte.Text);
        }
    }
}
