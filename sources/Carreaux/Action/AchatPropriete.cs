using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Carreaux.Action
{
    class AchatPropriete : ICommande
    {
        public void execute(Carreau carreau)
        {
            CarreauPropriete carreauActuel = (CarreauPropriete)carreau;
            if (!carreauActuel.estPossede())
                acheterPropriete(carreauActuel);
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
    }
}
