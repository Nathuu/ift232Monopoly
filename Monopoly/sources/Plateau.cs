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
using System.Xml.Linq;
using WpfApplication1.sources.Carreaux.CarreauConcret;

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

        public Dictionary<String, Carreau> dictionnaireCarreaux { get; private set; } = new Dictionary<string, Carreau>();

        protected List<int> Proprietes = new List<int>(); //{ INDEX_BELLEVILLE, 3, 6, 21, 23, 24, 26, 27, 29, 31, 32, 34, 37, 39 };

        private Canvas canvas = new Canvas();
        private Point decalage = new Point(30, 30);

        private Random random1 = new Random(DateTime.Now.Millisecond);

        public bool Rejouer { get; set; }

        public int LanceUnDes()// un dé est lancé
        {
            return random1.Next(1, 6);

        }

        private const Int16 NB_CARREAUX_MAX = 40;
        public Int16 getNbCarreauxMax() { return NB_CARREAUX_MAX; }

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
            PaquetTest = new PaquetDeCarte(Properties.Resources.CartesTest, dictionnaireCarreaux);
            // SW Les carreauCarte doivent être initialisés après le paquetTest
            dictionnaireCarreaux.Add("INDEX_CARTE_TEST", new CarreauCarte(7, PaquetTest));
            dictionnaireCarreaux.Add("INDEX_CARTE_CHANCE2", new CarreauCarte(22, PaquetTest));
            dictionnaireCarreaux.Add("INDEX_CARTE_CHANCE3", new CarreauCarte(36, PaquetTest));
            Rejouer = false;
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


        /// <summary>
        /// On va init tous les carreaux de chaque type
        /// </summary>
        private void initDictionnaire()
        { 
            dictionnaireCarreaux.Add("INDEX_GO", new CarreauGo(0));
            dictionnaireCarreaux.Add("INDEX_PRISON", new CarreauVisiterPrison(10));
            dictionnaireCarreaux.Add("INDEX_ALLEZ_PRISON", new CarreauAllerEnPrison(30));
            dictionnaireCarreaux.Add("INDEX_PARKING_GRATUIT", new CarreauParkingGratuit(20));
            // Ajout de cases concrets tests (sera éventuellement remplacé par les vraies cases)
            int[] carreauxConcrets = { 2,12,17,28,33 };
            foreach (int i in carreauxConcrets)
            {
                dictionnaireCarreaux.Add("INDEX_CONCRET_"+ i, new CarreauConcretTest(i));
            }
            lireXMLProprietes();
            lireXMLTrains();
            lireXMLTaxe();
        }

        private void lireXMLTaxe()
        {
            XDocument doc = XDocument.Parse(Properties.Resources.taxe);
            XElement proprietes = doc.Root.Element("Taxes");
            //MessageBox.Show(doc.Root.Elements("Titre").Count()+"");
            foreach (XElement titre in doc.Root.Elements("Titre"))
            {
                String indexDictionnaire = titre.Descendants("IndexDictionnaire").First().Value.ToString();
                int position = Int32.Parse(titre.Descendants("Position").First().Value.ToString());
                long droitPassage = Int32.Parse(titre.Element("Valeur").Value.ToString());          
                Carreau nouveauCarreau = new CarreauTaxe(position, droitPassage);
                //MessageBox.Show(titre.Descendants("Couleur").First().Value.ToString() + " "+ nouveauCarreauCouleur); 
                dictionnaireCarreaux.Add(indexDictionnaire, nouveauCarreau);
            }
        }

        /// <summary>
        /// On vient lire le XML des propriété
        /// </summary>
        private void lireXMLProprietes()
        {
            XDocument doc = XDocument.Parse(Properties.Resources.propriete);            
            XElement proprietes = doc.Root.Element("Proprietes");
            //MessageBox.Show(doc.Root.Elements("Titre").Count()+"");
            foreach (XElement titre in doc.Root.Elements("Titre"))
            {
                String indexDictionnaire = titre.Descendants("IndexDictionnaire").First().Value.ToString();
                int position = Int32.Parse(titre.Descendants("Position").First().Value.ToString());
                CarreauPropriete.Couleurs nouveauCarreauCouleur = (CarreauPropriete.Couleurs) Int32.Parse(titre.Descendants("Couleur").First().Value.ToString());
                long prixAchat = Int32.Parse(titre.Descendants("Prix").First().Value.ToString());
                long droitPassBase = Int32.Parse(titre.Element("Location").Descendants("base").First().Value.ToString());
                long droitPass1Maison = Int32.Parse(titre.Element("Location").Descendants("uneMaison").First().Value.ToString());
                long droitPass2Maisons = Int32.Parse(titre.Element("Location").Descendants("deuxMaisons").First().Value.ToString());
                long droitPass3Maisons = Int32.Parse(titre.Element("Location").Descendants("troisMaisons").First().Value.ToString());
                long droitPass4Maisons = Int32.Parse(titre.Element("Location").Descendants("quatreMaisons").First().Value.ToString());
                long droitPassHotel = Int32.Parse(titre.Element("Location").Descendants("hotel").First().Value.ToString());
                long[] droitPassage = { droitPassBase, droitPass1Maison, droitPass2Maisons, droitPass3Maisons, droitPass4Maisons, droitPassHotel };
                Carreau nouveauCarreau = new CarreauPropriete(position,  nouveauCarreauCouleur, prixAchat, droitPassage);
                //MessageBox.Show(titre.Descendants("Couleur").First().Value.ToString() + " "+ nouveauCarreauCouleur); 
                dictionnaireCarreaux.Add(indexDictionnaire, nouveauCarreau);
            }
        }

        private void lireXMLTrains()
        {                      
            XDocument doc = XDocument.Parse(Properties.Resources.cheminFer);
            XElement trains = doc.Root.Element("CheminFers");
            foreach (XElement titre in doc.Root.Elements("Titre"))
            {
                String indexDictionnaire = titre.Descendants("IndexDictionnaire").First().Value.ToString();
                int position = Int32.Parse(titre.Descendants("Position").First().Value.ToString());
                
                long prixAchat = Int32.Parse(titre.Descendants("Prix").First().Value.ToString());
                
                long droitPass1Train = Int32.Parse(titre.Element("Location").Descendants("unTrain").First().Value.ToString());
                long droitPass2Trains = Int32.Parse(titre.Element("Location").Descendants("deuxTrains").First().Value.ToString());
                long droitPass3Trains = Int32.Parse(titre.Element("Location").Descendants("troisTrains").First().Value.ToString());
                long droitPass4Trains = Int32.Parse(titre.Element("Location").Descendants("quatreTrains").First().Value.ToString());
                
                long[] droitPassage = { droitPass1Train, droitPass2Trains, droitPass3Trains, droitPass4Trains};
                Carreau nouveauTrain = new CarreauTrain(position, prixAchat, droitPassage);
                //MessageBox.Show(titre.Descendants("Couleur").First().Value.ToString() + " "+ nouveauCarreauCouleur); 
                dictionnaireCarreaux.Add(indexDictionnaire, nouveauTrain);
            }
        }

        //Redefini le joueur courant.
        public void JouerTour()
        {
            if (!Rejouer)
            {
                Joueur ProchainJoueur;
                do
                {
                    int i = Joueurs.FindIndex(x => x == JoueurCourant);
                    ProchainJoueur = Joueurs[(i + 1) % Joueurs.Count];
                } while (!ProchainJoueur.EstVivant);
                if(ProchainJoueur == JoueurCourant)
                {
                    //Ceci est la fin du jeu
                    MessageBox.Show("Joueur " + JoueurCourant.Nom + "GAAAAAAAGNNNNEEEEEEEEEEEEEE!!!!!!!!!!!!!!!!!!!!!!!!!!! ", "FIN DE LA PARTIE :)", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Application.Current.Shutdown();
                }
                else
                {
                    JoueurCourant = ProchainJoueur;
                }
            }
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
            foreach (KeyValuePair<string, Carreau> carreau in dictionnaireCarreaux)
            {
                if (indiceCarreau == carreau.Value.positionCarreau)
                    return carreau.Value;
            }
            return null;
        }
    }
}
