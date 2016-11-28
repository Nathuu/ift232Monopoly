using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public abstract class CarreauCarte:CarreauAction
    {

        public CarreauCarte(int position): base(position)
        {

        }

        public abstract Carte Piger();
        
            
        

    }
}
