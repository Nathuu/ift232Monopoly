using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.sources.Carreaux.CarreauConcret;

namespace WpfApplication1.sources.Carreaux.Action
{
    class RamasserMontant : ICommande
    {
        public void execute(Carreau carreau)
        {
            MessageBox.Show("Joueur " + Plateau.Instance.JoueurCourant.Nom + " remporte le montant sur la case Parking Gratuit!");
            CarreauParkingGratuit caseParkingGratuit = (CarreauParkingGratuit)carreau;
            long montant = caseParkingGratuit.prendreMontant();
            Plateau.Instance.JoueurCourant.Depot(montant);   
        }
    }
}
