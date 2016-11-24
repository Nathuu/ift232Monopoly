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
        }

        public  static Plateau Instance
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
        
        private Canvas canvas = new Canvas();
        private Point decalage = new Point(30,30);
        private int hauteur = 660;
        private int largeur = 660;
        private const int nbCarreaux = 40;
        private Carreau[] arrayCarreaux = new Carreau[nbCarreaux];

        //Redefini le joueur courant.
        public void FinTour()
        {
            JoueurCourant.AFiniSonTour = true;
            int i = Joueurs.FindIndex(x => x == JoueurCourant);
            JoueurCourant = Joueurs[(i + 1) % Joueurs.Count];
            JoueurCourant.JouerSonTour();
        }

        public void sauvegarderPartie()
        {
            StreamWriter fichierSauvegarde = new StreamWriter("sauvegardePartie.txt");
            
            //on sauvegarde:
            //Postions de tous les joueurs
            //tous leurs propriété, argents, nom autrement dit tout ce qu'un jouer a
            
            foreach(Joueur j in Joueurs)
            {
                //IL FAUT REPLACE LES IMAGES DES JOUEURS (POSITION) !!!! JN && SARA
               fichierSauvegarde.WriteLine(j.getNom());
               fichierSauvegarde.WriteLine(j.getPosition().getColonne());
               fichierSauvegarde.WriteLine(j.getPosition().getRangee()); 
               fichierSauvegarde.WriteLine(j.getArgent());
            }

            fichierSauvegarde.Close();
        }
        public void restaurerPartie()
        {
            StreamReader fichierSauvegarde = new StreamReader("sauvegardePartie.txt");
            foreach (Joueur j in Joueurs)
            {
                j.setNom(fichierSauvegarde.ReadLine());
                j.setPosition(Int32.Parse(fichierSauvegarde.ReadLine()), Int32.Parse(fichierSauvegarde.ReadLine()));
                j.setArgent(Int64.Parse(fichierSauvegarde.ReadLine()));
            }
            fichierSauvegarde.Close();
        }

        private static int[] indicesProprietes = { 1,3,6,8,9,11,13,14,16,18,19,21,23,24,26,27,29,31,32,34,37,39};
        private static int[] indicesPrison = { 10 };

        private void dessiner()
        {
            // Contour du rectangle
            Rectangle rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Width = this.largeur,
                Height = this.hauteur
            };
            Canvas.SetLeft(rect, decalage.X);
            Canvas.SetTop(rect, decalage.Y);
            canvas.Children.Add(rect);
            // Cases
            foreach(Carreau carreau in arrayCarreaux) {
                carreau.dessiner();
            }
        }

        public Point getDecalage()
        {
            return decalage;
        }

        public Canvas getCanvas()
        {
            return canvas;
        }

        public Carreau[] getArrayCarreaux()
        {
            return arrayCarreaux;
        }

        public int getNbCarreaux()
        {
            return nbCarreaux;
        }
    }
}
