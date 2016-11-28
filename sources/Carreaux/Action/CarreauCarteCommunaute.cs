using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public  class CarreauCarteCommunaute:CarreauCarte
    {
        private PaquetDeCarte CartesCommunaute;
        public CarreauCarteCommunaute(int position, ref PaquetDeCarte cartesCommunaute): base(position)
        {
            this.CartesCommunaute = cartesCommunaute;
        }
        /// <summary>
        /// Fait la rotation du paquet de carte
        /// </summary>
        /// <returns>
        /// la carte piger
        /// </returns>
        public override Carte Piger()
        {
            return CartesCommunaute.Piger();
        }
    }
}
