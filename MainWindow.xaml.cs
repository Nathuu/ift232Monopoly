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
using WpfApplication1.sources.Commandes;
using WpfApplication1.sources.Interface;

namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private CommandFactory commandFactory;


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
            commandFactory = CommandFactory.Instance;
            InitButtons();

            // Création des joueurs 
            Plateau.Instance.Joueurs.Add(new Joueur("Vert", pionImageVert));
            Plateau.Instance.Joueurs.Add(new Joueur("Rouge", pionImageRouge));
            Plateau.Instance.Joueurs.Add(new Joueur("Bleu", pionImageBleu));
            Plateau.Instance.Joueurs.Add(new Joueur("Jaune", pionImageJaune));
            Plateau.Instance.JoueurCourant = Plateau.Instance.Joueurs.First();
        }

        private void InitButtons()
        {
            // Possibilite d'implanter une factory de panels pour simplifier le code ici
            StackPanel panelButtonFinTour = new StackPanel();
            panelButtonFinTour.Name = "panelBtnFinTour";
            PlateauMonopoly.Children.Add(panelButtonFinTour);
            Grid.SetRow(panelButtonFinTour, 10);
            Grid.SetColumn(panelButtonFinTour, 6);
            Grid.SetColumnSpan(panelButtonFinTour, 2);

            // Possibilite d'implanter une factory de buttons pour simplifier le code ici
            ButtonMonopoly buttonLancerDes = new ButtonMonopoly();
            CommandLancerDes cmdLancerDes = commandFactory.CreateCommandLancerDes();
            buttonLancerDes.storeCommand(cmdLancerDes);
            buttonLancerDes.Name = "btnLancerDes";
            buttonLancerDes.Content = "Lancer les dés";
            buttonLancerDes.Click += buttonLancerDes.execute;
            panelButtonFinTour.Children.Add(buttonLancerDes);

            ButtonMonopoly buttonFinTour = new ButtonMonopoly();
            CommandFinTour cmdFinTour = commandFactory.CreateCommandFinTour();
            buttonFinTour.storeCommand(cmdFinTour);
            buttonFinTour.Name = "btnFinTour";
            buttonFinTour.Content = "Fin de tour";
            buttonFinTour.Click += buttonFinTour.execute;
            panelButtonFinTour.Children.Add(buttonFinTour);
            
            //  menuItemFichier
            MenuItemMonopoly menuSauvegarde = new MenuItemMonopoly();
            CommandSauvegarde cmdSauver = CommandFactory.Instance.CreateCommandSauvegarde();
            menuSauvegarde.storeCommand(cmdSauver);
            menuSauvegarde.Name = "menuSave";
            menuSauvegarde.Header = "Sauvegarde";
            menuSauvegarde.Click += menuSauvegarde.execute;
            menuItemFichier.Items.Add(menuSauvegarde);
        }

        private void Deplacer_click(object sender, RoutedEventArgs e)
        {
            int dep = int.Parse(txtBoxNbDeplacement.Text);
            Plateau.Instance.JoueurCourant.Avancer(dep);
        }
        private void Restaurer_Click(object sender, RoutedEventArgs e)
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

        private void txtBoxNbDeplacement_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void voirSiCaMarche()
        {

        }
    }
}
