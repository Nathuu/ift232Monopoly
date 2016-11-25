using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
    public class Carreau
    {
        public Point position { get; private set; } // coordonnées X,Y du coin supérieur gauche du carreau
        public int postionCarreau{ get; private set;}

        protected  List<int> carreauxBruns = new List<int>() { 1, 3 };
        protected  List<int> carreauxRouges = new List<int>() { 21, 23, 24 };
        protected  List<int> carreauxVerts = new List<int>() { 31, 32, 34 };
        protected  List<int> carreauxBleus = new List<int>() { 37, 39 };

        public Carreau(int postionCarreau)
        {
            this.postionCarreau = postionCarreau;
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