using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Cartes
{
    class CarteMontant : Carte
    {
        public int Montant { get; protected set; }

        public CarteMontant(String desc, int montant) : base(desc)
        {
            Montant = montant;
        }

        public override void Executer()
        {
            CalculerMontant();
            if (Montant > 0)
                Plateau.Instance.JoueurCourant.Depot(Montant);
            else
                Plateau.Instance.JoueurCourant.Payer(Montant);
        }

        private void CalculerMontant()
        {
            // Certaines cartes ont un montant variable, ex: selon le nb de maisons
        }
    }

}
