using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Exo9
{
    /// <summary>
    /// classe des sections de stagiaires
    /// </summary>
    public class MSection
    {

        /// <summary>
        /// code de la section = sa cl� 
        /// ==> en lecture seule et initialis� dans le constructeur
        /// </summary>
        private int leCodeSection;

        /// <summary>
        /// obtient le code de la section (lecture seule)
        /// </summary>
        public int CodeSection
        {
            get { return leCodeSection; }
        }

        /// <summary>
        /// nom de la section
        /// (non initialis� dans le constructeur)
        /// </summary>
        private String leNomSection;

        /// <summary>
        /// obtient ou d�finit le nom de la section
        /// </summary>
        public String NomSection
        {
            get { return leNomSection; }
            set { leNomSection = value; }
        }

        /// <summary>
        /// date de d�but de la formation
        /// </summary>
        private DateTime debutFormation;

        /// <summary>
        /// obtient ou d�finit la date de d�but de la formation
        /// </summary>
        public DateTime DebutFormation
        {
            get
            {
                return this.debutFormation;
            }
            set
            {
                this.debutFormation = value;
            }
        }

        /// <summary>
        /// date de fin de la formation
        /// </summary>
        private DateTime finFormation;

        /// <summary>
        /// obtient ou d�finit la date de fin de la formation
        /// </summary>
        public DateTime FinFormation
        {
            get
            {
                return this.finFormation;
            }
            set
            {
                this.finFormation = value;
            }
        }
        /// <summary>
        /// collection des stagiaires de cette section 
        /// (en lecture seule) sous forme de liste tri�e
        /// </summary>
        private SortedDictionary<Int32, MStagiaire> lesStagiaires;


        /// <summary>
        /// datatable des stagiaires pour affichages en datagridview 
        /// et pour exporter/importer en XML
        /// </summary>
        private DataTable dtStagiaires;


        
        /// <summary>
        /// constructeur : attend le code de la section
        /// </summary>
        /// <param name="leCode"></param>
        public  MSection(int leCode, String leNom)
        {
            // initialise le nom de la section
            this.leCodeSection = leCode;
            this.NomSection = leNom.ToUpper();
            // instancie une liste vide pour les stagiaires
            lesStagiaires = new SortedDictionary<Int32, MStagiaire>();
            // datatable : pour recopier les stagiaires (stock�s en collection)
            // et � relier au datagridview pour personnaliser son affichage
            dtStagiaires = new DataTable();

            // ajout � la datatable de 3 colonnes personnalis�es 
            this.dtStagiaires.Columns.Add(new DataColumn("Num�ro OSIA", typeof(System.Int32)));
            this.dtStagiaires.Columns.Add(new DataColumn("Nom", typeof(System.String)));
            this.dtStagiaires.Columns.Add(new DataColumn("Pr�nom", typeof(System.String)));

        }

        /// <summary>
        /// ajouter un stagiaire � la collection
        /// (re�oit la ref au stagiaire et en d�duit la cl� (= numOsia) pour la collection)
        /// </summary>
        /// <param name="unStagiaire">la r�f�rence du stagiaire � ajouter</param>
        public void Ajouter(MStagiaire unStagiaire)
        {
            this.lesStagiaires.Add(unStagiaire.NumOsia, unStagiaire);
        }


        /// <summary>
        /// supprimer un stagaire de la collection
        /// (re�oit la ref au stagiaire et en d�duit la cl� (= numOsia) pour la collection)
        /// </summary>
        /// <param name="unStagiaire">la r�f�rence au stagiaire � supprimer</param>
        public void Supprimer(MStagiaire unStagiaire)
        {
            this.lesStagiaires.Remove(unStagiaire.NumOsia); // � s�curiser...
        }

        /// <summary>
        /// supprimer un stagaire de la collection
        /// (re�oit la cl� du stagiaire (= numOsia) pour la collection
        /// </summary>
        /// <param name="unNumOSIA">la cl� (= numOsia) du stagiaire � supprimer</param>
        /// <exception cref="Exception">Si numOSIA re�u non trouv� en collection</exception>
        public void Supprimer(Int32 unNumOSIA)
        {
            // suppression s�curis�e
            if (this.lesStagiaires.ContainsKey(unNumOSIA))
            {
                this.lesStagiaires.Remove(unNumOSIA);
            }
            else
            {
                throw new Exception("Erreur : num�ro OSIA non trouv� dans la collection");
            }
        }

        /// <summary>
        /// modifier les donn�es d'un stagiaire
        /// tout est modifiable sauf le numOsia (= cl� de la collection)
        /// </summary>
        /// <param name="unStagiaire">la r�f�rence au nouvel objet MStagiaire pour cette cl�</param>
        public void Remplacer(MStagiaire unStagiaire)
        {
            // il suffit de modifier la r�f�rence � l'objet MStagiaire
            // dans la collection pour ce numOsia

            //modifier la r�f�rence de stagiaire stock�e dans la collection            
            this.lesStagiaires[unStagiaire.NumOsia] = unStagiaire;

        }


        /// <summary>
        /// Rechercher un stagiaire dans la liste en connaissant sa cl�
        /// </summary>
        /// <param name="unNumOsia">le num�ro OSIA (=la cl�) du stagiaire � rechercher</param>
        /// <returns>la r�f�rence au stagiaire (ou bien l�ve une erreur)</returns>
        public MStagiaire RestituerStagiaire(Int32 unNumOsia)
        {
            MStagiaire leStagiaire;
            leStagiaire = this.lesStagiaires[unNumOsia] ;
            if (leStagiaire == null)
            {
                throw new Exception("Aucun stagiaire pour le num�ro OSIA " + unNumOsia.ToString());
            }
            else
            {
                return leStagiaire;
            }
        }


        /// <summary>
        /// g�n�rer et retourner une datatable
        /// qui liste les nomOsia, nom et prenom
        /// de tous les stagaires de la collection
        /// </summary>
        /// <returns>une r�f�rence de datatable � 3 colonne</returns>
        public DataTable ListerStagiaires()
        {
            
            // boucle de remplissage de la datatable � partir de la collection
            this.dtStagiaires.Clear();
            foreach (MStagiaire unStagiaire in this.lesStagiaires.Values)
            {
                // instanciation datarow (=ligne)
                DataRow dr;   // ligne de la datatable
                dr = this.dtStagiaires.NewRow();
                // affectation des 3 colonnes
                // r�cup�rer le valeur de cl�
                dr[0] = unStagiaire.NumOsia ;
                // r�cup�rer l'objet MStagiaire correspondant - avec transtypage
                //MStagiaire unStagiaire; // le stagiaire en cours de la collection

                //unStagiaire = this.lesStagiaires[de.Key] as MStagiaire;
                // affecter les 2 autres colonnes des valeurs de prop. de l'objet MStagiaire
                dr[1] = unStagiaire.Nom;
                dr[2] = unStagiaire.Prenom;
                // ajouter la ligne � la datatable
                this.dtStagiaires.Rows.Add(dr);
            }
            // retourne la r�f�rence � la datatable
            return this.dtStagiaires;
        }

    }
}
