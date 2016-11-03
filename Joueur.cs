using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Joueur
    {

        private String nom;
        private String imagePiece; // le jpg de la piece  va etre change
        private long argent;
        public long Argent
        {
            get { return argent; }
            set { argent = value; }
        }
        private int position;
        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        //hypotheque?
        //private long hypotheque;

        //ce que le joueur possede
        //   private List<CarreauPropriete> propriete; //a reregarder en équipe



        //TEST POUR GITHUB ALEXANDRE


        public Joueur(String nom, String imagePiece)//une piece construite va toujours avoir la meme argent et meme position de depart
        {
            this.nom = nom;
            this.imagePiece = imagePiece;
            argent = 500;
            position = 0;
        }

        // on implente l'interface des des graphiques? 
        public int LanceDeuxDes()// le jouer lance les dés
        {
            Random random = new Random();
            int des = random.Next(1, 12); // va retourner entre 1 et 12 
            return des;
        }
        public int LanceUnDes()// le jouer lance les dés
        {
            Random random = new Random();
            int des = random.Next(1, 6); // va retourner entre 1 et 12 
            return des;
        }


        //va bouger le joueur sur le plateau
        public void Bouger() { }

        // va appeller LanceDes,Bouger, 
        public void Jouer() { }

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


        public bool EstFaillite()
        {

            if (argent < 0)
                return true;
            return false;
        }



    }

}
