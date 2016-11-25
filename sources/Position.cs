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
        public int rangee { get; set; }
        public int colonne { get; set; }
        public Position(int rangee, int colonne)
        {
            this.colonne = colonne;
            this.rangee = rangee;
        }



    }
}
