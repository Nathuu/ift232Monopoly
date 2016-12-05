using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Commandes
{
    public class CommandStatistique : Command
    {
        public String nom { get; set; }
        public override void execute()
        {
            InformationJoueur infoWindow = new InformationJoueur(Plateau.Instance.Joueurs.FirstOrDefault(x => x.Nom == this.nom));
            infoWindow.ShowDialog();
        }

      
    }
}
