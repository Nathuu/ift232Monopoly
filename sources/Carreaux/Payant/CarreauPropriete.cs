using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
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


        // Droit de passage qui varie en fonction du nombre de maisons ou hotel
        private long[] DroitPassage = new long[6] { 5, 10, 15, 20, 25, 30 }; // prix selon le nombre de maison

        // Ancienne implementation du loyer
        //public int Loyer { get; private set; }
        public int NombreMaison { get; private set; }

        public Couleurs Couleur { get; private set; }


        public CarreauPropriete(int positionCarreau, Couleurs Couleur) : base(positionCarreau)
        {
            PrixAchat = 50;
            //Loyer = 5;
            NombreMaison = 0;
            this.Couleur = Couleur;
        }

        public bool estLibre()
        {
            return (Proprietaire == null);
        }

        // C'est deja implemente dans la classe Joueur, ou c'est beaucoup plus a propos!
        //internal void PayerLoyer()
        //{
        //    Plateau.Instance.JoueurCourant.Argent -= DroitPassage[NombreMaison];
        //}

        public override long getPrixAchat()
        {
            return PrixAchat;
        }


        public override long getPrixPassage()
        {
            if (NombreMaison == 0)
            {
                if (Proprietaire.estSeulProprietaireDeMemeCouleur(Couleur))
                    return 2 * DroitPassage[0];
                else
                    return DroitPassage[0];
            }
            else
            {
                return DroitPassage[NombreMaison];
            }
        }
    }
}
