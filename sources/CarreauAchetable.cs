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
    abstract class CarreauAchetable:CarreauPayant
    {
        protected Joueur proprietaire;

        public CarreauAchetable(Plateau plateau, int indice):base(plateau, indice) { }

        public abstract long getPrixAchat();

        public Joueur getProprietaire()
        {
            return proprietaire;
        }

        public void setProprietaire(Joueur proprietaire)
        {
            this.proprietaire = proprietaire;
        }

        public bool estPossede()
        {
            return (proprietaire == null);
        }

        public bool estPropriete()
        {
            foreach (int j in carreauxBruns)
            {
                if (j == indice) return true;
            }
            foreach (int j in carreauxRouges)
            {
                if (j == indice) return true;
            }
            foreach (int j in carreauxVerts)
            {
                if (j == indice) return true;
            }
            foreach (int j in carreauxBleus)
            {
                if (j == indice) return true;
            }
            return false;
        }
    }
}
