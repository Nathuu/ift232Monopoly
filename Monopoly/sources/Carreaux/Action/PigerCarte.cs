using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1.sources.Carreaux.Action
{
    public class PigerCarte : ICommande
    {
        public void execute(Carreau carreau)
        {
            CarreauCarte caseCarte = (CarreauCarte)carreau;
            Carte cartePigee = caseCarte.PaquetCarte.Piger();
            MessageBox.Show("Vous avez pige une carte!\n\n" + cartePigee.Description);
            // Effectuer l'action de la carte
            cartePigee.Executer();
        }
    }
}
