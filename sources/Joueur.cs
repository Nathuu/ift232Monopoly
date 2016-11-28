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

namespace WpfApplication1.sources
{
    public class Joueur
    {
        public Image Image { get; private set; }
        public long Argent { get;  set; }
        public Position Position { get; private set; } // un objet de type Position
        public string Nom { get;  set; }
        public List<CarreauPropriete> Proprietes { get; private set; }

        public int PositionCarreau { get; set; }
        public int NbCartesSortirPrison; // Il y a deux cartes de ce type

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
            this.NbCartesSortirPrison = 0;
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
            int coupDe = LanceDeuxDes();
            MessageBox.Show("Joueur " + Nom + " avance de " + coupDe + " cases", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            Avancer(coupDe);
        }


        public void Avancer(int nbCases)
        {
            this.PositionCarreau = (this.PositionCarreau + nbCases) % Plateau.Instance.NombreCarreauxMaximal;
            this.Position = Carreau.conversionInt2Position(this.PositionCarreau);
            Grid.SetRow(this.Image, this.Position.rangee + 1);
            Grid.SetColumn(this.Image, this.Position.colonne + 1);
            actionSurCase();
        }

        /// <summary>
        /// Hypotheque les proprietes jusqu'a etre capable de payer
        /// </summary>
        /// <param name="aPayer"></param>
        /// <returns>Si le joueur peut payer le montant</returns>
        public bool PeutPayer(long aPayer)//on regarde si le joueur peut payer tel montant
        {
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
            return Argent; // return l'argent  que le jouer a ;
        }
        public long Depot(long deposer)
        {

            Argent += deposer;
            return Argent; // on retourne le nouveau argent
        }

        /// <summary>
        /// détermine l'action à effectuer selon la case et la situation du joueur
        /// </summary>
        /// <returns>action effectuée</returns>
        public bool actionSurCase()
        {
            Carreau caseActuelle = getCarreauActuel();
            if (caseActuelle.estCarreauPayant())
            {
                CarreauPayant casePayante = (CarreauPayant)caseActuelle;
                if (casePayante.estCarreauAchetable())
                {
                    CarreauAchetable caseAchetable = (CarreauAchetable)casePayante;
                    if (caseAchetable.estPossede())
                    {
                        payerDroitPassage(); // le joueur paie selon l'action.
                        return true;
                    }
                    else
                    {
                        acheterPropriete();
                        return true;
                    }
                }
                else // les deux cases taxes. 
                {
                    //Autres actions à déterminer
                    return false;
                }
            }
            else if (caseActuelle.estCarreauAction())
            {
                CarreauAction caseAction = (CarreauAction)caseActuelle;
                if (caseAction.estCarreauCarte())
                {
                    CarreauCarte caseCarte = (CarreauCarte)caseAction;
                    Carte cartePigee = caseCarte.Piger();
                    // Effectuer l'action de la carte
                  
                }
                else if (caseAction.estCarreauVaEnPrison())
                {
                    
                }

                return true;
 
            }
            //Autres actions à déterminer
            else 
            {
                return false;
            }
        }
        /// <summary>
        /// Cette fonction sert a payer le droit de passage sur une propriété qui n'est pas la sienne
        /// Si le joueur n'a pas assez de tunes pour payer le propriétaire, il fait faillite
        /// </summary>
        public void payerDroitPassage()
        {
            CarreauAchetable carreauActuel = (CarreauAchetable)getCarreauActuel();
            Joueur carreauProprietaire = carreauActuel.Proprietaire;
            long droitPassage = carreauActuel.getPrixPassage();

            // Faire une fct qui valide si le joueur est propriétaire de la case sur laquel il est
            //enlever ce if de la fonction, ya pas d,affaire la
            if (this != carreauProprietaire)
            {
                if (!carreauActuel.estHypothequee)
                {
                    if (PeutPayer(droitPassage))
                    {
                        Payer(droitPassage);
                        carreauProprietaire.Depot(droitPassage);
                    }
                    else
                    {
                        faitFaillite();
                    }
                }
            }
        }

        /// <summary>
        /// Verifie s'il a l'argent pour acheter un terrain, et l'achete automatiquement s'il peut
        /// </summary>
        /// <returns>La propriete a bien ete achetee</returns>
        public bool acheterPropriete()
        {
            CarreauAchetable caseAchetable = (CarreauAchetable)getCarreauActuel();
            if (PeutPayer(caseAchetable.getPrixAchat()))
            {
                Payer(caseAchetable.getPrixAchat()); // le jouer peut decider d'acheter la case.
                caseAchetable.Proprietaire = this;
                Proprietes.Add((CarreauPropriete)caseAchetable);
                return true;
            }
            return false;
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

        private void faitFaillite()
        {
            Console.Write("Tu vas rotter du sang enculé!\n");
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// retourne un carreau selon la position du jouer.
        /// </returns>
        private Carreau getCarreauActuel() 
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
    }
}
