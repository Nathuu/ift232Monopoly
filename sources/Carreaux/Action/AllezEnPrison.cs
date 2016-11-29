using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Carreaux.Action
{
    class AllezEnPrison : ICommande
    {
        public void execute(Carreau carreau)
        {
            AllerEnPrison(carreau);
        }

        // Déplacer le joueur vers la prison
        private void AllerEnPrison(Carreau carreau)
        {
            // .....
        }
    }
}
