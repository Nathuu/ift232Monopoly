using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
<<<<<<< HEAD
    /// <summary>
    /// Carreau qui sont des carreau achetables, mais plus spécifiquement des propriété de style couleurs. ils ont une différente facon d'utiliser le prixPassage
    /// </summary>
    class CarreauPropriete:CarreauAchetable
    {
        protected int prixAchat;
        private Brush couleur = Brushes.Gray;
=======
   public class CarreauPropriete
    {
        public int PrixAchat { get; private set; }
        public int Loyer { get; private set; }
        public Joueur Proprietaire { get; set; }
>>>>>>> a362fbf21d7e6ad4351923fc412a821628d28387

        public CarreauPropriete(int postionCarreau) 
        {
            PrixAchat = 50;
            Loyer = 5;
        }

<<<<<<< HEAD
        public override long getPrixAchat()
=======
        public bool estLibre()
>>>>>>> a362fbf21d7e6ad4351923fc412a821628d28387
        {
            return (Proprietaire == null);
        }

<<<<<<< HEAD

        /// <summary>
        /// Retourne le prix de passage de la propriété + le modif de maison ou Hotel
        /// </summary>
        /// <returns></returns>
        public override long getPrixPassage()
        {
            long prixPassage = (prixAchat/10); // est incomplète (voir la carte trello)
            return prixPassage;
=======
        internal void PayerLoyer()
        {
            Plateau.Instance.JoueurCourant.Argent -= Loyer;
>>>>>>> a362fbf21d7e6ad4351923fc412a821628d28387
        }
    }
}
