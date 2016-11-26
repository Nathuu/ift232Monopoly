using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
   public abstract class Carreau 
    {
        //Transformation de vos vielle variable par des propriete pour permettre un peu plus d'integrer et eviter davoir des fonctions get et set inutile
        //protected Canvas canvas;
        //private int hauteur = 60; //Suppression de hauteur et largeur. Inutile
        //private int largeur = 60;
        //protected int indice; // position dans la liste de la classe Plateau
        //protected Plateau plateau; // plateau auquel appartient le carreau
        public Point position { get; private set; }
        public int positionCarreau { get; private set; }

        protected List<int>  carreauxBruns =  new List<int>() {1,3}; //Remplacement des tableaux par des listes. Plus faciles d'utilisation par apres
        protected List<int> carreauxRouges = new List<int>() { 21, 23, 24 };
        protected List<int> carreauxVerts = new List<int>() { 31,32,34 };
        protected List<int> carreauxBleus = new List<int>() { 37,39 };

        //public Carreau(Plateau plateau, int indice)
        public Carreau(int positionCarreau)
        {
            this.positionCarreau = positionCarreau;
        }
        //if      (this.indice >= 0 && this.indice < 10) this.position = new Point(this.indice * getLargeur() + plateau.getDecalage().X, plateau.getDecalage().Y);
        //else if (this.indice >= 10 && this.indice < 21) this.position = new Point(10*60 + plateau.getDecalage().X, (this.indice - 10) * getHauteur() + plateau.getDecalage().Y);
        //else if (this.indice >= 21 && this.indice < 30) this.position = new Point((30 - this.indice) * getLargeur() + plateau.getDecalage().X, 10*60 + plateau.getDecalage().Y);
        //else if (this.indice >= 30 && this.indice < 40) this.position = new Point(plateau.getDecalage().X, (40 - this.indice) * getHauteur() + plateau.getDecalage().Y);
        //else { }
        //public abstract void dessiner(); //Utilisation d'une image comme tableaux de jeux dessiner est inutile dans ce cas.

        public static Position conversionInt2Position(int positionCarreau)
        {
            Position position;
            if (positionCarreau >= 0 && positionCarreau < 10) position = new Position(0, positionCarreau);
            else if (positionCarreau >= 10 && positionCarreau < 21) position = new Position(positionCarreau - 10, 10);
            else if (positionCarreau >= 21 && positionCarreau < 30) position = new Position(10, 30 - positionCarreau);
            else if (positionCarreau >= 30 && positionCarreau < 40) position = new Position(40 - positionCarreau, 0);
            else position = null;

            return position;
        }

        /**************************************************************************
          * valeur d'entree : 
          * valeur Sortie : Boolean: True: C'est une propriété
                                     False: Ce n'est pas une propriété
          * Vérifie si c'est une propriété

          **Appelé par achat pour savoir si la propriété peut être acheté
         ************************************************************************/
        public bool estCarreauPayant()
        {
            return this is CarreauPayant; //Peut-etre faire dautre chose ici 
        }

        /**************************************************************************
          * valeur d'entree : 
          * valeur Sortie : Boolean: True: C'est une action
                                     False: Ce n'est pas une action
          * Vérifie si c'est une action

          **Appelé par actionSurCase pour savoir si la case contient une action
         ************************************************************************/
        public bool estCarreauAction()
        {
            return this is CarreauAction;
        }

        public bool estPropriete(int positionCarreau)
        {
            bool estProp = false;
            estProp = carreauxBruns.Exists(x => x == positionCarreau);
            if (!estProp) estProp = carreauxRouges.Exists(x => x == positionCarreau);
            if (!estProp) estProp = carreauxVerts.Exists(x => x == positionCarreau);
            if (!estProp) estProp = carreauxBleus.Exists(x => x == positionCarreau);
            return estProp;
        }
    }
}