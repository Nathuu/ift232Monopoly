using System;

namespace WpfApplication1.sources
{
    public class CarteChance:Carte
    {
        public CarteChance(String desc, ref Carreau destination, int deplacement = 0, int montant = 0) : 
            base(desc, ref destination, deplacement, montant) { }
    }
}