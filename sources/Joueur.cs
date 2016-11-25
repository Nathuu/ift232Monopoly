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
        public String nom { get; set; }
        private Image image;
        public Image Image
        {
            get { return image; }
        }
        public long argent { get; set; }
        public int positionCarreau; // Un int de 0 à nbCases-1
        public Position position { get; set; } // un objet de type Position
        public bool AFiniSonTour { get; set; }


        private List<CarreauPropriete> proprietes;


        public Joueur(String nom, Image image)//une piece construite va toujours avoir la meme argent et meme position de depart
        {
            this.nom = nom;
            this.image = image;
            this.argent = 500;
            this.positionCarreau = 0;
            this.position = new Position(0, 0);
            this.Init();
        }

        public void Init()
        {
            this.image.Width = 20;
            this.image.Height = 20;
        }
        public Image getImage() { return image; }





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

        internal void Sauvegarder(StreamWriter fichierSauvegarde)
        {
            fichierSauvegarde.WriteLine(nom);
            fichierSauvegarde.WriteLine(position.colonne);
            fichierSauvegarde.WriteLine(position.rangee);
            fichierSauvegarde.WriteLine(argent);
            fichierSauvegarde.WriteLine(positionCarreau);
        }

        // Début du tour du joueur : va appeller LanceDes,Bouger,...
        public void JouerSonTour()
        {
            AFiniSonTour = false;
            //while (!AFiniSonTour)
            {
                MessageBox.Show("Joueur " + nom + " va brasser un de.");
                int coupDe = LanceDeuxDes();
                MessageBox.Show("Joueur " + nom + " avance de " + coupDe + " cases");
                Avancer(coupDe);
            }

        }


        public void Avancer(int nbCases)
        {
            this.positionCarreau = (this.positionCarreau + nbCases) % Plateau.Instance.getNbCarreaux();
            this.position = Carreau.conversionInt2Position(this.positionCarreau);
            Grid.SetRow(this.image, this.position.rangee);
            Grid.SetColumn(this.image, this.position.colonne);
            action();
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

        /**************************************************************************
         * valeur d'entree : 
         * valeur Sortie : Boolean : True: action effectuée
                                     False: Action à déterminer
         * détermine l'action à effectuer selon la case et la situation du joueur
         ************************************************************************/
        public bool action()
        {
            /*Carreau caseActuelle = Plateau.Instance.getArrayCarreaux()[positionCarreau];
            MessageBox.Show(caseActuelle.getLargeur() + "");
            if (caseActuelle.estPropriete())
            {
                CarreauPropriete proprieteActuelle = (CarreauPropriete)caseActuelle;
                if (proprieteActuelle.estLibre())
                {
                    if (PeutPayer(proprieteActuelle.getPrix()))
                    {
                        Payer(proprieteActuelle.getPrix());
                        proprietes.Add(proprieteActuelle);
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
                    //Payer qqun (loyer)
                    return false;
                }

            }*/
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
