using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
    class CarreauPropriete:Carreau
    {
        private int prixAchat;
        private Joueur proprietaire;
        private Brush couleur = Brushes.Gray;
>>>>>>> parent of cc90394... Voir trello
=======
    class CarreauPropriete:Carreau
    {
        private int prixAchat;
        private Joueur proprietaire;
        private Brush couleur = Brushes.Gray;
>>>>>>> parent of cc90394... Voir trello
=======
    class CarreauPropriete:Carreau
    {
        private int prixAchat;
        private Joueur proprietaire;
        private Brush couleur = Brushes.Gray;
>>>>>>> parent of cc90394... Voir trello

        public CarreauPropriete(Plateau plateau, int indice):base(plateau, indice)
        {
            if (carreauxBruns.Contains(this.indice)) this.couleur = Brushes.Brown;
            else if (carreauxRouges.Contains(this.indice)) this.couleur = Brushes.Red;
            else if (carreauxVerts.Contains(this.indice)) this.couleur = Brushes.Green;
            else if (carreauxBleus.Contains(this.indice)) this.couleur = Brushes.Blue;
        }
        public override void dessiner()
        {
            // Contour du rectangle
            Rectangle rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Width = this.getLargeur(),
                Height = this.getHauteur(),
                Fill = couleur
            };
            Canvas.SetLeft(rect, this.getPosition().X);
            Canvas.SetTop(rect, this.getPosition().Y);
            plateau.getCanvas().Children.Add(rect);
        }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        public override long getPrixAchat()
=======
        public bool estLibre()
>>>>>>> a362fbf21d7e6ad4351923fc412a821628d28387
=======
        public long getPrix()
>>>>>>> parent of cc90394... Voir trello
=======
        public long getPrix()
>>>>>>> parent of cc90394... Voir trello
=======
        public long getPrix()
>>>>>>> parent of cc90394... Voir trello
        {
            return prixAchat;
        }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
        public bool estLibre()
        {
            return (proprietaire == null);
>>>>>>> parent of cc90394... Voir trello
=======
        public bool estLibre()
        {
            return (proprietaire == null);
>>>>>>> parent of cc90394... Voir trello
=======
        public bool estLibre()
        {
            return (proprietaire == null);
>>>>>>> parent of cc90394... Voir trello
        }


    }
}
