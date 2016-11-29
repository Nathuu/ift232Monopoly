using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Carreaux
{
    /// <summary>
    /// Carreau qui ont la fonction de faire payer une joueur lors de son arrivé sur la case
    /// </summary>
    public abstract class CarreauPayant:Carreau
    {
        public CarreauPayant(int position) :base(position) { }

        public abstract long getPrixPassage();
    }
}
