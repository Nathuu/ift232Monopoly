using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.sources.Carreaux.Action
{
    class AllezEnPrison : ICommande
    {
        public void execute(Carreau carreau)
        {
            AllerEnPrison(carreau);
        }

        // Déplacer le joueur vers la prison
        private void AllerEnPrison(Carreau carreau)
        {
            Joueur j = Plateau.Instance.JoueurCourant;
            j.PositionCarreau = 10;
            j.Position = Carreau.conversionInt2Position(j.PositionCarreau);
            Grid.SetRow(j.Image, j.Position.rangee + 1);
            Grid.SetColumn(j.Image, j.Position.colonne + 1);

            j.EstPrisonnier = true;
            MessageBox.Show("Vous allez en prison!");


        }
    }
}
