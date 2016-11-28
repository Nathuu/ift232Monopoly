using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1.sources
{
    class CarreauPrison : CarreauAction
    {
        public CarreauPrison(int postionCarreau) :base(postionCarreau)
        {

        }

        //Dessine pu a lecran
        //public override void dessiner()
        //{
        //    // Contour du rectangle
        //    Rectangle rect = new Rectangle
        //    {
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2,
        //        Width = this.getLargeur(),
        //        Height = this.getHauteur()
        //    };
        //    Canvas.SetLeft(rect, this.getPosition().X);
        //    Canvas.SetTop(rect, this.getPosition().Y);
        //    plateau.getCanvas().Children.Add(rect);
        //}
    }
}
