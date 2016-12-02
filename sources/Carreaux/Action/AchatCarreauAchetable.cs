using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool acheterPropriete(CarreauPropriete carreau)
        {
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            if (joueurCourant.PeutPayer(carreau.getPrixAchat()))
            {
                joueurCourant.Payer(carreau.getPrixAchat()); // le jouer peut decider d'acheter la case.
                carreau.Proprietaire = joueurCourant;
                joueurCourant.Proprietes.Add(carreau);
                return true;
            }
            return false;
        }

        public bool acheterTrain(CarreauTrain carreau)
        {
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            if (joueurCourant.PeutPayer(carreau.getPrixAchat()))
            {
                joueurCourant.Payer(carreau.getPrixAchat()); // le jouer peut decider d'acheter la case.
                carreau.Proprietaire = joueurCourant;
                joueurCourant.Proprietes.Add(carreau);
                return true;
            }
            return false;
        }
    }
}
