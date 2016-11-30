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
using WpfApplication1.sources.Carreaux;

namespace WpfApplication1.sources
{
    public class Plateau
    {
        private static Plateau instance;
        public List<Joueur> Joueurs { get; set; }
        public Joueur JoueurCourant { get; set; }
        public PaquetDeCarte PaquetCarteChance { get; set; }
        public PaquetDeCarte PaquetCarteCommunaute { get; set; }

        public PaquetDeCarte PaquetTest { get; set; }

        private Carreau[] Cases;

        public Dictionary<String, int> dictionnaireCarreaux { get; private set; } = new Dictionary<string, int>(); 

        protected List<int> Proprietes = new List<int>(); //{ INDEX_BELLEVILLE, 3, 6, 21, 23, 24, 26, 27, 29, 31, 32, 34, 37, 39 };

        private Canvas canvas = new Canvas();
        private Point decalage = new Point(30, 30);
        private int hauteur = 660;
        private int largeur = 660;

        private const Int16 NB_CARREAUX_MAX = 40;
        private const Int16 MONTANT_CARREAU_DEPART = 200;
        
        public Int16 NombreCarreauxMaximal
        {
            get { return NB_CARREAUX_MAX; }
        }
        
        public Int16 MontantCarreauDepart
        {
            get { return MONTANT_CARREAU_DEPART; }
        }

        private Plateau()
        { 
            Joueurs = new List<Joueur>();
            JoueurCourant = null;
            initDictionnaire();
            PaquetTest = new PaquetDeCarte("U:\\monopolyBernard\\ressources\\CartesTest.xml", dictionnaireCarreaux);
            //PaquetCarteChance = new PaquetDeCarte(/*fichier xml CartesChance*/);
            //PaquetCarteCommunaute = new PaquetDeCarte(/*fichier xml CartesCommunaute*/);
            initCarreaux();
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
            private set { }
        }

        private void initDictionnaire()
        {
            dictionnaireCarreaux.Add("INDEX_GO", 0);
            dictionnaireCarreaux.Add("INDEX_BELLEVILLE", 1);
            dictionnaireCarreaux.Add("INDEX_TRAIN_1", 5);
            dictionnaireCarreaux.Add("INDEX_PRISON", 10);
            dictionnaireCarreaux.Add("INDEX_TRAIN_2", 15);
            dictionnaireCarreaux.Add("INDEX_TRAIN_3", 25);
            dictionnaireCarreaux.Add("INDEX_ALLEZ_PRISON", 30);
            dictionnaireCarreaux.Add("INDEX_TRAIN_4", 35);
            dictionnaireCarreaux.Add("INDEX_CARTE_TEST", 2);
        }

        private void initCarreaux()
        {
            Cases = new Carreau[NB_CARREAUX_MAX];

            for (int i = 0; i < NB_CARREAUX_MAX; ++i)
            {
                Cases[i] = new CarreauConcretTest(i);                
            }
            foreach (int indexCase in Proprietes)
            {
                Cases[indexCase] = new CarreauPropriete(indexCase, CarreauPropriete.Couleurs.Brun);
            }
            Cases[dictionnaireCarreaux["INDEX_PRISON"]] = new CarreauPrison(dictionnaireCarreaux["INDEX_PRISON"]);
            Cases[dictionnaireCarreaux["INDEX_ALLEZ_PRISON"]] = new CarreauVaPrison(dictionnaireCarreaux["INDEX_ALLEZ_PRISON"]);
            Cases[dictionnaireCarreaux["INDEX_CARTE_TEST"]] = new CarreauCarte(dictionnaireCarreaux["INDEX_CARTE_TEST"], PaquetTest);
        }
        
        //Redefini le joueur courant.
        public void FinTour()
        {
            int i = Joueurs.FindIndex(x => x == JoueurCourant);
            JoueurCourant = Joueurs[(i + 1) % Joueurs.Count];
            JoueurCourant.JouerSonTour();
            JoueurCourant.getCarreauActuel().execute();
        }

        /// <summary>
        /// détermine l'action à effectuer selon la case et la situation du joueur
        /// </summary>
        /// <returns>action effectuée</returns>
        /*public bool actionSurCase()
        {
            Carreau caseActuelle = JoueurCourant.getCarreauActuel();

            caseActuelle.execute(); 

            
            else if (caseActuelle.estCarreauAction())
            {
                CarreauAction caseAction = (CarreauAction)caseActuelle;
                if (caseAction.estCarreauCarte())
                {
                    CarreauCarte caseCarte = (CarreauCarte)caseAction;
                    Carte cartePigee = caseCarte.Piger();
                    // Effectuer l'action de la carte
                    cartePigee.Executer();
                }
                else if (caseAction.estCarreauVaEnPrison())
                {
                    JoueurCourant.PeutPasserGo = false;
                    JoueurCourant.Avancer(20); // constante avec un nom significatif?
                    JoueurCourant.PeutPasserGo = true;
                    if (JoueurCourant.ACarteSortirPrison)
                        JoueurCourant.ACarteSortirPrison = false; // CHOIX: utiliser carte ou pas?
                    else
                    {
                        MessageBox.Show("Joueur " + JoueurCourant.Nom + " est emprisonné!", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                        JoueurCourant.EstPrisonnier = true;
                    }
                }
                else if (caseAction.estCarreauPrison())
                {
                    if(JoueurCourant.EstPrisonnier)
                        JoueurCourant.TenteSortirPrison();
                }

                return true;

            }
            //Autres actions à déterminer
            else
            {
                return false;
            }
        }*/

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
