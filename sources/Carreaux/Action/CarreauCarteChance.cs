using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public class CarreauCarteChance : CarreauCarte
    {
        private PaquetDeCarte CartesChance;
        public CarreauCarteChance(int position, ref PaquetDeCarte cartesChance) : base(position)
        {
            this.CartesChance = cartesChance;
        }

        /// <summary>
        /// Fait la rotation du paquet de carte
        /// </summary>
        /// <returns>
        /// la carte piger
        /// </returns>
        public override Carte Piger()
        { 
            return CartesChance.Piger();
        }
    }
}
