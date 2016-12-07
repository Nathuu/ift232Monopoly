using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.sources
{
    public static class GestionnaireJeu
    {

        //Les methodes sauvegarder partie et restorer devrais etre dans Joueurs. Puisqu'on creer nos fichier a partir de nos joueurs
        public static void sauvegarderPartie()
        {
            SauvegardeFichier saveWindow = new SauvegardeFichier();
            saveWindow.ShowDialog();
            StreamWriter fichierSauvegarde = new StreamWriter(saveWindow.FileName);
            //on sauvegarde:
            //Postions de tous les joueurs
            //tous leurs propriété, argents, nom autrement dit tout ce qu'un jouer a

            fichierSauvegarde.WriteLine(Plateau.Instance.JoueurCourant.Nom);
            foreach (Joueur j in Plateau.Instance.Joueurs)
            {
                j.Sauvegarder(fichierSauvegarde);
            }
            fichierSauvegarde.Close();
        }
        public static void restaurerPartie()
        {
            //ajouter une validation si le restore est anuler
            List<string> fichierRestaurationDisponible = new List<string>();
            DirectoryInfo dinfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                fichierRestaurationDisponible.Add(file.Name);
            }

            RestaurationFichier restoreWindow = new RestaurationFichier(fichierRestaurationDisponible);
            restoreWindow.ShowDialog();

            StreamReader fichierSauvegarde = new StreamReader(restoreWindow.FileName);
            String nomJoueurCourant = fichierSauvegarde.ReadLine();

            foreach (Joueur j in Plateau.Instance.Joueurs)
            {
                j.Restaurer(fichierSauvegarde, nomJoueurCourant);
            }
            fichierSauvegarde.Close();
        }
    }
}
