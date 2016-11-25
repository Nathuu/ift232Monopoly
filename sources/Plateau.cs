using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;

namespace WpfApplication1.sources
{
    public class Plateau
    {
        private static Plateau instance;
        public List<Joueur> Joueurs { get; set; }
        public Joueur JoueurCourant { get; set; }

        private Plateau()
        {
            Joueurs = new List<Joueur>();
            JoueurCourant = null;
        }

        public static Plateau Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Plateau();
                }
                return instance;
            }
        }

        //Redefini le joueur courant.
        public void FinTour()
        {
            int i = Joueurs.FindIndex(x => x == JoueurCourant);
            JoueurCourant = Joueurs[(i + 1) % Joueurs.Count];
            JoueurCourant.JouerSonTour();
        }

        public void sauvegarderPartie()
        {
            SauvegardeFichier saveWindow = new SauvegardeFichier();
            saveWindow.ShowDialog();
            StreamWriter fichierSauvegarde = new StreamWriter(saveWindow.FileName);
            //on sauvegarde:
            //Postions de tous les joueurs
            //tous leurs propriété, argents, nom autrement dit tout ce qu'un jouer a

            fichierSauvegarde.WriteLine(JoueurCourant.Nom);
            foreach (Joueur j in Joueurs)
            {
                j.Sauvegarder(fichierSauvegarde);
            }
            fichierSauvegarde.Close();
        }
        public void restaurerPartie()
        {
            List<string> fichierRestaurationDisponible = new List<string>();
            DirectoryInfo dinfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                fichierRestaurationDisponible.Add(file.Name);
            }

            RestaureFichier restoreWindow = new RestaureFichier(fichierRestaurationDisponible);
            restoreWindow.ShowDialog();
                   
            StreamReader fichierSauvegarde = new StreamReader(restoreWindow.FileName);
            String nomJoueurCourant = fichierSauvegarde.ReadLine();

            foreach (Joueur j in Joueurs)
            {
                j.Nom = fichierSauvegarde.ReadLine();
                j.Position.colonne = Int32.Parse(fichierSauvegarde.ReadLine());
                j.Position.rangee = Int32.Parse(fichierSauvegarde.ReadLine());
                j.Argent = Int64.Parse(fichierSauvegarde.ReadLine());
                j.PositionCarreau = Int32.Parse(fichierSauvegarde.ReadLine());
                j.Avancer(0);
                if (j.Nom == nomJoueurCourant)
                    JoueurCourant = j;

            }            
            fichierSauvegarde.Close();
        }
    }
}
