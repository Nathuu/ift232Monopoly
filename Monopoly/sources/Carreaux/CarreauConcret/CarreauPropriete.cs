using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources.Carreaux
{
    /// <summary>
    /// Carreau qui sont des carreau achetables, mais plus spécifiquement des propriété de style couleurs. ils ont une différente facon d'utiliser le prixPassage
    /// </summary>
    public class CarreauPropriete : CarreauAchetable
    {
        public enum Couleurs
        {
            Brun, BleuPale, Rose, Orange, Rouge, Jaune, Vert, BleuFonce
        };

        // Ancienne implementation du loyer
        //public int Loyer { get; private set; }
        public int NombreMaisons { get; private set; }

        public Couleurs Couleur { get; private set; }

        public CarreauPropriete(int positionCarreau, Couleurs Couleur, long prixAchat, long[] droitPassage) : base(positionCarreau)
        {
            this.Couleur = Couleur;
            this.PrixAchat = prixAchat;
            this.DroitPassage = droitPassage;
            this.NombreMaisons = 0; 
        }

        public bool estLibre()
        {
            return (Proprietaire == null);
        }

        public override long getPrixPassage()
        {
            if (NombreMaisons == 0)
            {
                if (Proprietaire.estSeulProprietaireDeMemeCouleur(Couleur))
                    return 2 * DroitPassage[0];
                else
                    return DroitPassage[0];
            }
            else
            {
                return DroitPassage[NombreMaisons];
            }
        }
    }
}
