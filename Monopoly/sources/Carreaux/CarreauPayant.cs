using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux.Action;

namespace WpfApplication1.sources.Carreaux
{
    /// <summary>
    /// Carreau qui ont la fonction de faire payer une joueur lors de son arrivé sur la case
    /// </summary>
    public abstract class CarreauPayant:Carreau
    {
        public CarreauPayant(int position) :base(position) {
            actions.Add(new DroitPassage());
        }

        public abstract long getPrixPassage();
    }
}
