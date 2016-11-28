using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public abstract class CarreauAction : Carreau
    {
        public CarreauAction(int position) : base(position) { }

        public bool estCarreauCarte()
        {
            return this is CarreauCarte;
        }

        public bool estCarreauVaEnPrison()
        {
            return this is CarreauVaPrison;
        }

        public bool estCarreauPrison()
        {
            return this is CarreauPrison;
        }
    }
}
