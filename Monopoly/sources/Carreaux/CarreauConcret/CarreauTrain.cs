﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux;

namespace WpfApplication1.sources.Carreaux.CarreauConcret
{
    class CarreauTrain : CarreauAchetable
    {
        public CarreauTrain(int positionCarreau, long prixAchat, long[] droitPassage) : base(positionCarreau, prixAchat)
        {
            this.DroitPassage = new long[4] { 25, 50, 75, 100 };
        }

        public override long getPrixPassage()
        {
                return DroitPassage[Proprietaire.Trains.Count() - 1];
        }

    }
}
