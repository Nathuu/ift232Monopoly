using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux.Action;

namespace WpfApplication1.sources.Carreaux
{
    /// <summary>
    /// Carreau qui sont des carreauPayant mais qui peuvent être possédés par des joueurs (terrain, chemin de fer, aqueduc)
    /// </summary>  
    public abstract class CarreauAchetable : CarreauPayant
    {
        public int PrixAchat { get; protected set; }
        public bool estHypothequee { get; set; }
        public Joueur Proprietaire { get; set; }
        public CarreauAchetable(int position) : base(position)
        {
            estHypothequee = false;
            actions.Add(new AchatPropriete());
            actions.Add(new DroitPassage());
        }

        public abstract long getPrixAchat();

        public bool estPossede()
        {
            return (Proprietaire != null);
        }
    }
}
