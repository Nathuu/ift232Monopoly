using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Cartes
{
    public class CarteDeplacement : Carte
    {
        public List<Carreau> DestinationsPossibles { get; protected set; } // Une seule carte requiert un deplacement numerique.
        public bool PasserGo { get; protected set; }

        public CarteDeplacement(String desc, List<Carreau> destinationsPossibles) : base(desc)
        {
            this.DestinationsPossibles = destinationsPossibles;
        }

        private int CalculerDestination()
        {
            foreach (Carreau dest in DestinationsPossibles)
            {
                if (Plateau.Instance.JoueurCourant.PositionCarreau < dest.positionCarreau)
                    return dest.positionCarreau;
            }
            //Retourner la position Carreau minimale

            int min = Plateau.Instance.getNbCarreauxMax();
            foreach (Carreau dest in DestinationsPossibles)
            {
                if (min > dest.positionCarreau)
                    min = dest.positionCarreau;
            }
            return min;
        }

        public override void Executer()
        {
            Plateau.Instance.JoueurCourant.Avancer(CalculerDestination() - Plateau.Instance.JoueurCourant.PositionCarreau);
        }
    }
}
