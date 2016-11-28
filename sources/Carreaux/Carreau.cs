using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
   public abstract class Carreau 
    {
        public Point position { get; private set; }
        public int positionCarreau { get; private set; }   
              
        public Carreau(int positionCarreau)
        {
            this.positionCarreau = positionCarreau;
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

        /// <summary>
        /// Appelé par achat pour savoir si la propriété peut être acheté
        /// </summary>
        /// <returns>< C'est une propriété</returns>
        public bool estCarreauPayant()
        {
            return this is CarreauPayant; //Peut-etre faire dautre chose ici 
        }

        /// <summary>
        /// Appelé par actionSurCase pour savoir si la case contient une action
        /// </summary>
        /// <returns>C'est une action</returns>
        public bool estCarreauAction()
        {
            return this is CarreauAction;
        }
    }
}