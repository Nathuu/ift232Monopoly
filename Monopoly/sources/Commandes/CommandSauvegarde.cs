using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Commandes
{
    public class CommandSauvegarde : Command
    {
        public override void execute()
        {
            Plateau.Instance.sauvegarderPartie();
        }
    }
}
