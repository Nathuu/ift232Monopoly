using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Cartes
{
    public class CarteDeplacement : Carte
    {
        public List<int> DestinationsPossibles { get; protected set; } // Une seule carte requiert un deplacement numerique.
        public bool PasserGo { get; protected set; }

        public CarteDeplacement(String desc, List<int> destinationsPossibles) : base(desc)
        {
            this.DestinationsPossibles = destinationsPossibles;
        }

        private int CalculerDestination()
        {
            foreach (int dest in DestinationsPossibles)
            {
                if (Plateau.Instance.JoueurCourant.PositionCarreau < dest)
                    return dest;
            }
            return DestinationsPossibles.Min();
        }

        public override void Executer()
        {
            Plateau.Instance.JoueurCourant.Avancer(CalculerDestination() - Plateau.Instance.JoueurCourant.PositionCarreau);
        }
    }
}
