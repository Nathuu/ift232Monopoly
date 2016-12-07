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
            Plateau.Instance.Joueurs.Add(new Joueur("Vert", pionImageVert));
            Plateau.Instance.Joueurs.Add(new Joueur("Rouge", pionImageRouge));
            Plateau.Instance.Joueurs.Add(new Joueur("Bleu", pionImageBleu));
            Plateau.Instance.Joueurs.Add(new Joueur("Jaune", pionImageJaune));
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

            //  menuItemFichier
            MenuItemMonopoly menuTest = new MenuItemMonopoly();
            CommandTest cmdTest = CommandFactory.Instance.CreateCommandTest();
            menuTest.storeCommand(cmdTest);
            menuTest.Name = "menuTest";
            menuTest.Header = "Panneau Test";
            menuTest.Click += menuTest.execute;
            menuItemTest.Items.Add(menuTest);


            MenuItemMonopoly menuItemJoueurRouge = new MenuItemMonopoly();
            CommandStatistique cmdStatRouge = CommandFactory.Instance.CreateCommandStatistique("Rouge");
            menuItemJoueurRouge.storeCommand(cmdStatRouge);
            menuItemJoueurRouge.Name = "menuItemJoueurRouge";
            menuItemJoueurRouge.Header = "Joueur Rouge";
            menuItemJoueurRouge.Click += menuItemJoueurRouge.execute;
            menuItemStatistique.Items.Add(menuItemJoueurRouge);


            MenuItemMonopoly menuItemJoueurVert = new MenuItemMonopoly();
            CommandStatistique cmdStatVert = CommandFactory.Instance.CreateCommandStatistique("Vert");
            menuItemJoueurVert.storeCommand(cmdStatVert);
            menuItemJoueurVert.Name = "menuItemJoueurVert";
            menuItemJoueurVert.Header = "Joueur Vert";
            menuItemJoueurVert.Click += menuItemJoueurVert.execute;
            menuItemStatistique.Items.Add(menuItemJoueurVert);
            
            MenuItemMonopoly menuItemJoueurBleu = new MenuItemMonopoly();
            CommandStatistique cmdStatbleu = CommandFactory.Instance.CreateCommandStatistique("Bleu");
            menuItemJoueurBleu.storeCommand(cmdStatbleu);
            menuItemJoueurBleu.Name = "menuItemJoueurBleu";
            menuItemJoueurBleu.Header = "Joueur Bleu";
            menuItemJoueurBleu.Click += menuItemJoueurBleu.execute;
            menuItemStatistique.Items.Add(menuItemJoueurBleu);

            MenuItemMonopoly menuItemJoueurJaune = new MenuItemMonopoly();
            CommandStatistique cmdStatJaune = CommandFactory.Instance.CreateCommandStatistique("Jaune");
            menuItemJoueurJaune.storeCommand(cmdStatJaune);
            menuItemJoueurJaune.Name = "menuItemJoueurJaune";
            menuItemJoueurJaune.Header = "Joueur Jaune";
            menuItemJoueurJaune.Click += menuItemJoueurJaune.execute;
            menuItemStatistique.Items.Add(menuItemJoueurJaune);
            

        }

        private void menuItemJoueurRouge_Click(object sender, RoutedEventArgs e)
        {
            InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == "Rouge"));
            //À titre d'information : Tout le code en commentaire revient au meme qu'au lambda 
            //InformationJoueur infoWindow = null;
            //foreach (Joueur item in Plateau.Instance.Joueurs)
            //{
            //    if (item.Nom == "Rouge")
            //    {
            //        InformationJoueur infoWindow = new InformationJoueur(item);
            //        break;
            //    }
            //}
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

        private void Faillite_click(object sender, RoutedEventArgs e)
        {
            Plateau.Instance.JoueurCourant.FaitFaillite();
        }
        private void menuTest_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
