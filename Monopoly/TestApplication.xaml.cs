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
using System.Windows.Shapes;
using WpfApplication1.sources;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for TestApplication.xaml
    /// </summary>
    public partial class TestApplication : Window
    {
        public TestApplication()
        {
            InitializeComponent();
            List<String> nomJoueurs = new List<string>();
            Plateau.Instance.Joueurs.ForEach(x => nomJoueurs.Add(x.Nom));
            cmbJouers.ItemsSource = nomJoueurs;
        }

        private void cmbJouers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Plateau.Instance.JoueurCourant = Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == cmbJouers.SelectedValue.ToString());
            InsertionInformationJoueur();
        }
        private void InsertionInformationJoueur()
        {
            txtNom.Text = Plateau.Instance.JoueurCourant.Nom;
            txtBudget.Text = Plateau.Instance.JoueurCourant.Argent.ToString();
            chkEstFaillite.IsChecked = !Plateau.Instance.JoueurCourant.EstVivant;
            lvProprietes.ItemsSource = Plateau.Instance.JoueurCourant.Proprietes;
            lvProprietesHypotheque.ItemsSource = Plateau.Instance.JoueurCourant.Hypotheques;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            long argent = Plateau.Instance.JoueurCourant.Argent;
            if (!Int64.TryParse(txtBudget.Text, out argent)) MessageBox.Show("Vous devez rentrer un nombre pour le budget monetaire du joueur. Espèce de YABEEEEE");
            Plateau.Instance.JoueurCourant.Argent = argent;

            Plateau.Instance.JoueurCourant.EstVivant = (bool) !chkEstFaillite.IsChecked;


            int dep = 0;
            if (!Int32.TryParse(txtBoxDeplacementJoueur.Text, out dep)) MessageBox.Show("Vous devez rentrer un chiffre pour le déplacement. Espèce de noob");
            Plateau.Instance.JoueurCourant.Avancer(dep);
            Close();
        }
    }

}
