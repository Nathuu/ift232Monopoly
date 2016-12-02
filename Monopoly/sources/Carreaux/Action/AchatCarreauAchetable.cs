using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Forms;
using WpfApplication1.sources.Carreaux.CarreauConcret;

namespace WpfApplication1.sources.Carreaux.Action
{
    class AchatCarreauAchetable : ICommande
    {
        public void execute(Carreau carreau)
        {

            if (carreau is CarreauPropriete)
            {
                CarreauPropriete carreauActuel = (CarreauPropriete)carreau;
                if (!carreauActuel.estPossede())
                    acheterPropriete(carreauActuel);
            }
            else if(carreau is CarreauTrain)
            {
                CarreauTrain carreauActuel = (CarreauTrain)carreau;
                if (!carreauActuel.estPossede())
                    acheterTrain(carreauActuel);
            }
        }
        
        /// <summary>
        /// Verifie s'il a l'argent pour acheter un terrain, et l'achete automatiquement s'il peut
        /// </summary>
        /// <returns>La propriete a bien ete achetee</returns>
        public void acheterPropriete(CarreauPropriete carreau)
        {
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            if (joueurCourant.PeutPayer(carreau.PrixAchat))
            {
                
                if(DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Voulez-vous achetez la proprieté?\n" + " Prix: " + carreau.PrixAchat +  " $        Votre argent: " + Plateau.Instance.JoueurCourant.Argent + " $ ", "Achat de la proprieté", MessageBoxButtons.YesNo, MessageBoxIcon.Question)){
                    joueurCourant.Payer(carreau.PrixAchat); // le jouer peut decider d'acheter la case.
                    carreau.Proprietaire = joueurCourant;
                    joueurCourant.Proprietes.Add(carreau);
                }
                
            }
            else
            {
                if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Voulez-vous hypothéquer pour acheter la proprieté?\n" + " Prix: " + carreau.PrixAchat + " $        Votre argent: " + Plateau.Instance.JoueurCourant.Argent + " $ ", "Achat de la proprieté", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    if (Plateau.Instance.JoueurCourant.HypothequerSuivant())
                    {
                        acheterPropriete(carreau);
                    }
                    else
                    {
                        Plateau.Instance.JoueurCourant.FaitFaillite();
                    }
                }
            }
        }

        public bool acheterTrain(CarreauTrain carreau)
        {
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            if (joueurCourant.PeutPayer(carreau.PrixAchat))
            {
                joueurCourant.Payer(carreau.PrixAchat); // le jouer peut decider d'acheter la case.
                carreau.Proprietaire = joueurCourant;
                joueurCourant.Trains.Add(carreau);
                return true;
            }
            return false;
        }
    }
}
