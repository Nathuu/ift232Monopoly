using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApplication1.sources.Carreaux;
using WpfApplication1.sources.Carreaux.CarreauConcret;

namespace WpfApplication1.sources
{
    public class Joueur
    {
        public Image Image { get; private set; }
        public long Argent { get;  set; }
        public Position Position { get; set; } // un objet de type Position
        public string Nom { get;  set; }
        public List<CarreauAchetable> Proprietes { get; private set; }

        public bool EstPrisonnier { get; set; }
        public bool ACarteSortirPrison { get; set; } // Il y a deux cartes de ce type
        public bool PeutPasserGo { get; set; }
        public int PositionCarreau { get; set; }
        public int CompteurDeDouble { get; set; }


        //Joueur n'a pas de propriétés? Oui il a une liste de proprietes
        public Joueur(String nom, Image image)//une piece construite va toujours avoir la meme argent et meme position de depart
        {
            this.Nom = nom;
            this.Image = image;
            this.Argent = 500;
            this.PositionCarreau = 0;
            this.Position = new Position(1, 1);
            this.Image.Width = 20;
            this.Image.Height = 20;
            this.ACarteSortirPrison = false;
            this.EstPrisonnier = false;
            this.PeutPasserGo = true;
            this.Proprietes = new List<CarreauAchetable>();
            this.CompteurDeDouble = 0;
        }

        public int LanceDeuxDes(Random random1)// un dé est lancé
        {
            return LanceUnDes() + LanceUnDes();
        }
        public int LanceUnDes()// un dé est lancé
        {
            return Plateau.Instance.LanceUnDes();
        }

        internal void Sauvegarder(StreamWriter fichierSauvegarde)
        {
            fichierSauvegarde.WriteLine(Nom);
            fichierSauvegarde.WriteLine(Position.colonne);
            fichierSauvegarde.WriteLine(Position.rangee);
            fichierSauvegarde.WriteLine(Argent);
            fichierSauvegarde.WriteLine(PositionCarreau);
        }


        // Début du tour du joueur : va appeller LanceDes,Bouger,...
        public void JouerSonTour()
        {
            if (EstPrisonnier)
            {
                TenteSortirPrison();
            }
            else
            {
                                
                int coupDe1 = LanceUnDes();
                int coupDe2 = LanceUnDes();
           
                int sommeDes = coupDe1 + coupDe2;
                MessageBox.Show("Vous avez eu: (" + coupDe1 + " + " + coupDe2 + ")", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);

                if (coupDe1 == coupDe2)
                {
                    CompteurDeDouble += 1;
                    if(CompteurDeDouble == 3)
                    {
                        PositionCarreau = 30;
                        Position = Carreau.conversionInt2Position(PositionCarreau);
                        getCarreauActuel().execute();
                    }
                    else{
                        Avancer(sommeDes);
                        JouerSonTour();
                    }
                }
                else
                {
                    CompteurDeDouble = 0;
                    Avancer(sommeDes);
                }
            }
        }


        public void Avancer(int nbCases)
        {
            MessageBox.Show("Joueur " + Nom + " avance de " + nbCases + " cases", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            int nouvellePosition = (this.PositionCarreau + nbCases) % Plateau.Instance.NombreCarreauxMaximal;
            if (nouvellePosition < this.PositionCarreau && PeutPasserGo)
                Depot(Plateau.Instance.MontantCarreauDepart);

            this.PositionCarreau = nouvellePosition;
            this.Position = Carreau.conversionInt2Position(this.PositionCarreau);
            Grid.SetRow(this.Image, this.Position.rangee + 1);
            Grid.SetColumn(this.Image, this.Position.colonne + 1);
            getCarreauActuel().execute();


        }

        /// <summary>
        /// Hypotheque les proprietes jusqu'a etre capable de payer
        /// </summary>
        /// <param name="aPayer"></param>
        /// <returns>Si le joueur peut payer le montant</returns>
        public bool PeutPayer(long aPayer)//on regarde si le joueur peut payer tel montant
        {
            if (Argent > aPayer) return true;
            for (int index = 0; index < Proprietes.Count; ++index)
            {
                hypothequer(Proprietes[index]);
                if (Argent > aPayer) return true;
            }

            return false;
        }


        /**************************************************************************
         * valeur d'entree : ce que le joueur doit payer
         * valeur Sortie : nouvelle argent de joueur
         * fait la transaction entre deux joueurs
         Cete fonction ne prend pas encore en compte les Avoirs du joueurs(maison, terrain)
         ************************************************************************/
        public long Payer(long aPayer)
        {
            Argent -= aPayer;
            MessageBox.Show("Joueur " + Nom + " paie " + aPayer + "$.\n" +
            "Montant dans le compte: " + Argent + "$", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            return Argent; // return l'argent  que le jouer a ;
        }
        public long Depot(long deposer)
        {

            Argent += deposer;
            MessageBox.Show("Joueur " + Nom + " dépose " + deposer + "$ dans son compte.\n" +
                "Montant dans le compte: " + Argent + "$", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            return Argent; // on retourne le nouveau argent
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propriete"></param>
        /// <returns>La propriete a bien ete hypothequee</returns>
        private bool hypothequer(CarreauAchetable propriete)
        {
            if (!propriete.estHypothequee)
            {
                propriete.estHypothequee = true;
                Depot(propriete.getPrixAchat() / 2);
                return true;
            }
            else
                return false;
        }

        public void faitFaillite()
        {
            Console.Write("Tu vas rotter du sang enculé!\n");
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// retourne un carreau selon la position du jouer.
        /// </returns>
        public Carreau getCarreauActuel() 
        {
            return Plateau.Instance.getCarreau(PositionCarreau);
        }


        public bool estSeulProprietaireDeMemeCouleur(CarreauPropriete.Couleurs couleur)
        {
            int nbProprieteCouleur = 0;
            foreach (CarreauPropriete propriete in Proprietes)
            {
                if (couleur == propriete.Couleur)
                {
                    nbProprieteCouleur++;
                }
            }
            if (couleur == CarreauPropriete.Couleurs.Brun ||
                couleur == CarreauPropriete.Couleurs.BleuFonce)
                return nbProprieteCouleur == 2;
            else
                return nbProprieteCouleur == 3;
        }

        public void TenteSortirPrison()
        {
            int coupDe1 = LanceUnDes();
            int coupDe2 = LanceUnDes();
            int somme = coupDe1 + coupDe2;
            MessageBox.Show("Joueur " + Nom + " tente de s'échapper.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);

            if (coupDe1 == coupDe2)
            {
                EstPrisonnier = false;
                MessageBox.Show("Joueur " + Nom + " est libre!", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                Avancer(somme);               
            }
            else
                MessageBox.Show("Joueur " + Nom + " resteen prison! ("+ coupDe1+" + "+ coupDe2+")", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public int getNbTrains()
        {
            int nbTrains = 0;
            foreach (CarreauAchetable c in Proprietes)
                if (c is CarreauTrain)
                    ++nbTrains;
            return nbTrains;
        }
    }
}
