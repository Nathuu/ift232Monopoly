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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for RestaureFichier.xaml
    /// </summary>
    public partial class RestaurationFichier : Window
    {
        public string FileName { get; private set; }
        public RestaurationFichier(List<string> fichierDisponible)
        {
            InitializeComponent();
            cmbFiles.ItemsSource = fichierDisponible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileName = cmbFiles.SelectedValue.ToString();
        }
    }
}
