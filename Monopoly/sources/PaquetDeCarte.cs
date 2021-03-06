﻿using System;
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
        private int nbCartesPigees = 0;
        private Random rand = new Random(DateTime.Now.Millisecond);

        public PaquetDeCarte(string fsPaquetCartes, Dictionary<string, Carreau> dictionnaireCarreaux)
        {
            Paquet = new List<Carte>();

            XDocument doc = XDocument.Parse(fsPaquetCartes);

            IEnumerable<XElement> cartes = doc.Root.Elements();
            foreach (XElement carte in cartes)
            {
                if (carte.Attribute("type").Value == "deplacement")
                {
                    List<Carreau> deplacementsPossibles = new List<Carreau>();
                    foreach (XElement deplacements in carte.Descendants("destinationPossible"))
                    {
                        deplacementsPossibles.Add(dictionnaireCarreaux[deplacements.Attribute("val").Value]);
                    }

                    Paquet.Add(new CarteDeplacement(carte.Value, deplacementsPossibles, bool.Parse(carte.Attribute("passerGo").Value)));
                }
            }
        }

        public Carte Piger()
        {
            // Pige la 1ere carte, puis la met a la fin du paquet
            int index = rand.Next(0, Paquet.Count - 1 - nbCartesPigees);
            Carte cartePigee = Paquet[index];
            Paquet.RemoveAt(index);
            Paquet.Add(cartePigee);
            if (++nbCartesPigees == Paquet.Count - 1)
                nbCartesPigees = 0;
            return cartePigee;
        }
    }
}
