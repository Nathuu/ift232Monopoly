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
using WpfApplication1.sources.Carreaux;

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
            this.DataContext = this;
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
            List<string> affichageProp = new List<string>();
            List<string> affichagePropHypothequer = new List<string>();
            Plateau.Instance.JoueurCourant.Proprietes.ForEach(x => affichageProp.Add(x.ToString()));
            Plateau.Instance.JoueurCourant.Proprietes.Where(x => x.EstHypotheque == true).ToList().ForEach(x => affichagePropHypothequer.Add(x.ToString()));

            txtNom.Text = Plateau.Instance.JoueurCourant.Nom;
            txtBudget.Text = Plateau.Instance.JoueurCourant.Argent.ToString();
            chkEstFaillite.IsChecked = !Plateau.Instance.JoueurCourant.EstVivant;
            gridProprietes.ItemsSource = affichageProp;
            gridProprietesHypotheque.ItemsSource = affichagePropHypothequer;
            txtBoxHypothequer.Text = "";
            txtBoxDeplacementJoueur.Text = "";
            txtBoxDeplacementJoueur.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            long argent = Plateau.Instance.JoueurCourant.Argent;
            if (txtBudget.Text != "")
            {
                if (!Int64.TryParse(txtBudget.Text, out argent)) MessageBox.Show("Vous devez rentrer un nombre pour le budget monetaire du joueur. Espèce de YABEEEEE");
                Plateau.Instance.JoueurCourant.Argent = argent;
            }
            Plateau.Instance.JoueurCourant.EstVivant = (bool)!chkEstFaillite.IsChecked;

            int hypotheque;
            if (txtBoxHypothequer.Text != "")
            {
                if (!Int32.TryParse(txtBoxHypothequer.Text, out hypotheque)) MessageBox.Show("Vous devez rentrer un chiffre pour l'hypotheque. Espèce de noob");
                Plateau.Instance.JoueurCourant.Hypothequer(Plateau.Instance.JoueurCourant.Proprietes.FirstOrDefault(x => x.positionCarreau == hypotheque));
            }

            int dehypotheque;
            if (txtBoxDehypotequer.Text != "")
            {
                if (!Int32.TryParse(txtBoxDehypotequer.Text, out dehypotheque)) MessageBox.Show("Vous devez rentrer un chiffre pour dehypothequer. Espèce de noob");
                Plateau.Instance.JoueurCourant.Dehypothequer(Plateau.Instance.JoueurCourant.Proprietes.FirstOrDefault(x => x.positionCarreau == dehypotheque));
            }

            int dep;
            if (txtBoxDeplacementJoueur.Text != "")
            {
                if (!Int32.TryParse(txtBoxDeplacementJoueur.Text, out dep)) MessageBox.Show("Vous devez rentrer un chiffre pour le déplacement. Espèce de noob");
                Plateau.Instance.JoueurCourant.Avancer(dep);
            }
            InsertionInformationJoueur();
        }
    }

}
