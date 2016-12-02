using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux.CarreauConcret;

namespace WpfApplication1.sources.Carreaux.Action
{
    class DroitPassage : ICommande
    {
        public DroitPassage()
        {
        }

        public void execute(Carreau carreau)
        {            
            if (carreau is CarreauAchetable)
            {
                CarreauAchetable carreauActuel= (CarreauAchetable)carreau;
                if (carreauActuel.estPossede())
                    payerDroitPassage(carreauActuel);
            }
            else if (carreau is CarreauTaxe)
            {
                payerTaxe((CarreauPayant)carreau);
            }
             
        }     

        /// <summary>
        /// Cette fonction sert a payer le droit de passage sur une propriété qui n'est pas la sienne
        /// Si le joueur n'a pas assez de tunes pour payer le propriétaire, il fait faillite
        /// </summary>
        public void payerDroitPassage(CarreauAchetable carreau)
        {
            Joueur carreauProprietaire = carreau.Proprietaire;
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            long droitPassage = carreau.getPrixPassage();

            // Faire une fct qui valide si le joueur est propriétaire de la case sur laquel il est
            //enlever ce if de la fonction, ya pas d,affaire la
            if (joueurCourant != carreauProprietaire)
            {
                if (! carreauProprietaire.Hypotheques.Contains(carreau))
                {
                    if (joueurCourant.PeutPayer(droitPassage))
                    {
                        joueurCourant.Payer(droitPassage);
                        carreauProprietaire.Depot(droitPassage);
                    }
                    else
                    {
                        if (Plateau.Instance.JoueurCourant.HypothequerSuivant())
                        {
                            payerDroitPassage(carreau);
                        }
                        else
                        {
                            Plateau.Instance.JoueurCourant.FaitFaillite();
                        }
                        
                    }
                }
            }
        }

        private void payerTaxe(CarreauPayant carreau)
        {
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            long droitPassage = carreau.getPrixPassage();
            if (joueurCourant.PeutPayer(droitPassage))
            {
                joueurCourant.Payer(droitPassage);               

                (Plateau.Instance.dictionnaireCarreaux["INDEX_PARKING_GRATUIT"] as CarreauParkingGratuit).ajoutMontant(droitPassage);
            }
            else
            {
                joueurCourant.FaitFaillite();
            }
        }
    }
}
