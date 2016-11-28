using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApplication1.TestsCarreaux
{
    class Carreau
    {
        protected Plateau plateau;
        protected Canvas canvas;
        private Brush couleur = Brushes.White;
        private int hauteur;
        private int largeur;
        private int indice;

        Carreau(Plateau plateau, int indice)
        {
            this.plateau = plateau;
            this.indice = indice;
        }

        Carreau(Plateau plateau, int indice, Brush couleur)
        {
            this.plateau = plateau;
            this.indice = indice;
            this.couleur = couleur;
        }

        public void dessiner()
        {
            Point position = getPosition();
            // Contour du rectangle
            Rectangle rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Width = largeur,
                Height = hauteur,
                Fill = couleur
            };
            Canvas.SetLeft(rect, position.X);
            Canvas.SetTop(rect, position.Y);
            plateau.getCanvas().Children.Add(rect);
        }

        private Point getPosition()
        {
            if (this.indice >= 0 && this.indice < 10)
                return new Point(indice * largeur + plateau.getDecalage().X, plateau.getDecalage().Y);
            else if (this.indice >= 10 && this.indice < 21)
                return new Point(10 * 60 + plateau.getDecalage().X, (this.indice - 10) * hauteur + plateau.getDecalage().Y);
            else if (this.indice >= 21 && this.indice < 30)
                return new Point((30 - this.indice) * largeur + plateau.getDecalage().X, 10 * 60 + plateau.getDecalage().Y);
            else if (this.indice >= 30 && this.indice < 40)
                return new Point(plateau.getDecalage().X, (40 - this.indice) * hauteur + plateau.getDecalage().Y);
            else
                return new Point();
        }
    }


}
