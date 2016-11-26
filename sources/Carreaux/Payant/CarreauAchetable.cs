using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    /// <summary>
    /// Carreau qui sont des carreauPayant mais qui peuvent être possédés par des joueurs (terrain, chemin de fer, aqueduc)
    /// </summary>
    public abstract class CarreauAchetable : CarreauPayant
    {
        public int PrixAchat { get; protected set; }

        public Joueur Proprietaire { get; set; }


        public CarreauAchetable(int position) : base(position) { }

        public abstract long getPrixAchat();

        public bool estPossede()
        {
            return (Proprietaire == null);
        }
    }
}
