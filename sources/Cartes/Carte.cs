using System;

namespace WpfApplication1.sources
{
    abstract public class Carte
    {
        public String Description { get; protected set; } 
        public int Montant { get; protected set; }
        public int Deplacement { get; protected set; } // Une seule carte requiert un deplacement numerique.
        public Carreau Destination { get; }
        bool PasserGo { get; } // La carte Va En Prison neglige l'effet de la case Go

        /// <summary>
        /// La carte constitue un evenement.
        /// La carte contien une description, un carreau marquant sa destination (si applicable),
        /// un montant a gagner (positif) ou a perdre (negatif), ainsi qu'un deplacement (pour une
        /// seule carte, peut etre pas necessaire)
        /// Eventuellement, la destination devrait etre calculee dynamiquement
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="destination"></param>
        /// <param name="deplacement"></param>
        /// <param name="montant"></param>
        public Carte(String desc, ref Carreau destination, int deplacement = 0, int montant = 0)
        {
            Description = desc;
            Destination = destination;
            Montant = montant;
            Deplacement = deplacement;
            PasserGo = true;
        }

        public void Executer()
        {
            CalculerMontant();
            CalculerDestination();
        }

        private void CalculerMontant()
        {
            // Certaines cartes ont un montant variable, ex: selon le nb de maisons
            throw new NotImplementedException();
        }

        private void CalculerDestination()
        {
            // Certaines cartes ont une destination variable selon la position du joueur
            throw new NotImplementedException();
        }

    }
}