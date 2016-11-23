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
using WpfApplication1.sources;

namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            // Création des joueurs 
            Plateau.Instance.Joueurs.Add(new Joueur("Vert", pionImageVert));
            Plateau.Instance.Joueurs.Add(new Joueur("Rouge", pionImageRouge));
            Plateau.Instance.Joueurs.Add(new Joueur("Bleu", pionImageBleu));
            Plateau.Instance.Joueurs.Add(new Joueur("Jaune", pionImageJaune));
            Plateau.Instance.JoueurCourant = Plateau.Instance.Joueurs.First();
        }
        private void btnFinTour_Click(object sender, RoutedEventArgs e)
        {
            Plateau.Instance.FinTour();
        }
    }
}
