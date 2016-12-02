using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplication1.sources;
using WpfApplication1.sources.Carreaux;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for InformationJoueur.xaml
    /// </summary>
    public partial class InformationJoueur : Window
    {
        public InformationJoueur(Joueur j)
        {
            InitializeComponent();
            txtNom.Text = j.Nom;
            txtBudget.Text = j.Argent.ToString();
            string liste="";

            foreach (CarreauAchetable p in j.Proprietes)
            {
                liste += p.positionCarreau.ToString();
                liste += ", ";
            }
            if (liste == "") liste = "Aucune";
            txtListProp.Text = liste;


        }
    }
}
