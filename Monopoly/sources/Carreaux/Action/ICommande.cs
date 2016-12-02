using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources.Carreaux.Action
{
    public interface ICommande
    {
         void execute(Carreau carreau);
    
    }
}
