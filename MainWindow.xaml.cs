using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Plateau plateauPrincipal;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        public MainWindow()
        {
            InitializeComponent();
            plateauPrincipal = Plateau.getInstance();
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            /*BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("logo.png");
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;*/
        }
    }
}
