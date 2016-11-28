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
        public PaquetDeCarte PaquetCarteChance { get; set; }
        public PaquetDeCarte PaquetCarteCommunaute { get; set; }
        private Carreau[] Cases;


        protected List<int> Proprietes = new List<int>() { 1, 3, 6, 21, 23, 24 , 26, 27, 29, 31, 32, 34 , 37 ,39 };

        private Canvas canvas = new Canvas();
        private Point decalage = new Point(30, 30);
        private int hauteur = 660;
        private int largeur = 660;

        private const Int16 nombreCarreauxMaximal = 40;
        
        public Int16 NombreCarreauxMaximal
        {
            get { return nombreCarreauxMaximal; }
        }
        
    private Plateau()
        { 
            Joueurs = new List<Joueur>();
            JoueurCourant = null;          
            initCarreaux();
            PaquetCarteChance = new PaquetDeCarte(/*fichier xml CartesChance*/);
            PaquetCarteCommunaute = new PaquetDeCarte(/*fichier xml CartesCommunaute*/);
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

        private void initCarreaux()
        {
            Cases = new Carreau[nombreCarreauxMaximal];

            for (int i = 0; i < nombreCarreauxMaximal; ++i)
            {
                Cases[i] = new CarreauConcretTest(i);                
            }
            foreach (int indexCase in Proprietes)
            {
                Cases[indexCase] = new CarreauPropriete(indexCase, CarreauPropriete.Couleurs.Brun);
            }
        }
        
        //Redefini le joueur courant.
        public void FinTour()
        {
            int i = Joueurs.FindIndex(x => x == JoueurCourant);
            JoueurCourant = Joueurs[(i + 1) % Joueurs.Count];
            JoueurCourant.JouerSonTour();
        }
        
        //Les methodes sauvegarder partie et restorer devrais etre dans Joueurs. Puisqu'on creer nos fichier a partir de nos joueurs
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

            RestaurationFichier restoreWindow = new RestaurationFichier(fichierRestaurationDisponible);
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
        

        /// <summary>
        /// </summary>
        /// <param name="position"></param>
        /// <returns>
        /// Carreau qui correspond a la position
        /// </returns>
        public Carreau getCarreau(int indiceCarreau)
        {
            //indice  0 à 39
            return Cases[indiceCarreau];
        }
    }
}
