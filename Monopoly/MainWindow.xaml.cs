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
using WpfApplication1.sources.Carreaux;
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
            Plateau.Instance.Joueurs.Add(new Joueur("Vert", pionImageVert, this));
            Plateau.Instance.Joueurs.Add(new Joueur("Rouge", pionImageRouge, this));
            Plateau.Instance.Joueurs.Add(new Joueur("Bleu", pionImageBleu, this));
            Plateau.Instance.Joueurs.Add(new Joueur("Jaune", pionImageJaune, this));
            Plateau.Instance.JoueurRestant = Plateau.Instance.Joueurs.Count();
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

            // Possibilite d'implanter une factory de buttons pour simplifier le code ici
            ButtonMonopoly buttonEchangerProprietes = new ButtonMonopoly();
            CommandEchangerProprietes cmdEchangerProprietes = commandFactory.CreateCommandEchangerProprietes();
            buttonEchangerProprietes.storeCommand(cmdEchangerProprietes);
            buttonEchangerProprietes.Name = "btnEchangerProprietes";
            buttonEchangerProprietes.Content = "Échanger les propriétés";
            buttonEchangerProprietes.Click += buttonEchangerProprietes.execute;
            panelButtonFinTour.Children.Add(buttonEchangerProprietes);

            //  menuItemFichier
            MenuItemMonopoly menuSauvegarde = new MenuItemMonopoly();
            CommandSauvegarde cmdSauver = CommandFactory.Instance.CreateCommandSauvegarde();
            menuSauvegarde.storeCommand(cmdSauver);
            menuSauvegarde.Name = "menuSave";
            menuSauvegarde.Header = "Sauvegarde";
            menuSauvegarde.Click += menuSauvegarde.execute;
            menuItemFichier.Items.Add(menuSauvegarde);

            //  menuItemFichier
            MenuItemMonopoly menuRestaure = new MenuItemMonopoly();
            CommandRestaurer cmdRestaurer = CommandFactory.Instance.CreateCommandRestaurer();
            menuRestaure.storeCommand(cmdRestaurer);
            menuRestaure.Name = "menuRestaure";
            menuRestaure.Header = "Restaurer";
            menuRestaure.Click += menuRestaure.execute;
            menuItemFichier.Items.Add(menuRestaure);




            MenuItemMonopoly menuItemJoueurRouge = new MenuItemMonopoly();
            menuItemJoueurRouge.Name = "menuItemJoueurRouge";
            menuItemJoueurRouge.Header = "Joueur Rouge";
            menuItemJoueurRouge.Click += menuItemJoueurRouge.execute;


            MenuItemMonopoly menuItemJoueurVert = new MenuItemMonopoly();
            menuItemJoueurVert.Name = "menuItemJoueurVert";
            menuItemJoueurVert.Header = "Joueur Vert";
            menuItemJoueurVert.Click += menuItemJoueurVert.execute;

            MenuItemMonopoly menuItemJoueurBleu = new MenuItemMonopoly();
            menuItemJoueurBleu.Name = "menuItemJoueurBleu";
            menuItemJoueurBleu.Header = "Joueur Bleu";
            menuItemJoueurBleu.Click += menuItemJoueurBleu.execute;

            MenuItemMonopoly menuItemJoueurJaune = new MenuItemMonopoly();
            menuItemJoueurJaune.Name = "menuItemJoueurJaune";
            menuItemJoueurJaune.Header = "Joueur Jaune";
            menuItemJoueurJaune.Click += menuItemJoueurJaune.execute;


        }

        //private void menuItemJoueurRouge_Click(object sender, RoutedEventArgs e)
        //{
        //    InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == "Rouge"));
        //    //À titre d'information : Tout le code en commentaire revient au meme qu'au lambda 
        //    //InformationJoueur infoWindow = null;
        //    //foreach (Joueur item in Plateau.Instance.Joueurs)
        //    //{
        //    //    if (item.Nom == "Rouge")
        //    //    {
        //    //        InformationJoueur infoWindow = new InformationJoueur(item);
        //    //        break;
        //    //    }
        //    //}
        //    infoWindow.ShowDialog();
        //}
       
        private void Faillite_click(object sender, RoutedEventArgs e)
        {
            Plateau.Instance.JoueurCourant.FaitFaillite();
        }
        private void menuTest_Click(object sender, RoutedEventArgs e)
        {
            TestApplication test = new TestApplication();
            test.ShowDialog();
        }

        public void MajInformationJoueurs(Joueur joueur)
        {
            switch (joueur.Nom.ToUpper())
            {
                case "ROUGE":
                    string affichageProp1 = "";
                    string affichagePropHypothequer1 = "";
                    string affichageTrain1 = "";
                    string affichageService1 = "";

                    joueur.Proprietes.ForEach(x => affichageProp1 += (x.ToString() + ", "));
                    joueur.Proprietes.Where(x => x.EstHypotheque == true).ToList().ForEach(x => affichagePropHypothequer1 += x.ToString() + ", ");
                    joueur.Trains.ForEach(x => affichageTrain1 += (x.ToString() + ", "));
                    joueur.Services.ForEach(x => affichageService1 += (x.ToString() + ", "));
                
                    chkEstVivant1.IsChecked = joueur.EstVivant;
                    txtArgent1.Text = joueur.Argent.ToString();
                    lvPropriete1.Text = affichageProp1;
                    lvProprieteHypotheque1.Text = affichagePropHypothequer1;
                    chkAlaCartePrison1.IsChecked = joueur.ACarteSortirPrison;
                    txtTrain1.Text = affichageTrain1;
                    txtService1.Text = affichageService1;

                    break;
                case "JAUNE":
                    string affichageProp2 = "";
                    string affichagePropHypothequer2 = "";
                    string affichageTrain2 = "";
                    string affichageService2 = "";

                    joueur.Proprietes.ForEach(x => affichageProp2 += (x.ToString() + ", "));
                    joueur.Proprietes.Where(x => x.EstHypotheque == true).ToList().ForEach(x => affichagePropHypothequer2 += x.ToString() + ", ");
                    joueur.Trains.ForEach(x => affichageTrain2 += (x.ToString() + ", "));
                    joueur.Services.ForEach(x => affichageService2 += (x.ToString() + ", "));

                    chkEstVivant2.IsChecked = joueur.EstVivant;
                    txtArgent2.Text = joueur.Argent.ToString();
                    lvPropriete2.Text = affichageProp2;
                    lvProprieteHypotheque2.Text = affichagePropHypothequer2;
                    chkAlaCartePrison2.IsChecked = joueur.ACarteSortirPrison;
                    txtTrain2.Text = affichageTrain2;
                    txtService2.Text = affichageService2;

                    break;
                case "BLEU":
                    string affichageProp3 = "";
                    string affichagePropHypothequer3 = "";
                    string affichageTrain3 = "";
                    string affichageService3 = "";

                    joueur.Proprietes.ForEach(x => affichageProp3 += (x.ToString() + ", "));
                    joueur.Proprietes.Where(x => x.EstHypotheque == true).ToList().ForEach(x => affichagePropHypothequer3 += x.ToString() + ", ");
                    joueur.Trains.ForEach(x => affichageTrain3 += (x.ToString() + ", "));
                    joueur.Services.ForEach(x => affichageService3 += (x.ToString() + ", "));

                    chkEstVivant3.IsChecked = joueur.EstVivant;
                    txtArgent3.Text = joueur.Argent.ToString();
                    lvPropriete3.Text = affichageProp3;
                    lvProprieteHypotheque3.Text = affichagePropHypothequer3;
                    chkAlaCartePrison3.IsChecked = joueur.ACarteSortirPrison;
                    txtTrain3.Text = affichageTrain3;
                    txtService3.Text = affichageService3;

                    break;
                case "VERT":
                    string affichageProp4 = "";
                    string affichagePropHypothequer4 = "";
                    string affichageTrain4 = "";
                    string affichageService4 = "";

                    joueur.Proprietes.ForEach(x => affichageProp4 += (x.ToString() + ", "));
                    joueur.Proprietes.Where(x => x.EstHypotheque == true).ToList().ForEach(x => affichagePropHypothequer4 += x.ToString() + ", ");
                    joueur.Trains.ForEach(x => affichageTrain4 += (x.ToString() + ", "));
                    joueur.Services.ForEach(x => affichageService4 += (x.ToString() + ", "));

                    chkEstVivant4.IsChecked = joueur.EstVivant;
                    txtArgent4.Text = joueur.Argent.ToString();
                    lvPropriete4.Text = affichageProp4;
                    lvProprieteHypotheque4.Text = affichagePropHypothequer4;
                    chkAlaCartePrison4.IsChecked = joueur.ACarteSortirPrison;
                    txtTrain4.Text = affichageTrain4;
                    txtService4.Text = affichageService4;

                    break;
            }
        }
    }
}
