using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using WpfApplication1.sources.Cartes;

namespace WpfApplication1.sources
{
    public class PaquetDeCarte
    {
        private List<Carte> Paquet;
        private String nomFichierXML;

        public PaquetDeCarte(FileStream fsPaquetCartes)
        {
            Paquet = new List<Carte>();

            //this.nomFichierXML = fsPaquetCartes;
            XmlDocument doc = new XmlDocument();
            doc.Load(fsPaquetCartes);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Attributes["type"]?.InnerText == "deplacement")
                {
                    List<int> deplacementsPossibles = new List<int>();
                    foreach (XmlNode deplacements in node.ChildNodes)
                    {
                        String key = deplacements.Attributes["val"]?.InnerText;
                        int i = Plateau.Instance.dictionnaireCarreaux[key];
                        deplacementsPossibles.Add(i);
                    }

                    Paquet.Add(new CarteDeplacement(node.InnerText, deplacementsPossibles));
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
