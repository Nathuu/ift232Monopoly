using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux.Action;

namespace WpfApplication1.sources.Carreaux
{
    public class CarreauCarte : Carreau
    {
        public PaquetDeCarte PaquetCarte { get; set;}

        public CarreauCarte(int position, PaquetDeCarte paquetCarte):base(position)
        {
            this.PaquetCarte = paquetCarte;
            actions.Add(new PigerCarte());
        }
    }
}
