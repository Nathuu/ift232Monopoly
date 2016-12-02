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

        public CarteDeplacement(String desc, List<Carreau> destinationsPossibles, bool passerGo) : base(desc)
        {
            this.DestinationsPossibles = destinationsPossibles;
            this.PasserGo = passerGo;
        }

        private int CalculerDestination()
        {
            foreach (Carreau dest in DestinationsPossibles)
            {
                if (Plateau.Instance.JoueurCourant.PositionCarreau < dest.positionCarreau)
                    return dest.positionCarreau;
            }
            //Retourner la position Carreau minimale

            //int min = Plateau.Instance.getNbCarreauxMax();
            //foreach (Carreau dest in DestinationsPossibles)
            //{
            //    if (min > dest.positionCarreau)
            //        min = dest.positionCarreau;
            //}
            return DestinationsPossibles.Min<Carreau>().positionCarreau;
        }

        public override void Executer()
        {
            int deplacement = CalculerDestination() - Plateau.Instance.JoueurCourant.PositionCarreau;
            if (deplacement < 0)
                deplacement += 40;
            Plateau.Instance.JoueurCourant.PeutPasserGo = PasserGo;
            Plateau.Instance.JoueurCourant.Avancer(deplacement);
            Plateau.Instance.JoueurCourant.PeutPasserGo = true;
        }
    }
}
