using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
   public abstract class Carreau
    {
        protected Plateau plateau; // plateau auquel appartient le carreau
        protected Canvas canvas;
        private int hauteur = 60;
        private int largeur = 60;
        protected int indice; // position dans le tableau arrayCarreaux de la classe Plateau
        private Point position; // coordonnées X,Y du coin supérieur gauche du carreau

        protected static int[] carreauxBruns = {1,3};
        protected static int[] carreauxRouges = { 21, 23, 24 };
        protected static int[] carreauxVerts = { 31,32,34 };
        protected static int[] carreauxBleus = { 37,39 };

        public Carreau(Plateau plateau, int indice)
        {
            this.plateau = plateau;
            this.canvas = plateau.getCanvas();
            this.indice = indice;
            if      (this.indice >= 0 && this.indice < 10) this.position = new Point(this.indice * getLargeur() + plateau.getDecalage().X, plateau.getDecalage().Y);
            else if (this.indice >= 10 && this.indice < 21) this.position = new Point(10*60 + plateau.getDecalage().X, (this.indice - 10) * getHauteur() + plateau.getDecalage().Y);
            else if (this.indice >= 21 && this.indice < 30) this.position = new Point((30 - this.indice) * getLargeur() + plateau.getDecalage().X, 10*60 + plateau.getDecalage().Y);
            else if (this.indice >= 30 && this.indice < 40) this.position = new Point(plateau.getDecalage().X, (40 - this.indice) * getHauteur() + plateau.getDecalage().Y);
            else { }
        }
        public abstract void dessiner();


        public int getLargeur()
        {
            return this.largeur;
        }
        public int getHauteur()
        {
            return this.hauteur;
        }
        public Point getPosition()
        {
            return this.position;
        }
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD


        public bool estCarreauPayant()
        {
            return this is CarreauPayant;
=======
        public bool estPropriete(int positionCarreau)
        {
            bool estProp = false;
            estProp = carreauxBruns.Exists(x => x == positionCarreau);
            if (!estProp) estProp = carreauxRouges.Exists(x => x == positionCarreau);
            if (!estProp) estProp = carreauxVerts.Exists(x => x == positionCarreau);
            if (!estProp) estProp = carreauxBleus.Exists(x => x == positionCarreau);
            return estProp;
>>>>>>> a362fbf21d7e6ad4351923fc412a821628d28387
=======
        public bool estPropriete()
        {
=======
        public bool estPropriete()
        {
>>>>>>> parent of cc90394... Voir trello
            foreach (int j in carreauxBruns)
            {
                if (j == indice) return true;
            }
            foreach (int j in carreauxRouges)
            {
                if (j == indice) return true;
            }
            foreach (int j in carreauxVerts)
            {
                if (j == indice) return true;
            }
            foreach (int j in carreauxBleus)
            {
                if (j == indice) return true;
            }
            return false;
<<<<<<< HEAD
>>>>>>> parent of cc90394... Voir trello
=======
>>>>>>> parent of cc90394... Voir trello
        }
    }
}