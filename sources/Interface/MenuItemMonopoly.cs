using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApplication1.sources.Commandes;
using System.Windows;

namespace WpfApplication1.sources.Interface
{
    class MenuItemMonopoly : MenuItem, Executeur
    {
        private Command commande;

        public void storeCommand(Command cmd)
        {
            commande = cmd;
        }

        public void execute(object sender, RoutedEventArgs e)
        {
            commande.execute();
        }
    }
}
