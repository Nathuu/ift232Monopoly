using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    abstract class Carreau
    {
        protected Plateau plateau; // plateau auquel appartient le carreau
        protected Canvas canvas;
        private int hauteur = 60;
        private int largeur = 60;
        protected int indice; // position dans le tableau arrayCarreaux de la classe Plateau
        private Point position; // coordonnées X,Y du coin supérieur gauche du carreau

        protected static int[] carreauxBruns = {1,3};
        protected static int[] carreauxRouges = { 21, 23, 24 };
        protected static int[] carreauxVerts = { 31,32,34 };
        protected static int[] carreauxBleus = { 37,39 };

        public Carreau(Plateau plateau, int indice)
        {
            this.plateau = plateau;
            this.canvas = plateau.getCanvas();
            this.indice = indice;
            if      (this.indice >= 0 && this.indice < 10) this.position = new Point(this.indice * getLargeur() + plateau.getDecalage().X, plateau.getDecalage().Y);
            else if (this.indice >= 10 && this.indice < 21) this.position = new Point(10*60 + plateau.getDecalage().X, (this.indice - 10) * getHauteur() + plateau.getDecalage().Y);
            else if (this.indice >= 21 && this.indice < 30) this.position = new Point((30 - this.indice) * getLargeur() + plateau.getDecalage().X, 10*60 + plateau.getDecalage().Y);
            else if (this.indice >= 30 && this.indice < 40) this.position = new Point(plateau.getDecalage().X, (40 - this.indice) * getHauteur() + plateau.getDecalage().Y);
            else { }
        }
        public abstract void dessiner();
        /*{
            // Contour du rectangle
            Rectangle rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Width = this.getLargeur(),
                Height = this.getHauteur(),
                Fill = Brushes.Red
            };
            Canvas.SetLeft(rect, this.getPosition().X);
            Canvas.SetTop(rect, this.getPosition().Y);
            this.canvas.Children.Add(rect);
        }*/

        public int getLargeur()
        {
            return this.largeur;
        }
        public int getHauteur()
        {
            return this.hauteur;
        }
        public Point getPosition()
        {
            return this.position;
        }
    }
}