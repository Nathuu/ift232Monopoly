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
            Joueur joueurCourant = Plateau.Instance.JoueurCourant;
            if (joueurCourant.PeutPayer(carreau.PrixAchat))
            {
                DialogResult confirmResult = System.Windows.Forms.MessageBox.Show("Souhaitez-vous acheter cette propriété?", "Achat de propriété", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    joueurCourant.Payer(carreau.PrixAchat); // le jouer peut decider d'acheter la case.
                    carreau.Proprietaire = joueurCourant;
                    joueurCourant.Proprietes.Add(carreau);
                    return true;
                }
                else
                {
                    System.Windows.MessageBox.Show("Le joueur "+joueurCourant.Nom+" n'a pas acheté la propriété", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                foreach(CarreauAchetable prop in Plateau.Instance.JoueurCourant.Proprietes)
                {
                    if (!prop.EstHypotheque)
                    {
                        Plateau.Instance.JoueurCourant.Hypothequer(prop);

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
