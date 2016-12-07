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
        private static List<CarreauAchetable> tousCarreauxAchetables = new List<CarreauAchetable>();

        public long PrixAchat { get; protected set; }
        public Joueur Proprietaire { get; set; }
        public bool EstHypotheque { get; set; }
        // Droit de passage qui varie en fonction du nombre de maisons ou hotel
        protected long[] DroitPassage;

        public CarreauAchetable(int position, long prixAchat) : base(position)
        {
            this.PrixAchat = prixAchat;
            actions.Add(new AchatCarreauAchetable());
            tousCarreauxAchetables.Add(this);
        }
        public bool estPossede()
        {
            return (Proprietaire != null);
        }

        public override string ToString()
        {
            return positionCarreau.ToString();
        }
        public static List<CarreauAchetable> getTousLesCarreauxAchetables()
        {
            return tousCarreauxAchetables;
        }
        public static List<CarreauAchetable> getTousLesCarreauxAchetablesPossedes()
        {
            List<CarreauAchetable> achetes = new List<CarreauAchetable>();
            foreach (CarreauAchetable c in getTousLesCarreauxAchetables())
            {
                if (c.estPossede()) achetes.Add(c);
            }
            return achetes;
        }
    }
}
