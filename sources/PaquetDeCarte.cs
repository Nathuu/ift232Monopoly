using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using WpfApplication1.sources.Cartes;
using System.Xml.Linq;

namespace WpfApplication1.sources
{
    public class PaquetDeCarte
    {
        private List<Carte> Paquet;

        public PaquetDeCarte(string fsPaquetCartes, Dictionary<String, int> dictionnaireCarreaux)
        {
            Paquet = new List<Carte>();

            XDocument doc = XDocument.Load(fsPaquetCartes);

            IEnumerable<XElement> cartes = doc.Root.Elements();
            foreach (XElement carte in cartes)
            {
                if (carte.Attribute("type").Value == "deplacement")
                {
                    List<int> deplacementsPossibles = new List<int>();
                    foreach (XElement deplacements in carte.Descendants("destinationPossible"))
                    {
                        deplacementsPossibles.Add(dictionnaireCarreaux[deplacements.Attribute("val").Value]);
                    }

                    Paquet.Add(new CarteDeplacement(carte.Value, deplacementsPossibles));
                }
            }
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
