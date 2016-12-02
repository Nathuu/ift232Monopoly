using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Carreaux
{
    class CarreauTaxe : CarreauPayant
    {
        long DroitPassage { get; set; }
        public CarreauTaxe(int positionCarreau, long droitPassage) : base(positionCarreau)
        {
            this.DroitPassage = droitPassage;        
        }

        public override long getPrixPassage()
        {
            return DroitPassage;
        }
    }
}
