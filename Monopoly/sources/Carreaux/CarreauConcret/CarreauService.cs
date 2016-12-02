using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Carreaux.CarreauConcret
{
    class CarreauService : CarreauAchetable
    {
        public CarreauService(int position, long prixAchat) : base(position, prixAchat) { }

        public override long getPrixPassage()
        {
            int sommeDes = Plateau.Instance.LanceUnDes() + Plateau.Instance.LanceUnDes();
            if (Proprietaire.estSeulProprietaireServices())
                return sommeDes * 10;
            return sommeDes * 4;
        }
    }
}
