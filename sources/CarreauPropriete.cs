using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
    /// <summary>
    /// Carreau qui sont des carreau achetables, mais plus spécifiquement des propriété de style couleurs. ils ont une différente facon d'utiliser le prixPassage
    /// </summary>
    public class CarreauPropriete
    {
        public int PrixAchat { get; private set; }
        public int Loyer { get; private set; }
        public Joueur Proprietaire { get; set; }

        public CarreauPropriete(int postionCarreau)
        {
            PrixAchat = 50;
            Loyer = 5;
        }

        public bool estLibre()
        {
            return (Proprietaire == null);
        }

        internal void PayerLoyer()
        {
            Plateau.Instance.JoueurCourant.Argent -= Loyer;
        }
    }
}
