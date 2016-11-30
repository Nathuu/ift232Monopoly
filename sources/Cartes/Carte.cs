using System;

namespace WpfApplication1.sources
{
    abstract public class Carte
    {
        public String Description { get; protected set; } 

        /// <summary>
        /// La carte constitue un evenement.
        /// La carte contient une description, un carreau marquant sa destination (si applicable),
        /// un montant a gagner (positif) ou a perdre (negatif), ainsi qu'un deplacement (pour une
        /// seule carte, peut etre pas necessaire)
        /// Eventuellement, la destination devrait etre calculee dynamiquement
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="destination"></param>
        /// <param name="deplacement"></param>
        /// <param name="montant"></param>
        public Carte(String desc)
        {
            this.Description = desc;
        }

        public abstract void Executer();
    }
}