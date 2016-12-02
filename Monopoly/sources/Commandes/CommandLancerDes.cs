using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public class CommandLancerDes : Command
    {
        public override void execute()
        {
            Plateau.Instance.JouerTour();
        }
    }
}
