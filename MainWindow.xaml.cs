<<<<<<< HEAD
﻿using System;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Plateau plateauPrincipal;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        public MainWindow()
        {
            InitializeComponent();
            plateauPrincipal = new Plateau(canvas);
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            /*BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("logo.png");
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;*/
        }
    }
}
=======
﻿using System;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Plateau.Instance.sauvegarderPartie();
        }

        private void btnRestaurer_Click(object sender, RoutedEventArgs e)
        {
            Plateau.Instance.restaurerPartie(); // il faudrait demander le nom du fichier le mettre en parametre !!!JN & SARA!!!
        }
        private void menuItemFichier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemJoueurRouge_Click(object sender, RoutedEventArgs e)
        {
            InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x=> x.Nom == "Rouge"));
            infoWindow.ShowDialog();
        }

        private void menuItemJoueurVert_Click(object sender, RoutedEventArgs e)
        {
            InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == "Vert"));
            infoWindow.ShowDialog();
        }

        private void menuItemJoueurBleu_Click(object sender, RoutedEventArgs e)
        {
            InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == "Bleu"));
            infoWindow.ShowDialog();
        }

        private void menuItemJoueurJaune_Click(object sender, RoutedEventArgs e)
        {
            InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == "Jaune"));
            infoWindow.ShowDialog();
        }
    }
}
>>>>>>> 5cc1b3f45a9fc6ead5d3b890f2c0aa29b74e55d3
