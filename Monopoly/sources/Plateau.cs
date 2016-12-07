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
        public PaquetDeCarte PaquetTest { get; set; }
        public Dictionary<String, Carreau> dictionnaireCarreaux { get; private set; } = new Dictionary<string, Carreau>();
        public bool Rejouer { get; set; }
        public int JoueurRestant { get; set; }
        public int MaisonDisponible { get; set; }
        public int HotelDisponible { get; set; }

        private const Int16 MONTANT_CARREAU_DEPART = 200;
        private const Int16 NB_CARREAUX_MAX = 40;
        protected List<int> Proprietes = new List<int>(); //{ INDEX_BELLEVILLE, 3, 6, 21, 23, 24, 26, 27, 29, 31, 32, 34, 37, 39 };
        private Canvas canvas = new Canvas();
        private Point decalage = new Point(30, 30);
        private Random random1 = new Random(DateTime.Now.Millisecond);
       

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
            //initialisation de tous les carreaux
            initDictionnaire();
            Rejouer = false;
            MaisonDisponible = 32;
            HotelDisponible = 12; 
            JoueurRestant = Joueurs.Count();
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

        public int LanceUnDes()// un dé est lancé
        {
            return random1.Next(1, 6);

        }

        /// <summary>
        /// On va init tous les carreaux de chaque type
        /// </summary>
        private void initDictionnaire()
        {
            //on initialise tous les carreaux 
            init4Coins();        
            lireXMLProprietes();
            lireXMLTrains();
            lireXMLTaxe();
            lireXMLServices();
            //Les carreauCarte doivent être initialisés après le paquetTest car lors de la lecture 
            //des cartes on assigne des déplacement  à des index connus du dictionnaire.
            //ces index doivent être créé avant
            PaquetTest = new PaquetDeCarte(Properties.Resources.CartesTest, dictionnaireCarreaux);
            initCarreauCarte();

        }
        public void init4Coins()
        {
            // ON INDEX LES 4 COINS
            dictionnaireCarreaux.Add("INDEX_GO", new CarreauGo(0));
            dictionnaireCarreaux.Add("INDEX_PRISON", new CarreauVisiterPrison(10));
            dictionnaireCarreaux.Add("INDEX_ALLEZ_PRISON", new CarreauAllerEnPrison(30));
            dictionnaireCarreaux.Add("INDEX_PARKING_GRATUIT", new CarreauParkingGratuit(20));

        }
        public void initCarreauCarte()
        {
            //on index les carreaux de cartes 
            dictionnaireCarreaux.Add("INDEX_CARTE_CHANCE1", new CarreauCarte(7, PaquetTest));
            dictionnaireCarreaux.Add("INDEX_CARTE_CHANCE2", new CarreauCarte(22, PaquetTest));
            dictionnaireCarreaux.Add("INDEX_CARTE_CHANCE3", new CarreauCarte(36, PaquetTest));
            // cartes Communautés
            dictionnaireCarreaux.Add("INDEX_CARTE_COMMUNAUTE1", new CarreauCarte(2, PaquetTest));
            dictionnaireCarreaux.Add("INDEX_CARTE_COMMUNAUTE2", new CarreauCarte(17, PaquetTest));
            dictionnaireCarreaux.Add("INDEX_CARTE_COMMUNAUTE3", new CarreauCarte(33, PaquetTest));
        }

        /// <summary>
        /// On vient lire le XML des propriété
        /// et on initalise les carreaux Propriétées
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
                CarreauPropriete.Couleurs nouveauCarreauCouleur = (CarreauPropriete.Couleurs)Int32.Parse(titre.Descendants("Couleur").First().Value.ToString());
                long prixAchat = Int32.Parse(titre.Descendants("Prix").First().Value.ToString());
                long droitPassBase = Int32.Parse(titre.Element("Location").Descendants("base").First().Value.ToString());
                long droitPass1Maison = Int32.Parse(titre.Element("Location").Descendants("uneMaison").First().Value.ToString());
                long droitPass2Maisons = Int32.Parse(titre.Element("Location").Descendants("deuxMaisons").First().Value.ToString());
                long droitPass3Maisons = Int32.Parse(titre.Element("Location").Descendants("troisMaisons").First().Value.ToString());
                long droitPass4Maisons = Int32.Parse(titre.Element("Location").Descendants("quatreMaisons").First().Value.ToString());
                long droitPassHotel = Int32.Parse(titre.Element("Location").Descendants("hotel").First().Value.ToString());
                long[] droitPassage = { droitPassBase, droitPass1Maison, droitPass2Maisons, droitPass3Maisons, droitPass4Maisons, droitPassHotel };
                Carreau nouveauCarreau = new CarreauPropriete(position, nouveauCarreauCouleur, prixAchat, droitPassage);
                //MessageBox.Show(titre.Descendants("Couleur").First().Value.ToString() + " "+ nouveauCarreauCouleur); 
                dictionnaireCarreaux.Add(indexDictionnaire, nouveauCarreau);
            }
        }
        /// <summary>
        /// on lis le xml des trains
        /// on initialise les trains
        /// </summary>
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

                long[] droitPassage = { droitPass1Train, droitPass2Trains, droitPass3Trains, droitPass4Trains };
                Carreau nouveauTrain = new CarreauTrain(position, prixAchat, droitPassage);
                //MessageBox.Show(titre.Descendants("Couleur").First().Value.ToString() + " "+ nouveauCarreauCouleur); 
                dictionnaireCarreaux.Add(indexDictionnaire, nouveauTrain);
            }
        }
        /// <summary>
        /// on initialise les carreaux taxes
        /// </summary>
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
        /// on initilase
        /// </summary>
        private void lireXMLServices()
        {
            XDocument doc = XDocument.Parse(Properties.Resources.services);
            XElement trains = doc.Root.Element("Services");
            foreach (XElement titre in doc.Root.Elements("Titre"))
            {
                String indexDictionnaire = titre.Descendants("IndexDictionnaire").First().Value.ToString();
                int position = Int32.Parse(titre.Descendants("Position").First().Value.ToString());
                long prixAchat = Int32.Parse(titre.Element("Prix").Value.ToString());
                Carreau nouveauCarreau = new CarreauService(position, prixAchat);
                dictionnaireCarreaux.Add(indexDictionnaire, nouveauCarreau);
            }
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
