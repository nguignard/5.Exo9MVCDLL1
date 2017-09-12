using System;
using System.Collections;
using System.Text;
using System.Windows.Forms; // ajout� manuellement

namespace Exo9
{
    /// <summary>
    /// controleur du cas d'utilisation : lister les stagiaires d'une section
    /// </summary>
    public class CtrlListerStagiairesSection
    {
        /// <summary>
        /// ref au form d'affichage
        /// </summary>
        private frmExo9 leForm;

        /// <summary>
        /// ref � la section g�r�e par ce contr�leur
        /// </summary>
        private MSection laSection;

        /// <summary>
        /// constructeur : instancie et personnalise le form frmExo9 et l'affiche en non modal
        /// </summary>
        public CtrlListerStagiairesSection()
        {
            // pour commencer on ne travaille qu'avec la section CDI
            // chargement en DataSet des donn�es de la section CDI
            this.init();
            
            // instancier le form initial
            this.leForm = new frmExo9(this.laSection);
            // impl�menter l'�v�nement bouton ajouter clic
            this.leForm.btnAjouter.Click += new EventHandler(btnAjouter_Click);
            // impl�menter l'�v�nement bouton supprimer clic
            this.leForm.btnSupprimer.Click += new EventHandler(btnSupprimer_Click);
            // impl�menter l'�v�nement double-clic en datagridview
            this.leForm.grdStagiaires.DoubleClick += new EventHandler(grdStagiaires_DoubleClick);
            // renseigner le form
            this.leForm.AfficheStagiaires(this.laSection); 
            // afficher le form
            this.leForm.MdiParent = Donnees.FrmMDI;
            this.leForm.Show();


        }

        /// <summary>
        /// double-clic sur DataGridView du form frmExo9 :
        /// instancier le form d�tail en y affichant
        /// le stagiaire correspondant � la ligne double-cliqu�e
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdStagiaires_DoubleClick(object sender, EventArgs e)
        {
            // ouvrir la feuille d�tail en y affichant 
            // le stagiaire correspondant � la ligne double-cliqu�e
            MStagiaire leStagiaire;
            Int32 laCle; // cl� (=numOSIA) du stagiaire dans la collection

            // r�cup�rer cl� du stagiaire cliqu� en DataGridView
            laCle = (Int32)this.leForm.grdStagiaires.CurrentRow.Cells[0].Value;
            // instancier un objet stagiaire pointant vers 
            // le stagiaire d'origine dans la collection
            leStagiaire = this.laSection.RestituerStagiaire(laCle);
            // instancier un contr�leur de form d�tail pour ce stagiaire
            CtrlVisuModifStagiaire ctrlVisuModif = new CtrlVisuModifStagiaire(leStagiaire);
            // en sortie r�g�n�rer l'affichage du dataGridView
            this.leForm.AfficheStagiaires(this.laSection);
        }

        /// <summary>
        /// bouton supprimer du form frmExo9 :
        /// supprimer le stagiaire de la collection pour cette section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            //si un stagiaire est point� dans la datagridview
            if (this.leForm.grdStagiaires.CurrentRow != null)
            {
                // r�cup�rer la cl� du stagiaire point�
                Int32 cleStagiaire;
                cleStagiaire = (Int32)this.leForm.grdStagiaires.CurrentRow.Cells[0].Value;
                // demander confirmation de la suppression
                // NB: messagebox retourne une valeur exploitable !
                if (MessageBox.Show("Voulez-vous supprimer le stagiaire num�ro :" + cleStagiaire.ToString(), "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // supprimer et compacter la collection
                    this.laSection.Supprimer(cleStagiaire);
                    // r�afficher la datagridview
                    this.leForm.AfficheStagiaires(this.laSection);
                }
            }
        }

        /// <summary>
        /// bouton ajouter du form frmExo9 :
        /// instancier un form de saisie via le controleur CtrlAjouterStagiaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // instancier un ctrl form de saisie de stagiaire pour afficher le form en modal
            CtrlNouveauStagiaire ctrlNouveau = new CtrlNouveauStagiaire(this.laSection);
            
            // si on sort de la saisie par OK
            if (ctrlNouveau.Resultat == DialogResult.OK)
            {
                try
                {
                    // ajouter la r�f�rence de l'objet MStagiaire cr�� par le ctrleur dans la collection des stagiaires de la section
                    this.laSection.Ajouter(ctrlNouveau.LeStagiaire);
                }
                // gestion des erreurs en MAJ de la collection des MStagiaire de la section
                catch (Exception ex)
                {
                    this.leForm.LeveErreur(ex);
                }
                finally // dans tous les cas, erreur ou non
                {
                    // r�g�n�rer l'affichage du dataGridView 
                    this.leForm.AfficheStagiaires(this.laSection);
                }
            }
        }

        /// <summary>
        /// initialisation section CDI et chargement des stagiaires correcpondants
        /// </summary>
        private void init()
        {
            // initialisation de la collection de sections
            Donnees.Sections = new MSections();
            // pour commencer, une seule section r�f�renc�e "en dur" dans ce programme
            // instancie la section
            this.laSection = new MSection(1, "Concepteur D�veloppeur Informatique 2011");
            // l'ajoute dans la collection des sections g�r�e par la classe de collection
            Donnees.Sections.Ajouter(this.laSection);
            // ajoute en dur un stagiaire � cette section
            MStagiaire unStagiaire;
            unStagiaire = new MStagiaireDE(11111, "DUPONT", "Albert", "12 rue des Fleurs", "NICE", "06300", false);
            this.laSection.Ajouter(unStagiaire);
            
        }



    }
}
