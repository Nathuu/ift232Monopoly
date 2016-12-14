using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApplication1.sources.Carreaux;

namespace WpfApplication1.sources.Commandes
{
    public static class Prompt
    {
        public static string ShowDialog(string label1, string label2, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 300,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            // Propriétés du joueur
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = label1 };
            ComboBox listePropJoueur = new ComboBox() { Left = 50, Top = 80, Width = 400 };
            string[] listePropJoueurStr = new string[Plateau.Instance.JoueurCourant.Proprietes.Count];
            int noProp = 0;
            foreach (CarreauAchetable p in Plateau.Instance.JoueurCourant.Proprietes)
            {
                listePropJoueurStr[noProp] = p.ToString();
                noProp++;
            }
            listePropJoueur.DataSource = listePropJoueurStr;

            // Propriétés qui ont été achetées mais qui n'appartiennent pas au joueur
            Label textLabel2 = new Label() { Left = 50, Top = 120, Width = 400, Text = label2 };
            ComboBox listePropTous = new ComboBox() { Left = 50, Top = 180, Width = 400 };
            string[] listePropTousStr = new string[(CarreauAchetable.getTousLesCarreauxAchetablesPossedes().Count - Plateau.Instance.JoueurCourant.Proprietes.Count)];
            int noPropTous = 0;
            foreach (CarreauAchetable c in CarreauAchetable.getTousLesCarreauxAchetablesPossedes())
            {
                if(! Plateau.Instance.JoueurCourant.Proprietes.Contains(c))
                {
                    listePropTousStr[noPropTous] = c.ToString();
                    noPropTous++;
                }
            }
            listePropTous.DataSource = listePropTousStr;

            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 220, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(listePropJoueur);
            prompt.Controls.Add(textLabel2);
            prompt.Controls.Add(listePropTous);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? listePropJoueurStr[listePropJoueur.SelectedIndex] + " contre "+ listePropTousStr[listePropTous.SelectedIndex] : "";
        }
    }
}
