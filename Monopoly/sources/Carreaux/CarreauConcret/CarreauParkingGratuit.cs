using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux.Action;

namespace WpfApplication1.sources.Carreaux.CarreauConcret
{
    class CarreauParkingGratuit : Carreau
    {
        private long Montant;
        public CarreauParkingGratuit(int positionCarreau) : base(positionCarreau)
        {
            Montant = 0;
            actions.Add(new RamasserMontant());
        }

        public void ajoutMontant(long montant)
        {
            Montant += montant;
        }

        public long prendreMontant()
        {
            long gain = Montant;
            Montant = 0;
            return gain;
        }
    }
}
