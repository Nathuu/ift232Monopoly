using System;
using System.Collections.Generic;
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
        private String nom;
        private Image image;
        private long argent;
        public Position PositionJoueur { get; set; }
        public bool AFiniSonTour { get; set; }
        public Joueur(String nom, Image image)//une piece construite va toujours avoir la meme argent et meme position de depart
        {
            this.nom = nom;
            this.image = image;
            this.argent = 500;
            this.PositionJoueur = new Position();
            this.Init();
        }

        public void Init()
        {
            this.image.Width = 40;
            this.image.Height = 40;
        }
        
        // Voir la propriete PositionJoueur
        //public Position getPosition()
        //{
        //    return PositionJoueur;
        //}
        //public void setPosition(int nouvPosition)
        //{
        //    PositionJoueur = nouvPosition;
        //    // SW!!! CALCUL QUI FAIT LE LIEN ENTRE L'INDICE D'UNE CASE ET SA POSITION EN PIXELS SUR LE PLATEAU
        //    this.image.Margin = new Thickness(0, 0, 0, 0);
        //}

        // Voir methode Avancer(int)
        //public void SeDeplacer(int deplacement)
        //{
        //    int nouvPosition = (getPosition() + deplacement) % Plateau.Instance.getNbCarreaux();
        //    setPosition(nouvPosition);
        //}

        //ce que le joueur possede
        //   private List<CarreauPropriete> propriete; //a regarder en équipe


        // on implente l'interface des des graphiques? 
        public int LanceDeuxDes()// le joueur lance les dés
        {
            return LanceUnDes() + LanceUnDes();
        }
        public int LanceUnDes()// un dé est lancé
        {
            Random random = new Random();
            int des = random.Next(1, 6); // va retourner entre 1 et 12 
            return des;
        }

        // Début du tour du joueur : va appeller LanceDes,Bouger,...
        public void JouerSonTour() {
            AFiniSonTour = false;
            //while (!AFiniSonTour)
            {
                MessageBox.Show("Joueur " + nom + " va brasser un de.");
                Avancer(LanceUnDes());
            }

        }

        private void Avancer(int nbCases)
        {
            PositionJoueur.Colonne += nbCases;
        }

        /**************************************************************************
        * valeur d'entree : ce que le joueur doit payer
        * valeur Sortie : bool vrai si joueur peut payer
        * regarde si un joueur peut payer
        ************************************************************************/
        public bool PeutPayer(long aPayer)//on regarde si le joueur peut payer tel montant
        {
            if (argent - aPayer < 0)
                return false;
            return true;
        }
        /**************************************************************************
         * valeur d'entree : ce que le joueur doit payer
         * valeur Sortie : nouvelle argent de joueur
         * fait la transaction entre deux joueurs
         ************************************************************************/
        public long Payer(long aPayer)
        {

            argent -= aPayer;
            return argent; // return l'argent  que le jouer a ;
        }
        public long Depot(long deposer)
        {

            argent += deposer;
            return argent; // on retourne le nouveau argent
        }

        //Ne pas tenier compte
        /**************************************************************************
         * valeur d'entree : quelle joueur doit payer this.joueur
         * valeur Sortie : vrai si la transaction a ete effectué
         * fait la transaction entre deux joueurs
         ************************************************************************/
        /*public bool Transaction(Joueur joueur) // this.joueur paie le Joueur joueur //
        {
            return true;
        }   // 
        */
    }
}
