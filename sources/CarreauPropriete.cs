using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
    class CarreauPropriete:Carreau
    {
        private int prixAchat;
        private Joueur proprietaire;
        private Brush couleur = Brushes.Gray;

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

        public long getPrix()
        {
            return prixAchat;
        }

        public bool estLibre()
        {
            return (proprietaire == null);
        }

    }
}
