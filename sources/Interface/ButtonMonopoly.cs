using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApplication1.sources;
using WpfApplication1.sources.Commandes;

namespace WpfApplication1
{
    class ButtonMonopoly : Button, Executeur
    {
        private Command commande;
        public void storeCommand(Command cmd)
        {
            commande = cmd;
        }
        public void execute()
        {
            commande.execute();
        }

    }
}
