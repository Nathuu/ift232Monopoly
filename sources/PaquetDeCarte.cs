using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public class PaquetDeCarte
    {
        private List<Carte> Paquet;

        public PaquetDeCarte(/*fichier xml de cartes*/)
        {
            /*while fichier.hasNext() Paquet.add(fichier.getCarte)*/
        }

        public Carte Piger()
        {
            // Pige la 1ere carte, puis la met a la fin du paquet
            Carte cartePigee = Paquet[0];
            Paquet.Remove(cartePigee);
            Paquet.Add(cartePigee);
            return cartePigee;
        }
    }
}
