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
                CarreauAchetable carreauActuel = (CarreauAchetable)carreau;
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
            Joueur joueurProprietaire = carreau.Proprietaire;
            long droitPassage = carreau.getPrixPassage();

            // Faire une fct qui valide si le joueur est propriétaire de la case sur laquel il est
            //enlever ce if de la fonction, ya pas d,affaire la
            if (Plateau.Instance.JoueurCourant != joueurProprietaire)
            {
                if (!joueurProprietaire.Proprietes.FirstOrDefault(x => x == carreau).EstHypotheque)
                {
                    if (Plateau.Instance.JoueurCourant.PeutPayer(droitPassage))
                    {
                        Plateau.Instance.JoueurCourant.Payer(droitPassage);
                        joueurProprietaire.Depot(droitPassage);
                    }
                    else
                    {
                        if(AUneProprieteAHypothequer())
                        {
                            payerDroitPassage(carreau);
                        }
                        else Plateau.Instance.JoueurCourant.FaitFaillite();
                    }
                }
            }
        }

        private bool AUneProprieteAHypothequer()
        {
            //A partir de ce moment, il va payer son droit de passage avec directement la premiere propriété qui n'est pas hypothequer parmis propriété, service et trains.
            //On pourrait ici demander a l'utilisateur laquel des propriété qui veut hypothequer. Je conseil d'utiliser un nouveau window (affiche propriété, service et trains + double-click)
            CarreauAchetable proprieteNonHypotheque = Plateau.Instance.JoueurCourant.Proprietes.Where(x => !x.EstHypotheque).First();
            CarreauAchetable serviceNonHypotheque = Plateau.Instance.JoueurCourant.Services.Where(x => !x.EstHypotheque).First();
            CarreauAchetable trainsNonHypotheque = Plateau.Instance.JoueurCourant.Proprietes.Where(x => !x.EstHypotheque).First();
            
            //int dep = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Quel propriete voulez-vous hypotheque?\n" + liste + "\n" + " $        Votre argent: " + Plateau.Instance.JoueurCourant.Argent + " $ ", "Hypotheque?"));
            //CarreauAchetable prop = Proprietes.FirstOrDefault(x => x.positionCarreau == dep);

            if (proprieteNonHypotheque != null)
            {
                Plateau.Instance.JoueurCourant.Hypothequer(proprieteNonHypotheque);
                return true;
            }
            else if (serviceNonHypotheque != null)
            {
                Plateau.Instance.JoueurCourant.Hypothequer(serviceNonHypotheque);
                return true;
            }
            else if (trainsNonHypotheque != null)
            {
                Plateau.Instance.JoueurCourant.Hypothequer(trainsNonHypotheque);
                return true;
            }
            else return false;
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
