using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Commandes
{
    class CommandTest : Command
    {
        public override void execute()
        {
            TestApplication test = new TestApplication();
            test.ShowDialog();
        }
    }
}
