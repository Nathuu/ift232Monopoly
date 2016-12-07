using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1.sources
{
    public class CommandLancerDes : Command
    {
        public override void execute()
        {
            //MessageBox.Show("" + Plateau.Instance.JoueurRestant + "", "Nb Joueur restant", MessageBoxButton.OK, MessageBoxImage.Information);
            if (Plateau.Instance.JoueurRestant == 1)
            {
                //Ceci est la fin du jeu
                MessageBox.Show("Joueur " + Plateau.Instance.JoueurCourant.Nom + " GAAAAAAAGNNNNEEEEEEEEEEEEEE!!!!!!!!!!!!!!!!!!!!!!!!!!! ", "FIN DE LA PARTIE :)", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Environment.Exit(0);
            }
            GestionnaireJeu.JouerTour();
        }
    }
}
