using System;

namespace WpfApplication1.sources
{
    internal class CarteCommunaute:Carte
    {
        public CarteCommunaute(String desc, ref Carreau destination, int deplacement = 0, int montant = 0) : 
            base(desc, ref destination, deplacement, montant) { }
    }
}