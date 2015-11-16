// FormSimVille.cs
// Jeu SimVille
// Programmé par Jean-Philippe Croteau
// Le 25 mars 2013


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimVille
{
    public partial class FormSimVille : Form
    {
        private Region jeu;

        /// <summary>
        /// Constructeur du form
        /// </summary>
        public FormSimVille()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Créer toute le form
        /// </summary>
        private void FormSimVille_Load(object sender, EventArgs e)
        {
            #region Visuel des boutons du bas et mettre les images dans le tableLayoutPanel
            btnEnergie.Image = imageList.Images[(int)Acre.TypeTerrain.Energie];
            btnEnergie.TextImageRelation = TextImageRelation.ImageAboveText;
            btnEnergie.Checked = true;
            btnEnergie.Appearance = Appearance.Button;

            btnPolice.Image = imageList.Images[(int)Acre.TypeTerrain.Police];
            btnPolice.TextImageRelation = TextImageRelation.ImageAboveText;
            btnPolice.Appearance = Appearance.Button;

            btnHopital.Image = imageList.Images[(int)Acre.TypeTerrain.Hopital];
            btnHopital.TextImageRelation = TextImageRelation.ImageAboveText;
            btnHopital.Appearance = Appearance.Button;

            btnResidentiel.Image = imageList.Images[(int)Acre.TypeTerrain.Residentiel];
            btnResidentiel.TextImageRelation = TextImageRelation.ImageAboveText;
            btnResidentiel.Appearance = Appearance.Button;

            btnCommercial.Image = imageList.Images[(int)Acre.TypeTerrain.Commercial];
            btnCommercial.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCommercial.Appearance = Appearance.Button;

            btnStade.Image = imageList.Images[(int)Acre.TypeTerrain.Stade];
            btnStade.TextImageRelation = TextImageRelation.ImageAboveText;
            btnStade.Appearance = Appearance.Button;

            btnStatistiques.Image = imageList.Images[(int)Acre.TypeTerrain.Statistiques];
            btnStatistiques.TextImageRelation = TextImageRelation.ImageAboveText;
            btnStatistiques.Appearance = Appearance.Button;

            for (int i = 0; i < tlpRegion.ColumnCount; i++)
            {
                for (int j = 0; j < tlpRegion.RowCount; j++)
                {
                    PictureBox boite = new PictureBox();
                    boite.Width = imageList.ImageSize.Width;
                    boite.Height = imageList.ImageSize.Height;
                    boite.Margin = new Padding(0, 0, 0, 0);
                    tlpRegion.Controls.Add(boite, i, j);
                    boite.Click += new System.EventHandler(Region_Click);
                }
            }
            #endregion

            jeu = new Region(tlpRegion.RowCount, tlpRegion.ColumnCount);
            jeu.Initialiser();
            minuterie.Enabled = true;
            Actualiser();
        }

        /// <summary>
        /// Appelle la méthode spécifique selon le choix de l'utilisateur.
        /// </summary>
        private void Region_Click(object sender, EventArgs e)
        {
            Control acre = (Control)sender;
            int positionY = tlpRegion.GetRow(acre);
            int positionX = tlpRegion.GetColumn(acre);
            if (btnResidentiel.Checked)
            {
                ValiderConstruction(Acre.TypeTerrain.Residentiel, positionX, positionY);
            }
            else if (btnCommercial.Checked)
            {
                ValiderConstruction(Acre.TypeTerrain.Commercial, positionX, positionY);
            }
            else if (btnStade.Checked)
            {
                ValiderConstruction(Acre.TypeTerrain.Stade, positionX, positionY);
            }
            else if (btnHopital.Checked)
            {
                ValiderConstruction(Acre.TypeTerrain.Hopital, positionX, positionY);
            }
            else if (btnPolice.Checked)
            {
                ValiderConstruction(Acre.TypeTerrain.Police, positionX, positionY);
            }
            else if (btnEnergie.Checked)
            {
                ValiderConstruction(Acre.TypeTerrain.Energie, positionX, positionY);
            }
            else if (btnStatistiques.Checked)
            {
                progbarLoisirs.Value = jeu.Carte[positionX, positionY].Loisirs;
                progbarSante.Value = jeu.Carte[positionX, positionY].Sante;
                progbarCriminilatite.Value = jeu.Carte[positionX, positionY].Criminalite;
                progbarSatisfaction.Value = jeu.Carte[positionX, positionY].Satisfaction;
                lblColonne.Text = positionY.ToString();
                lblLigne.Text = positionX.ToString();
                picboxSelection.Image = imageList.Images[(int)jeu.Carte[positionX, positionY].Terrain];
            }
            Actualiser();
        }


        /// <summary>
        /// Ferme le form quand on clic sur quitter
        /// </summary>
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Actualise l'affichage
        /// </summary>
        private void Actualiser()
        {
            int imageTerrain = 0;
            for (int i = 0; i < tlpRegion.ColumnCount; i++)
            {
                for (int j = 0; j < tlpRegion.RowCount; j++)
                {
                    PictureBox boite = (PictureBox)tlpRegion.GetControlFromPosition(i, j);
                    imageTerrain = (int)jeu.Carte[i, j].Terrain;
                    boite.Image = imageList.Images[imageTerrain];
                    
                }
            }

            lblEnBanque.Text = jeu.EconomieJeu.MontantBanque.ToString();
            lblRevenuMoyen.Text = jeu.EconomieJeu.RevenuMoyen.ToString();
            lblRevenuMois.Text = jeu.EconomieJeu.RevenuVille.ToString();
            lblHabitants.Text = jeu.NombreHabitant.ToString();
            lblHabitantsAcre.Text = jeu.HabitantAcre.ToString();
            lblDate.Text = jeu.Date.ToLongDateString();


        }

        /// <summary>
        /// Affiche le message d'erreur passé en paramètre.
        /// </summary>
        /// <param name="message">Message d'erreur qu'on veut afficher</param>
        private void AfficherMessage(Messages.Id message)
        {
            MessageBox.Show(Messages.Message[(int)message]);
        }

        private void ValiderConstruction(Acre.TypeTerrain type, int x, int y)
        {
            Messages.Id message = jeu.ValiderConstruction(type, x, y);

            if (message != Messages.Id.MessageOk)
            {
                AfficherMessage(message);
            }
            Actualiser();
        }



        /// <summary>
        /// Ramène la position d'ouverture du jeu.
        /// </summary>
        private void btnNouvellePartie_Click(object sender, EventArgs e)
        {
            jeu.Initialiser();
            jeu.AjoutMois();
            Actualiser();
        }

        /// <summary>
        /// Code de triche pour augmenter l'argent de 500 000$
        /// </summary>
        private void lblEnBanque_Click(object sender, EventArgs e)
        {
            jeu.EconomieJeu.MontantBanque += 500000;
            Actualiser();
        }

        /// <summary>
        /// Ajuste l'interval du timer et le texte du label selon le trackbar
        /// </summary>
        private void trackbarTauxSimulation_ValueChanged(object sender, EventArgs e)
        {
            lblTauxSimulation.Text = trackbarTauxSimulation.Value.ToString();
            minuterie.Interval = trackbarTauxSimulation.Value * 1000;
        }

        /// <summary>
        /// Ajuste le taux de taxe dans le jeu et le label du taux de taxe selon le trackbar
        /// </summary>
        private void trackbarTauxTaxe_ValueChanged(object sender, EventArgs e)
        {
            lblTauxTaxe.Text = trackbarTauxTaxe.Value.ToString();
            jeu.EconomieJeu.TauxTaxe = (int)trackbarTauxTaxe.Value;
        }

        /// <summary>
        /// Augmente de 1 mois la partie (appelle la fonction d'ajour de mois du jeu et mets à jour le label).
        /// </summary>
        private void minuterie_Tick(object sender, EventArgs e)
        {
            Messages.Id message = Messages.Id.MessageOk;
            lblDate.Text = jeu.Date.ToLongDateString();
            message = jeu.AjoutMois();
            if (message != Messages.Id.MessageOk)
            {
                AfficherMessage(message);
            }
            Actualiser();
        }

        /// <summary>
        /// Ouvre un boite de dialogue pour sauvegarder la partie (en .tritor)
        /// </summary>
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            minuterie.Enabled = false;
            SaveFileDialog dialogue = new SaveFileDialog();
            BinaryFormatter formateur = new BinaryFormatter();
            dialogue.Title = "Enregistrer sous";
            dialogue.Filter = "Fichier Tritor|*.tritor";
            dialogue.ShowDialog();
            try
            {
                Stream flux = dialogue.OpenFile();
                formateur.Serialize(flux, jeu);
                flux.Close();
            }
            catch
            {

            }
            finally
            {
                minuterie.Enabled = true;
            }
        }

        /// <summary>
        /// Ouvre un boite de dialogue pour charger la partie (en .tritor)
        /// </summary>
        private void btnCharger_Click(object sender, EventArgs e)
        {
            minuterie.Enabled = false;
            OpenFileDialog dialogue = new OpenFileDialog();
            BinaryFormatter formateur = new BinaryFormatter();
            
            dialogue.Filter = "Fichiers Tritor|*.tritor";
            dialogue.Title = "Ouvrir";
            dialogue.ShowDialog();

            try
            {
                Stream flux = dialogue.OpenFile();
                jeu = (Region)formateur.Deserialize(flux);
                flux.Close();
            }
            catch
            {
                MessageBox.Show("Fichier de sauvegarde non valide.");
            }
            finally
            {
                minuterie.Enabled = true;
                Actualiser();
            }
            
        }


    }
}
