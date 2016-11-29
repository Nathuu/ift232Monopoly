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

namespace WpfApplication1.sources
{
    public class Joueur
    {
        public Image Image { get; private set; }
        public long Argent { get;  set; }
        public Position Position { get; private set; } // un objet de type Position
        public string Nom { get;  set; }
        public List<CarreauPropriete> Proprietes { get; private set; }
        public bool EstPrisonnier { get; set; }
        public bool ACarteSortirPrison { get; set; } // Il y a deux cartes de ce type
        public bool PeutPasserGo { get; set; }
        public int PositionCarreau { get; set; }

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
            this.Proprietes = new List<CarreauPropriete>();
        }

        public int LanceDeuxDes()// le joueur lance les dés
        {
            return LanceUnDes() + LanceUnDes();
        }
        public int LanceUnDes()// un dé est lancé
        {
            Random random = new Random();
            int des = random.Next(1, 6); // va retourner entre 1 et 6 
            return des;
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
                int coupDe = LanceDeuxDes();
                MessageBox.Show("Joueur " + Nom + " avance de " + coupDe + " cases", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                Avancer(coupDe);
            }
        }


        public void Avancer(int nbCases)
        {
            int nouvellePosition = (this.PositionCarreau + nbCases) % Plateau.Instance.NombreCarreauxMaximal;
            if (nouvellePosition < this.PositionCarreau && PeutPasserGo)
                Depot(Plateau.Instance.MontantCarreauDepart);

            this.PositionCarreau = nouvellePosition;
            this.Position = Carreau.conversionInt2Position(this.PositionCarreau);
            Grid.SetRow(this.Image, this.Position.rangee + 1);
            Grid.SetColumn(this.Image, this.Position.colonne + 1);
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
                if (Argent > aPayer) return true;
                hypothequer(Proprietes[index]);
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
        private bool hypothequer(CarreauPropriete propriete)
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
            MessageBox.Show("Joueur " + Nom + " tente de s'échapper.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            if (LanceUnDes() == LanceUnDes())
            {
                EstPrisonnier = false;
                MessageBox.Show("Joueur " + Nom + " est libre!", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
