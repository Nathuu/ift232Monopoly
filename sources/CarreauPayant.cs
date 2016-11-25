using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    /// <summary>
    /// Carreau qui ont la fonction de faire payer une joueur lors de son arrivé sur la case
    /// </summary>
    abstract class CarreauPayant:Carreau
    {
        public CarreauPayant(Plateau plateau, int indice):base(plateau, indice) { }

        public abstract long getPrixPassage();

        public bool estCarreauAchetable()
        {
            return this is CarreauAchetable;
        }
    }
}
