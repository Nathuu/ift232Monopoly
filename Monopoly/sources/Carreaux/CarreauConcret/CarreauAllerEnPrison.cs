using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux.Action;

namespace WpfApplication1.sources.Carreaux
{
    public class CarreauAllerEnPrison:Carreau
    {
        public CarreauAllerEnPrison(int positionCarreau) : base(positionCarreau)
        {
            actions.Add(new AllezEnPrison());

        }
    }
}
