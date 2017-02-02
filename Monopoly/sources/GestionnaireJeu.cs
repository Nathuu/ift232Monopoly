

namespace WpfApplication1.sources
{
    public class GestionnaireJeu
    {
        //Redefini le joueur courant.
        public static void JouerTour()
        {
            Plateau.Instance.JoueurCourant.JouerSonTour();
            if (!Plateau.Instance.Rejouer)
            {
                ChangementJoueur();
            }
			//test push push
        }

        public static void ChangementJoueur()
        {
            do
            {
                int i = Plateau.Instance.Joueurs.FindIndex(x => x == Plateau.Instance.JoueurCourant);
                Plateau.Instance.JoueurCourant = Plateau.Instance.Joueurs[(i + 1) % Plateau.Instance.Joueurs.Count];
            } while (!Plateau.Instance.JoueurCourant.EstVivant);
        }

    }
}
