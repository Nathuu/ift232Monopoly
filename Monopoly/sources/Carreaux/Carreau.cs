using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApplication1.sources.Carreaux.Action;

namespace WpfApplication1.sources
{
   public abstract class Carreau
    {
        public List<ICommande> actions;
        public int positionCarreau { get; private set; }   
              
        public Carreau(int positionCarreau)
        {
            actions = new List<ICommande>();
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

        public void execute()
        {
            foreach(ICommande comm in actions)
            {
                comm.execute(this);
            }
        }
    }
}