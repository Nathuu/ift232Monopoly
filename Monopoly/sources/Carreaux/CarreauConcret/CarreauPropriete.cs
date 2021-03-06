﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace WpfApplication1.sources.Carreaux
{
    /// <summary>
    /// Carreau qui sont des carreau achetables, mais plus spécifiquement des propriété de style couleurs. ils ont une différente facon d'utiliser le prixPassage
    /// </summary>
    public class CarreauPropriete : CarreauAchetable
    {
        public enum Couleurs
        {
            Brun, BleuPale, Rose, Orange, Rouge, Jaune, Vert, BleuFonce
        };

        // Ancienne implementation du loyer
        //public int Loyer { get; private set; }
        public int NombreBatiement { get; set; }

        public Couleurs Couleur { get; private set; }

        public CarreauPropriete(int positionCarreau, Couleurs Couleur, long prixAchat, long[] droitPassage) : base(positionCarreau, prixAchat)
        {
            this.Couleur = Couleur;
            this.DroitPassage = droitPassage;
            this.NombreBatiement = 0;
        }

        public bool estLibre()
        {
            return (Proprietaire == null);
        }

        public void AchatBatiement()
        {            
            ++NombreBatiement;
        }

        public void VenteBatiement(int nombre)
        {
            NombreBatiement -= nombre;
        }


        public override long getPrixPassage()
        {
            if (NombreBatiement == 0)
            {
                if (Proprietaire.estSeulProprietaireDeMemeCouleur(Couleur))
                    return 2 * DroitPassage[0];
                else
                    return DroitPassage[0];
            }
            else
            {
                return DroitPassage[NombreBatiement];
            }
        }
    }
}
