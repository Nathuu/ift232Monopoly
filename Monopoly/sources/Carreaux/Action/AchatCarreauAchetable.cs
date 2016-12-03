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
            else
            {
                CarreauService carreauActuel = (CarreauService)carreau;
                //if (!carreauActuel.estPossede())
                    
            }
        }

        /// <summary>
        /// Verifie s'il a l'argent pour acheter un terrain, et l'achete automatiquement s'il peut
        /// </summary>
        /// <returns>La propriete a bien ete achetee</returns>
        public bool acheterPropriete(CarreauPropriete carreau)
        {
        //ACHAT OBLIGATOIRE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            if (joueurCourant.PeutPayer(carreau.PrixAchat))
            {
                joueurCourant.Payer(carreau.PrixAchat); // le jouer peut decider d'acheter la case.
                carreau.Proprietaire = joueurCourant;
                joueurCourant.Proprietes.Add(carreau);
                return true;
            }
            else
            {
                foreach(CarreauAchetable prop in Plateau.Instance.JoueurCourant.Proprietes)
                {
                    if (!Plateau.Instance.JoueurCourant.Hypotheques.Contains(prop))
                    {
                        Plateau.Instance.JoueurCourant.hypothequer(prop);
                        if (acheterPropriete(carreau))
                        {


                            return true;
                        }
                        else
                        {
                            //return false dans la vraie vie #Jo
                            Plateau.Instance.JoueurCourant.FaitFaillite();
                        }
                    }
                }
            }
            return false;
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
