using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.sources.Carreaux;
using WpfApplication1.sources.Carreaux.CarreauConcret;

namespace WpfApplication1.sources
{
    public static class GestionnaireSauvegardes
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
                Sauvegarder(j, fichierSauvegarde);
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
                Restaurer(j, fichierSauvegarde, nomJoueurCourant);
            }
            fichierSauvegarde.Close();
        }

        public static void Sauvegarder(Joueur j, StreamWriter fichierSauvegarde)
        {
            fichierSauvegarde.WriteLine(j.Nom);
            fichierSauvegarde.WriteLine(j.Position.colonne);
            fichierSauvegarde.WriteLine(j.Position.rangee);
            fichierSauvegarde.WriteLine(j.Argent);
            fichierSauvegarde.WriteLine(j.PositionCarreau);
            fichierSauvegarde.WriteLine(j.EstVivant);
            foreach (CarreauPropriete p in j.Proprietes)
            {
                fichierSauvegarde.WriteLine(p.positionCarreau);
                fichierSauvegarde.WriteLine(p.NombreBatiement);
                fichierSauvegarde.WriteLine(p.EstHypotheque);
            }
            fichierSauvegarde.WriteLine("p");

            foreach (CarreauTrain t in j.Trains)
            {
                fichierSauvegarde.WriteLine(t.positionCarreau);
                fichierSauvegarde.WriteLine(t.EstHypotheque);
            }
            fichierSauvegarde.WriteLine("t");
            foreach (CarreauService s in j.Services)
            {
                fichierSauvegarde.WriteLine(s.positionCarreau);
                fichierSauvegarde.WriteLine(s.EstHypotheque);
            }
            fichierSauvegarde.WriteLine("s");
        }

        public static void Restaurer(Joueur j, StreamReader fichierSauvegarde, string nomJoueurCourant)
        {
            j.Nom = fichierSauvegarde.ReadLine();
            j.Position.colonne = Int32.Parse(fichierSauvegarde.ReadLine());
            j.Position.rangee = Int32.Parse(fichierSauvegarde.ReadLine());
            j.Argent = Int64.Parse(fichierSauvegarde.ReadLine());
            j.PositionCarreau = Int32.Parse(fichierSauvegarde.ReadLine());
            j.EstVivant = Boolean.Parse(fichierSauvegarde.ReadLine());

            while ((char)fichierSauvegarde.Peek() != 'p')
            {
                int positionCarreau = Int32.Parse(fichierSauvegarde.ReadLine());
                CarreauPropriete prop = (CarreauPropriete)Plateau.Instance.getCarreau(positionCarreau);
                prop.NombreBatiement = Int32.Parse(fichierSauvegarde.ReadLine());
                prop.EstHypotheque = Boolean.Parse(fichierSauvegarde.ReadLine());
                j.Proprietes.Add(prop);
            }
            fichierSauvegarde.ReadLine();
            while ((char)fichierSauvegarde.Peek() != 't')
            {
                int positionCarreau = Int32.Parse(fichierSauvegarde.ReadLine());
                CarreauTrain train = (CarreauTrain)Plateau.Instance.getCarreau(positionCarreau);
                train.EstHypotheque = Boolean.Parse(fichierSauvegarde.ReadLine());

                j.Trains.Add(train);
            }
            fichierSauvegarde.ReadLine();
            while ((char)fichierSauvegarde.Peek() != 's')
            {
                int positionCarreau = Int32.Parse(fichierSauvegarde.ReadLine());
                CarreauService service = (CarreauService)Plateau.Instance.getCarreau(positionCarreau);
                service.EstHypotheque = Boolean.Parse(fichierSauvegarde.ReadLine());
                j.Services.Add(service);
            }
            fichierSauvegarde.ReadLine();
            if (j.Nom == nomJoueurCourant)
                Plateau.Instance.JoueurCourant = j;

            j.BougerLaPiece(j.PositionCarreau);

        }
    }
}
