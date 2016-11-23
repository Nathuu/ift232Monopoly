using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApplication1.sources
{
    public class Position 
    {
        public int Rangee { get; set; }
        public int Colonne { get; set; }
        public Position(int Rangee, int Colonne)
        {
            this.Colonne = Colonne;
            this.Rangee = Rangee;
        }
    }
}
