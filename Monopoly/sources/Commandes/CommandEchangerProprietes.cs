using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApplication1.sources.Commandes
{
    public class CommandEchangerProprietes : Command
    {
        public override void execute()
        {
            // Si le joueur n'a pas de propriété, il ne peut pas échanger
            if (Plateau.Instance.JoueurCourant.Proprietes.Count != 0)
            {
                string promptValue = Prompt.ShowDialog("Lesquelles de vos propriétés souhaitez-vous céder? (S'il y en a plusieurs, séparez-les par une virgule)", "Lesquelles de vos propriétés souhaitez-vous obtenir? (S'il y en a plusieurs, séparez-les par une virgule)", "Échange de propriétés");
                MessageBox.Show(promptValue);
            }
            else
            {
                MessageBox.Show("Vous n'avez aucune propriété à échanger ("+ Plateau.Instance.JoueurCourant.Nom + ")");
            }
        }
    }
}
