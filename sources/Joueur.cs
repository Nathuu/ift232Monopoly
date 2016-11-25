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
        public Image Image { get; set; }
        public long Argent { get; set; }
        public Position Position { get; set; } // un objet de type Position
        public string Nom { get; set; }
        public int PositionCarreau { get; set; }
        public List<CarreauPropriete> Proprietes { get; private set; }

        public Joueur(String nom, Image image)//une piece construite va toujours avoir la meme argent et meme position de depart
        {
            this.Nom = nom;
            this.Image = image;
            this.Argent = 500;
            this.PositionCarreau = 0;
            this.Position = new Position(1, 1);
            this.Image.Width = 20;
            this.Image.Height = 20;
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
            this.PositionCarreau = (this.PositionCarreau + nbCases) % Plateau.Instance.getNbCarreaux();
            this.Position = Carreau.conversionInt2Position(this.PositionCarreau);
            Grid.SetRow(this.Image, this.Position.rangee + 1);
            Grid.SetColumn(this.Image, this.Position.colonne + 1);
            action();
        }

        /**************************************************************************
        * valeur d'entree : ce que le joueur doit payer
        * valeur Sortie : bool vrai si joueur peut payer
        * regarde si un joueur peut payer
        ************************************************************************/
        public bool PeutPayer(long aPayer)//on regarde si le joueur peut payer tel montant
        {
            if (Argent - aPayer < 0)
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

            Argent -= aPayer;
            return Argent; // return l'argent  que le jouer a ;
        }
        public long Depot(long deposer)
        {

            Argent += deposer;
            return Argent; // on retourne le nouveau argent
        }

        /**************************************************************************
         * valeur d'entree : 
         * valeur Sortie : Boolean : True: action effectuée
                                     False: Action à déterminer
         * détermine l'action à effectuer selon la case et la situation du joueur
         ************************************************************************/
        public bool action()
        {
            Carreau carreauActuelle = new Carreau(PositionCarreau);
            if (carreauActuelle.estPropriete(PositionCarreau))
            {
                CarreauPropriete proprieteActuelle = new CarreauPropriete(PositionCarreau);
                if (proprieteActuelle.estLibre())
                {
                    if (PeutPayer(proprieteActuelle.PrixAchat))
                    {
                        Payer(proprieteActuelle.PrixAchat);
                        Proprietes.Add(proprieteActuelle);
                        return true;
                    }
                    else
                    {
                        //NE se passe rien (pour l'instant), manque hypotheque... autre moyen pour payer
                        return false;
                    }
                }
                else
                {
                    proprieteActuelle.PayerLoyer();
                    //Payer qqun (loyer)
                    return false;
                }

            }
            //Autres actions à déterminer
            return false;
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
